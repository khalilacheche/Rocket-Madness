using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour {
    public bool hasCollidedWithRocket;
	// Use this for initialization
	void Start () {
        hasCollidedWithRocket = false;
	}
	
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag=="Rocket"){
            hasCollidedWithRocket = true;
        }
    }
}
