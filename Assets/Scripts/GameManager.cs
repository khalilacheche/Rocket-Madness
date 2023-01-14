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
    public GameObject predictionManager;
    public Button nextButton;
    public bool GameEnded;
    public GameObject GameOverPanel;
    public GameObject ghostTrajectoryPrefab;
    public UIStarAnimator uiStarAnimator;
    public Sprite WinSprite;
    public Sprite LoseSprite;
    public Image resultImage;

    private GameObject ghostTrajectory;
    private StarFragmentsManager starFragmentsCounter;
    private int numberFragments;
    private Level currentLevel;
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
        ghostTrajectory = GameObject.Find("Ghost_Trajectory");
        if (ghostTrajectory == null){
            ghostTrajectory = Instantiate(ghostTrajectoryPrefab, Vector3.zero, Quaternion.identity);
        }
        DontDestroyOnLoad(ghostTrajectory);
        starFragmentsCounter = GameObject.Find("Star_Fragments_Counter")?.GetComponent<StarFragmentsManager>();
        starFragmentsCounter.initialize(currentLevel.nbStarFragments);
        numberFragments = 0;

    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = 2;
        if (Input.GetKeyUp(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        if (GameEnded)
            return;
        if (rocket.GetComponent<Health>().health < 1 || GameObject.Find("Boundaries").GetComponent<CollisionHandler>().hasCollidedWithRocket)
        {
            if(rocket.GetComponent<Health>().health < 1)
            {
                Invoke("Lose", 2);
            }
            else
            {
                Lose();
            }
            rocket.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            rocket.GetComponent<Animator>().SetTrigger("explode");
        }
        else if (GameObject.FindGameObjectWithTag("FinishPlanet").GetComponent<CollisionHandler>().hasCollidedWithRocket)
        {
            Win();
        }
    }
    void Lose()
    {
        if (PlayerPrefs.GetInt("CurrentLevel") + 1 <= PlayerPrefs.GetInt("HighestLevel") && PlayerPrefs.GetInt("NumberOfLevels") - 1 > PlayerPrefs.GetInt("CurrentLevel"))
        {
            Color temp = nextButton.gameObject.GetComponent<Image>().color;
            temp.a = 1;
            nextButton.gameObject.GetComponent<Image>().color = temp;
            nextButton.interactable = true;
        }
        else {
            Color temp = nextButton.gameObject.GetComponent<Image>().color;
            temp.a = 0;
            nextButton.gameObject.GetComponent<Image>().color = temp;
            nextButton.interactable = false; 
        }
        GameOverPanel.SetActive(true);
        resultImage.sprite = LoseSprite;
        GameEnded = true;
        //Time.timeScale = 0;
    }
    void Win()
    {
        if (PlayerPrefs.GetInt("NumberOfLevels") - 1 > PlayerPrefs.GetInt("CurrentLevel"))
        {
            Color temp = nextButton.gameObject.GetComponent<Image>().color;
            temp.a = 1;
            nextButton.gameObject.GetComponent<Image>().color = temp;
            nextButton.interactable = true;
        }
        else
        {
            Color temp = nextButton.gameObject.GetComponent<Image>().color;
            temp.a = 0;
            nextButton.gameObject.GetComponent<Image>().color = temp;
            nextButton.interactable = false;
        }
        GameOverPanel.SetActive(true);
        resultImage.sprite = WinSprite;
        bool allStarFragments = numberFragments >= currentLevel.nbStarFragments;
        bool fullHealth = rocket.GetComponent<Health>().health == rocket.GetComponent<Health>().maxHealth;
        uiStarAnimator.StartAnimation(true, allStarFragments, fullHealth);
        GameEnded = true;
        if (PlayerPrefs.GetInt("HighestLevel") == PlayerPrefs.GetInt("CurrentLevel") && PlayerPrefs.GetInt("NumberOfLevels") - 1 > PlayerPrefs.GetInt("HighestLevel"))
        {
            PlayerPrefs.SetInt("HighestLevel", PlayerPrefs.GetInt("CurrentLevel") + 1);
        }
    }
    public void loadNextLevel()
    {
        ghostTrajectory.GetComponent<GhostTrajectoryManager>().EmptyOut();
        PlayerPrefs.SetInt("CurrentLevel", PlayerPrefs.GetInt("CurrentLevel") + 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void restartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void GoHome()
    {
        Destroy(ghostTrajectory);
        SceneManager.LoadScene("Main Menu");
    }
    public void addFragment(){
        numberFragments++;
        starFragmentsCounter?.addFragment();
    }
    void LoadLevel()
    {
        int levelIndex = PlayerPrefs.GetInt("CurrentLevel");
        TextAsset level = Resources.Load<TextAsset>("Levels/levels");
        StreamReader strm = new StreamReader(new MemoryStream(level.bytes));
        string json = strm.ReadToEnd();
        Level[] levels = Levels.CreateFromJSON(json).levels;
        Level thisLevel = levels[levelIndex];
        currentLevel = thisLevel;
        for (int i = 0; i < thisLevel.elements.Length; i++)
        {
            predictionManager.GetComponent<PredictionManager>().addElement(instantiator.InstantiateElement(thisLevel.elements[i]));
        }

    }
}
