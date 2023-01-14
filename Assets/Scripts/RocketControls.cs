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
            rb = gameObject.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.zero;
            rb.angularVelocity = 0;
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
            gameObject.GetComponent<Animator>().SetTrigger("fadeout");
            transform.GetChild(1).GetComponent<Animator>().SetTrigger("fadeout");
            gameObject.GetComponent<Health>().TakeDamage(gameObject.GetComponent<Health>().maxHealth);
        }else if(col.gameObject.tag == "StarFragment")
        {
            Destroy(col.gameObject);
            gm.addFragment();
        }
    }

}
