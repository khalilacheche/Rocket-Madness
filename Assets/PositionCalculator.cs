using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionCalculator : MonoBehaviour {

    float X;
    float Y;
    float time;
    float velocity;
    float angle;

    // Use this for initialization
    void Start () {
        //gameObject.GetComponent<Collider2D>() = GameObject.FindGameObjectWithTag("Rocket").GetComponent<CircleCollider2D>();
        //TODO: Copy CircleCollider 2D component from rocket GO
    }
	
    public void setPosition(float T, float V, float A){
        time = T;
        velocity = V;
        angle = A;
        X = Mathf.Cos(A) * V * T + GameObject.FindGameObjectWithTag("Rocket").transform.position.x;
        Y = Mathf.Sin(A) * V * T + GameObject.FindGameObjectWithTag("Rocket").transform.position.y;
        transform.position = new Vector2(X, Y);
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "GravityField"){
            /*transform.position =new Vector2 (
                X-0.5f*Mathf.Cos(Mathf.Deg2Rad * Vector2.Angle(Vector2.right,gameObject.transform.position - col.transform.position  )* (gameObject.transform.position.y > col.transform.position.y ? 1 : -1))* Mathf.Pow(time,2),
                Y - 0.5f * Mathf.Sin(Mathf.Deg2Rad * Vector2.Angle(Vector2.right, gameObject.transform.position - col.transform.position) * (gameObject.transform.position.y > col.transform.position.y ? 1 : -1)) * Mathf.Pow(time, 2)

            );*/
        }
    }
}
