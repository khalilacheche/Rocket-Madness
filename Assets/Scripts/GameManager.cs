using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    public bool rocketLaunched;
    public GameObject rocket;
    public GameObject moonPrefab;
    public GameObject finishPlanetPrefab;
    public Button nextButton;
    public bool GameEnded;
    public GameObject GameOverPanel;
    // Use this for initialization
    void Start()
    {
        LoadLevel();
        rocket = GameObject.FindGameObjectWithTag("Rocket");
        rocketLaunched = false;
        GameEnded = false;
        Time.timeScale = 1;
        GameOverPanel = GameObject.FindWithTag("GameOver");
        nextButton = GameObject.Find("Next Button").GetComponent<Button>();
        GameOverPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        if (GameEnded)
            return;
        if (rocket.GetComponent<Health>().health <= 1 || GameObject.Find("Boundaries").GetComponent<CollisionHandler>().hasCollidedWithRocket){
            Lose();
        }else if (GameObject.FindGameObjectWithTag("FinishPlanet").GetComponent<CollisionHandler>().hasCollidedWithRocket){
            Win();
        }
    }
    void Lose (){
        if (PlayerPrefs.GetInt("CurrentLevel") + 1 <= PlayerPrefs.GetInt("HighestLevel") && PlayerPrefs.GetInt("NumberOfLevels") - 1 > PlayerPrefs.GetInt("CurrentLevel"))
        { 
            nextButton.interactable = true; 
        } else { nextButton.interactable = false; }
        rocket.GetComponent<Animator>().SetTrigger("explode");
        rocket.transform.GetChild(1).GetComponent<Animator>().SetTrigger("fadeout");
        GameOverPanel.SetActive(true);
        print("You Lost!");
        GameEnded = true;
        //Time.timeScale = 0;
    }
    void Win(){
        if(PlayerPrefs.GetInt("NumberOfLevels") -1>PlayerPrefs.GetInt("CurrentLevel")){
            nextButton.interactable = true;
        }else{
            nextButton.interactable = false;
        }

        GameOverPanel.SetActive(true);
        print("You Won!");
        GameEnded = true;
        //Time.timeScale = 0;
        if (PlayerPrefs.GetInt("HighestLevel") == PlayerPrefs.GetInt("CurrentLevel") && PlayerPrefs.GetInt("NumberOfLevels") - 1 > PlayerPrefs.GetInt("HighestLevel"))
        {
            PlayerPrefs.SetInt("HighestLevel", PlayerPrefs.GetInt("CurrentLevel") + 1);
        } 
    }
    public void loadNextLevel(){
        PlayerPrefs.SetInt("CurrentLevel",PlayerPrefs.GetInt("CurrentLevel")+1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void restartLevel(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void GoHome(){
        SceneManager.LoadScene("Main Menu");
    }
    void LoadLevel(){
        int levelIndex = PlayerPrefs.GetInt("CurrentLevel");
        TextAsset level = Resources.Load<TextAsset>("Levels/levels");
        StreamReader strm = new StreamReader(new MemoryStream(level.bytes));
        string json = strm.ReadToEnd();
        Level [] levels = Levels.CreateFromJSON(json).levels;
        Level thisLevel = levels[levelIndex];
        for (int i = 0; i < thisLevel.elements.Length;i++){
            instantiator.InstantiateElement(thisLevel.elements[i]);
        }

    }
}
