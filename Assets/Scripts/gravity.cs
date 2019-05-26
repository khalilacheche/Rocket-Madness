﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gravity : MonoBehaviour {
	public float gravityScale;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

    void OnTriggerStay2D(Collider2D col)
    {
        Vector2 gravityVect = CalcGravity(col.gameObject);
        Debug.DrawLine(transform.position, transform.position + new Vector3(gravityVect.x,gravityVect.y, 0));
        if (col.gameObject.tag == "Rocket"){
            col.gameObject.GetComponent<Rigidbody2D>().AddForce(gravityVect);
        }
    }
	Vector2 CalcGravity(GameObject obj1){
        return (gameObject.transform.position - obj1.transform.position) * gravityScale / Mathf.Pow(Vector2.Distance(gameObject.transform.position,obj1.transform.position),2) ;
	}

}
