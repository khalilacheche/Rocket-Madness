using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class main_menu : MonoBehaviour {

    public GameObject scroll;
    public GameObject buttonPrefab;
    public Button[] lvlButtons;
    public GameObject backButton;
    // Use this for initialization
    void Start () {
        hideLevelsPanel();
        if (PlayerPrefs.GetInt("firstTime",0)==0){
            PlayerPrefs.SetInt("firstTime", 1);
            PlayerPrefs.SetInt("HighestLevel", 0);
            PlayerPrefs.SetInt("CurrentLevel", 0);
            TextAsset level = Resources.Load<TextAsset>("Levels/levels");
            StreamReader strm = new StreamReader(new MemoryStream(level.bytes));
            string json = strm.ReadToEnd();
            PlayerPrefs.SetInt("NumberOfLevels", Levels.CreateFromJSON(json).levels.Length);
        }
        scroll.transform.GetChild(0).transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(0, PlayerPrefs.GetInt("NumberOfLevels") * 300);
        for (int i = 0; i < PlayerPrefs.GetInt("NumberOfLevels"); i++)
        {
            GameObject a = Instantiate(buttonPrefab);
            a.transform.parent = scroll.transform.GetChild(0).transform.GetChild(0).transform;
            a.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, scroll.transform.GetChild(0).transform.GetChild(0).GetComponent<RectTransform>().rect.height-220 - i*244);
            a.transform.GetChild(0).GetComponent<Text>().text = i.ToString();
            a.name = i.ToString();
            a.GetComponent<Button>().interactable = i <= PlayerPrefs.GetInt("HighestLevel");
        }
    }
    
    // Update is called once per frame
    void Update () {
        
    }
    public void loadLevel(string index){
        PlayerPrefs.SetInt("CurrentLevel", int.Parse(index));
        Debug.Log("loading level " + index);
        SceneManager.LoadScene("Main");
    }
    public void loadLastLevel(){
        PlayerPrefs.SetInt("CurrentLevel", PlayerPrefs.GetInt("HighestLevel"));
        SceneManager.LoadScene("Main");
    }
    public void showLevelsPanel(){

        scroll.SetActive(true);
        backButton.SetActive(true);

    }
    public void hideLevelsPanel()
    {
        backButton.SetActive(false);
        scroll.SetActive(false);
    }
    public void Reset()
    {
        PlayerPrefs.SetInt("firstTime", 0);
        SceneManager.LoadScene(0);
    }
}
