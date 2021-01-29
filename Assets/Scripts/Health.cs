using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour {

    public float maxHealth;
    public float health;
    public Slider healthbar;


	// Use this for initialization
    void Start () {
        health = maxHealth;
    }
    private void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        healthbar.value = health / maxHealth;
    }

    public void TakeDamage(float amount){
        health -= amount;

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector3 reference = transform.position - collision.transform.position;
        float severity = Mathf.Sin(Vector2.Angle(transform.right,reference)*Mathf.Deg2Rad);
        TakeDamage(100 * severity);
    }

}
