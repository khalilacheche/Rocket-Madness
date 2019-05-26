using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketControls : MonoBehaviour
{
    Rigidbody2D rb;
    GameManager gm;
//    LevelsGenerator levelsGenerator;
    float timer = 0;
    // Use this for initialization
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
     //   levelsGenerator = Camera.main.GetComponent<LevelsGenerator>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(gameObject.transform.position, gameObject.transform.position + new Vector3(rb.velocity.x, rb.velocity.y, 0));
        if (gm.rocketLaunched)
        {
            if (rb.velocity == Vector2.zero)
                return;
            transform.up = Vector2.Lerp(transform.forward, rb.velocity, Time.deltaTime * 5);
        }
        if (gm.GameEnded){
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0;

        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "FinishPlanet")
        {
    //        levelsGenerator.Arrived();
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Planet")
        {
            timer += Time.time;
        }

        if (timer > 4f)
        {
   //         levelsGenerator.skipTry();
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "BlackHole")
        {
            Debug.Log("aaa");
            gameObject.GetComponent<Animator>().SetTrigger("fadeout");
            rb.velocity = Vector2.zero;
            transform.GetChild(1).GetComponent<Animator>().SetTrigger("fadeout");
            gameObject.GetComponent<Health>().TakeDamage(100);
        }
    }

}
