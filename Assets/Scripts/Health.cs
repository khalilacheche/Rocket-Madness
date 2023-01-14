using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {

    public float maxHealth;
    public float health;
    public Slider healthbar;
    private float velocityBeforeCollisionUpdate;
    private Rigidbody2D rb2d;


	// Use this for initialization
    void Start () {
        health = maxHealth;
        rb2d = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        healthbar.value = health / maxHealth;
        velocityBeforeCollisionUpdate = rb2d.velocity.magnitude;
    }

    public void TakeDamage(float amount){
        health -= amount;

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        TakeDamage(collision);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        //TakeDamage(collision);
    }
    private void TakeDamage(Collision2D collision)
    {
        if(collision.gameObject.tag != "FinishPlanet" && collision.gameObject.tag != "boundaries")
        {
            Vector3 reference = transform.position - collision.transform.position;
            float severity = 0.05f + (1/2.5f) *velocityBeforeCollisionUpdate*Mathf.Abs(Mathf.Cos(Vector2.Angle(transform.up, reference) * Mathf.Deg2Rad));
            TakeDamage(100 * severity);

        }
    }

}
