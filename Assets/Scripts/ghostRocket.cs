using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghostRocket : MonoBehaviour {

	Rigidbody2D rb;
	// Use this for initialization
	void Start () {
	rb = gameObject.GetComponent<Rigidbody2D>();
	rb.AddForce(new Vector2 (10f,0f));	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
