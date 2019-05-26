using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boundaries : MonoBehaviour {
    public bool rocketOutOfBoundaries;
	// Use this for initialization
	void Start () {
        rocketOutOfBoundaries = false;
	}
	
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag =="Rocket"){
            rocketOutOfBoundaries = true;
        }
    }
}
