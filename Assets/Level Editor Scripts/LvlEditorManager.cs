using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.Serialization;

public class LvlEditorManager : MonoBehaviour
{
    public bool rocketLaunched;
    public GameObject rocket;
    public GameObject moonPrefab;
    public GameObject finishPlanetPrefab;
    bool GameEnded;
    // Use this for initialization
    void Start()
    {
        rocket = GameObject.FindGameObjectWithTag("Rocket");
        rocketLaunched = false;
        GameEnded = false;
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        if (GameEnded)
            return;
        if (rocket.GetComponent<Health>().health <= 1 || GameObject.Find("Boundaries").GetComponent<CollisionHandler>().hasCollidedWithRocket)
        {
            Lose();
        }
        else if (GameObject.FindGameObjectWithTag("FinishPlanet").GetComponent<CollisionHandler>().hasCollidedWithRocket)
        {
            Win();
        }
    }
    void Lose()
    {
        print("You Lost!");
        GameEnded = true;
        Time.timeScale = 0;
    }
    void Win()
    {
        print("You Won!");
        GameEnded = true;
        Time.timeScale = 0;
    }

}
