using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class LevelsGenerator : MonoBehaviour
{

    GameObject rocket;
    GameManager gm;
    Vector3 rocket_initial_pos;
    Quaternion rocket_initial_rotation;
    GameObject[] Planets;
    string[] combos;
    float straight_distance;
    float distanceTravelled;
    Vector2 lastPosition;
    void Start()
    {
        //GenerateForceCombo(4f, 5f);
        Time.timeScale = 100f;

        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        rocket = GameObject.FindGameObjectWithTag("Rocket");
        rocket_initial_pos = rocket.transform.position;
        rocket_initial_rotation = rocket.transform.rotation;

        Planets = GameObject.FindGameObjectsWithTag("Planet");
        StartCoroutine("GenerateLevel");
    }


    public void GenerateLevel()
    {
        Debug.Log("TIME SCALE : " + Time.timeScale);
        Debug.Log("Started Testing COMBOS");
        lastPosition = rocket.transform.position;
        PlayerPrefs.SetInt("successful_tries", 0);
        PlayerPrefs.SetFloat("successful_tries_distance_sum", 0);
        Debug.Log("CREATING LEVEL");
        Level level = new Level();
        level.LevelNumber = 1;
        level.ObstaclesNumber = Planets.Length;
        Vector2[] vectors = new Vector2[level.ObstaclesNumber];

        for (int i = 0; i < level.ObstaclesNumber; i++)
        {
            vectors[i] = new Vector2(Planets[i].transform.position.x, Planets[i].transform.position.y);
            level.ObstaclesPositions = vectors;
        }
        combos = File.ReadAllLines(Application.dataPath + "/combo.txt");

        Debug.Log("Startig Recursive");
        straight_distance = Vector2.Distance(rocket.transform.position, GameObject.FindGameObjectWithTag("FinishPlanet").transform.position);
        Debug.Log("straight distance : " + straight_distance);
        StartCoroutine(LaunchRocketTest(0, level));




        //Get Obstacles Positions 

        /* Create a new file with following format: 
         * Level n 
         * Obstacles number 
         * Obstacles Positions : [12,12],[11,11]
         * Succes Rate : %
         * Difficulty : [easy,medium,hard,very hard]         
        */
    }


    IEnumerator LaunchRocketTest(int i, Level level)
    {
        resetScene();
        if (i >= combos.Length)
        {
            decimal success_Rate = (decimal)PlayerPrefs.GetInt("successful_tries") / combos.Length;
            Debug.Log(success_Rate);
            level.Level_Difficulty = level.GetLevelDifficulty();
            Debug.Log("DONE TESTING , STORING RESULTS ....");
            float successful_tries_distance_avg = PlayerPrefs.GetFloat("successful_tries_distance_sum") / PlayerPrefs.GetInt("successful_tries");
            Debug.Log("SUM " + PlayerPrefs.GetFloat("successful_tries_distance_sum"));
            Debug.Log("AVG " + (successful_tries_distance_avg / 9.8f));
            level.SuccessRate = (float)success_Rate + (successful_tries_distance_avg / 9.8f);
            Debug.Log("SUCESS RATE " + level.SuccessRate);
            level.StoreFile();

            yield return null;
        }
        else
        {
            float x = float.Parse(combos[i].Split(',')[0]);
            float y = float.Parse(combos[i].Split(',')[1]);
            if (x < 10f || Mathf.Abs(y) > 85f)
            {
                StartCoroutine(LaunchRocketTest(i + 1, level));
            }
            else
            {
                rocket.GetComponent<Rigidbody2D>().AddForce(new Vector2(x, y));
                gm.rocketLaunched = true;
                yield return new WaitForSeconds(6);

                if (PlayerPrefs.GetInt("skip") == 1)
                {
                    distanceTravelled = 0;
                    StartCoroutine(LaunchRocketTest(i + 1, level));
                }
                else if (PlayerPrefs.GetInt("skip") == 2)
                {
                    PlayerPrefs.SetInt("successful_tries", PlayerPrefs.GetInt("successful_tries") + 1);
                    Debug.Log("this successfull attempt took : " + distanceTravelled);
                    PlayerPrefs.SetFloat("successful_tries_distance_sum", PlayerPrefs.GetFloat("successful_tries_distance_sum") + distanceTravelled);
                    StartCoroutine(LaunchRocketTest(i + 1, level));
                }
                else
                {
                    distanceTravelled = 0;
                    StartCoroutine(LaunchRocketTest(i + 1, level));

                }
            }
        }


    }

    void Update()
    {

        distanceTravelled += Vector2.Distance(rocket.transform.position, lastPosition);
        lastPosition = rocket.transform.position;

    }

    void resetScene()
    {

        PlayerPrefs.SetInt("skip", 0);
        gm.rocketLaunched = false;
        rocket.transform.position = rocket_initial_pos;
        rocket.transform.rotation = rocket_initial_rotation;
        lastPosition = rocket.transform.position;
        distanceTravelled = 0;

    }

    public void Arrived()
    {
        PlayerPrefs.SetInt("skip", 2);
        gm.rocketLaunched = false;
        rocket.transform.position = rocket_initial_pos;
        rocket.transform.rotation = rocket_initial_rotation;
    }

    public void skipTry()
    {
        PlayerPrefs.SetInt("skip", 1);
    }
    void GenerateForceCombo(float x_rate, float y_rate)
    {
        List<float> x_values = new List<float>();
        List<float> y_values = new List<float>();

        for (float i = 0; i < 110f; i += x_rate)
        {
            x_values.Add(i);
        }

        for (float i = 0; i < 85f; i += y_rate)
        {
            y_values.Add(i);
        }


        float[][] matrix = (
            from a in x_values
            from b in y_values.Concat(y_values.Select(x => -x).ToList()).ToArray()
            select new[] { a, b }
    ).ToArray();

        Debug.Log("Combos : " + matrix.Length);
        string path = Application.dataPath + "/combo.txt";

        using (StreamWriter sw = (File.Exists(path)) ? File.AppendText(path) : File.CreateText(path))
        {
            for (int i = 0; i < matrix.Length; i++)
            {
                string line = matrix[i][0] + "," + matrix[i][1];
                sw.WriteLine(line);
            }
        }
    }
}
