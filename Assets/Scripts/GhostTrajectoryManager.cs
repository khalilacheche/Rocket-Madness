using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostTrajectoryManager : MonoBehaviour
{
    private GameObject[] dots;
    // Start is called before the first frame update
    void Start()
    {
        dots = new GameObject[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Migrate(GameObject[] newDots){
        for (int i = 0; i < dots.Length; ++i){
            Destroy(dots[i]);
        }
        dots = newDots;
        for (int i =0; i < dots.Length; ++i){
            dots[i].transform.SetParent(transform,true);
            dots[i].GetComponent<SpriteRenderer>().color = Color.grey;
        }
    }
    public void EmptyOut()
    {
        Migrate(new GameObject[0]);
    }
}
