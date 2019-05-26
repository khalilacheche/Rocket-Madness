using UnityEngine;
using System.IO;

public class instantiator {
    public static GameObject moonPrefab = Resources.Load<GameObject>("Prefabs/Planet");
    public static GameObject finishPlanetPrefab = Resources.Load<GameObject>("Prefabs/Finish Planet");
    public static GameObject blackholePrefab = Resources.Load<GameObject>("Prefabs/Black Hole");


    public static void InstantiateElement(LevelElement element){
        switch (element.type)
        {
            case "moon":
            case "Planet":
                GameObject a=MonoBehaviour.Instantiate(moonPrefab, new Vector3(element.position_x, element.position_y, 0), Quaternion.identity);
                a.transform.GetChild(0).transform.localScale = new Vector3(element.scale_x, element.scale_y, 1);
                a.transform.GetChild(2).GetComponent<gravity>().gravityScale = element.gravity_scale;
                a.transform.GetChild(2).GetComponent<CircleCollider2D>().radius = element.radius;
                break;
            case "finish_planet":
            case "FinishPlanet":
                GameObject b =MonoBehaviour.Instantiate(finishPlanetPrefab, new Vector3(element.position_x, element.position_y, 0), Quaternion.identity);
                b.transform.localScale = new Vector3(element.scale_x, element.scale_y, 1);
                b.GetComponent<CircleCollider2D>().radius = element.radius;
                break;
            case "BlackHole":
                GameObject c = MonoBehaviour.Instantiate(blackholePrefab, new Vector3(element.position_x, element.position_y, 0), Quaternion.identity);
                c.transform.localScale = new Vector3(element.scale_x, element.scale_y, 1);
                c.GetComponent<CircleCollider2D>().radius = element.radius;
                break;
        }
    }
}
