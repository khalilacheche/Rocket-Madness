using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LvlEditortrajectory : MonoBehaviour {
    public float frequency;
    public int number;
    public GameObject dotsPref;
    public GameObject[] dots;
    LvlEditormvmnthandler mvmnt;
    // Use this for initialization
    void Start () {
        dots = new GameObject[number];
        for (int i = 0; i < number;i++){
            GameObject dot = Instantiate(dotsPref);
            dot.transform.parent = transform;
            dots[i] = dot;
            dots[i].name = (i/frequency).ToString();
        }
        mvmnt= GameObject.Find("RocketLauncher").GetComponent<LvlEditormvmnthandler>();

    }

    public void CalcTraj(){
        float V = Mathf.Sqrt( mvmnt.force.x* mvmnt.force.x + mvmnt.force.y * mvmnt.force.y)*0.1f;
        float A = Mathf.Deg2Rad * Vector2.Angle(Vector2.right,mvmnt.force) * (mvmnt.force.y > 0 ? 1 : -1 );
        for (int i = 0; i < number; i++){
            float time = i/frequency ;
            //dots[i].GetComponent<PositionCalculator>().setPosition(time,V,A);
        }
    }
}