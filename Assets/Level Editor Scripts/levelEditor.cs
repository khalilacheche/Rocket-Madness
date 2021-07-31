using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using System;


public class levelEditor : MonoBehaviour {
    public string levelName;
    public int index;
    public Levels levels;
    public GameObject[] gameObjects;
    public Level currentLevel;
    public Text loadText;
    public Text saveText;
    public GameObject moonPrefab;
    public GameObject finishPlanetPrefab;
    public Toggle ovrwrte;
    public int nbFragments;

    // Use this for initialization
    void Start () {
        TextAsset level = Resources.Load<TextAsset>("Levels/levels");
        StreamReader strm = new StreamReader(new MemoryStream(level.bytes));
        string json = strm.ReadToEnd();
        levels = Levels.CreateFromJSON(json);
        gameObjects = GetGameElements();
    }
	
	// Update is called once per frame
	void Update () {
        gameObjects = GetGameElements();
    }
#pragma warning disable CS0168
    public void Save(){

        try
        {

            if(int.Parse(saveText.text)>levels.levels.Length|| int.Parse(saveText.text)<0){
                throw new IndexOutOfRangeException();
            }
            LevelElement[] lvlElements = new LevelElement[gameObjects.Length];
            Level[] lvls;
            for (int i = 0; i < lvlElements.Length; i++)
            {
                lvlElements[i] = new LevelElement(gameObjects[i]);
            }
            currentLevel.elements = lvlElements;
            currentLevel.nbStarFragments = nbFragments;
            if (ovrwrte.isOn)
            {
                lvls = new Level[levels.levels.Length];
                for (int i = 0; i < lvls.Length;i++){
                    if(i == int.Parse(saveText.text)){
                       lvls[i] = currentLevel;
                    }else{
                        lvls[i] = levels.levels[i];
                    }
                }
            }
            else
            {
                lvls = new Level[levels.levels.Length + 1];
                int j = 0;
                for (int i = 0; i < lvls.Length; i++)
                {

                    if (i == int.Parse(saveText.text))
                    {
                        lvls[i] = currentLevel;
                    }
                    else
                    {
                        lvls[i] = levels.levels[j];
                        j++;
                    }
                }
            }

            levels.levels = lvls;
            string json = JsonUtility.ToJson(levels);
            StreamWriter strm = new StreamWriter(Application.dataPath + "/Resources/Levels/levels.json",false);
            strm.Write(json);
            strm.Close();
            PlayerPrefs.SetInt("NumberOfLevels", levels.levels.Length);
            Debug.Log("Saved Successfully");
        }
        catch (IndexOutOfRangeException a)
        {
            Debug.LogError("YAATEK AASBA MOCH MAWJOUD");
        }

    }
    public void LoadLevel(){
        TextAsset level = Resources.Load<TextAsset>("Levels/levels");
        StreamReader strm = new StreamReader(Application.dataPath + "/Resources/Levels/levels.json");
        string json = strm.ReadToEnd();
        levels = Levels.CreateFromJSON(json);
        strm.Close();
        try
        {
            if (int.Parse(loadText.text) >= levels.levels.Length) throw new IndexOutOfRangeException();
            for (int i = 0; i < gameObjects.Length;i++){
                Destroy(gameObjects[i]);
            }
            currentLevel = levels.levels[int.Parse(loadText.text)];
            levelName = currentLevel.name;
            nbFragments = currentLevel.nbStarFragments;
            for (int i = 0; i < currentLevel.elements.Length; i++)
            {
                instantiator.InstantiateElement(currentLevel.elements[i]);
            }
        }
        catch (IndexOutOfRangeException a)
        {
            
            Debug.LogError("YAATEK AASBA MOCH MAWJOUD");
        }
       
    }
    GameObject[] GetGameElements(){
        GameObject[] array=FindObjectsOfType<GameObject>();
        List<GameObject> result = new List<GameObject>();
        for (int i = 0; i < array.Length;i++){
            try
            {
                gameElement ga = array[i].GetComponent<gameElement>();
                ga.test(); 
                if (array[i].activeInHierarchy && ga != null) result.Add(array[i]);
            }
            catch (NullReferenceException a)
            {

            }
        }
        return result.ToArray();
    }
}
