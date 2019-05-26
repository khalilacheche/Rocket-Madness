using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {

    public float health;
    Vector3 reference;
    public Slider healthbar;


	// Use this for initialization
    void Start () {

    }
    private void Update()
    {
        health = Mathf.Clamp(health, 0, 100);
        healthbar.value = health / 100;
    }

    public void TakeDamage(float amount){
        health -= amount;

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        reference = transform.position - collision.transform.position;
        float severity = Mathf.Sin(Vector2.Angle(transform.right,reference)*Mathf.Deg2Rad);
        TakeDamage(100 * severity);
    }

}
