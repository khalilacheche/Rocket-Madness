

/* Create a new file with following format: 
        * Level n 
        * Obstacles number 
        * Obstacles Positions : [12,12],[11,11]
        * Succes Rate : %
        * Difficulty : [easy,medium,hard,very hard]         
       */
/*using System.IO;
using UnityEngine;
public class Level
{

    public enum Difficulty
    {
        easy, medium, hard, madness, ignore
    }
    public string PATH = Application.dataPath + "/Levels/";
    int levelNumber;

    public int LevelNumber
    {
        get
        {
            return levelNumber;
        }

        set
        {
            levelNumber = value;
        }
    }

    int obstaclesNumber;

    public int ObstaclesNumber
    {
        get
        {
            return obstaclesNumber;
        }

        set
        {
            obstaclesNumber = value;
        }
    }

    Vector2[] obstaclesPositions;

    public Vector2[] ObstaclesPositions
    {
        get
        {
            return obstaclesPositions;
        }

        set
        {
            obstaclesPositions = value;
        }
    }

    float successRate;

    public float SuccessRate
    {
        get
        {
            return successRate;
        }
        set
        {
            successRate = value;
        }
    }

    Difficulty difficulty;

    public Difficulty Level_Difficulty
    {
        get
        {
            return difficulty;
        }

        set
        {
            difficulty = value;
        }
    }

    public Difficulty GetLevelDifficulty()
    {
        return successRate > .8 ? Difficulty.easy :
                                      successRate > .6 ? Difficulty.medium : successRate > .3f ? Difficulty.hard : successRate > .15f ? Difficulty.madness : Difficulty.ignore;
    }


    public void StoreFile()
    {
        string path = PATH + "Level_" + levelNumber + ".txt";
        using (StreamWriter sw = (File.Exists(path)) ? File.AppendText(path) : File.CreateText(path))

        {

            sw.WriteLine("Level : " + levelNumber);
            sw.WriteLine("Obstacles Number : " + obstaclesNumber);
            sw.WriteLine("Obstacles Position : " + obstaclesNumber);
            for (int i = 0; i < obstaclesPositions.Length; i++)
            {
                string line = obstaclesPositions[i][0] + "," + obstaclesPositions[i][1];
                sw.WriteLine(line);
            }
            sw.WriteLine("END");
            sw.WriteLine("Succes Rate :" + successRate);
            sw.WriteLine("Difficulty : " + GetLevelDifficulty().ToString());
            Debug.Log("File Stored Successfuly");
        }
    }
}
*/