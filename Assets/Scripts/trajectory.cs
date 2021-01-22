using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trajectory : MonoBehaviour {
    public float frequency;
    public int number;
    public GameObject dotsPref;
    public GameObject[] dots;
    private GameObject rocket;
    mvmnthandler mvmnt;
	// Use this for initialization
	void Start () {
        dots = new GameObject[number];
        for (int i = 0; i < number;i++){
            GameObject dot = Instantiate(dotsPref);
            dot.transform.parent = transform;
            dots[i] = dot;
            dots[i].name = (i/frequency).ToString();
        }
        mvmnt= GameObject.Find("RocketLauncher").GetComponent<mvmnthandler>();
        rocket = GameObject.FindGameObjectWithTag("Rocket");

    }

    public void CalcTraj(){
        Vector2 acceleration;
        Vector2 velocity;
        Vector2 position;
        for (int i = 0; i < number; i++){
            float time = i/frequency ;
            print(i+" "+ time);
            acceleration = getAcceleration(time);
            velocity = getVelocity(time);
            float x = 0.5f*acceleration.x*time*time+velocity.x*time+rocket.transform.position.x;
            float y = 0.5f*acceleration.y*time*time+velocity.y*time+rocket.transform.position.y;
            position = new Vector2 (x,y);
            dots[i].transform.position = position;
        }
    }

    private Vector2 getVelocity(float time){
        return mvmnt.getLaunchVelocity();
    }
    private Vector2 getAcceleration(float time){
        return new Vector2(0,0);
    }
}