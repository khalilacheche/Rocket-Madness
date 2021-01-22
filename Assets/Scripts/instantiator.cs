using UnityEngine;
using System.IO;

public class instantiator {
    public static GameObject moonPrefab = Resources.Load<GameObject>("Prefabs/Planet");
    public static GameObject finishPlanetPrefab = Resources.Load<GameObject>("Prefabs/Finish Planet");
    public static GameObject blackholePrefab = Resources.Load<GameObject>("Prefabs/Black Hole");


    public static GameObject InstantiateElement(LevelElement element){
        GameObject a = null;
        switch (element.type)
        {
            case "moon":
            case "Planet":
                a=MonoBehaviour.Instantiate(moonPrefab, new Vector3(element.position_x, element.position_y, 0), Quaternion.identity);
                a.transform.GetChild(0).transform.localScale = new Vector3(element.scale_x, element.scale_y, 1);
                a.transform.GetChild(2).GetComponent<gravity>().gravityScale = element.gravity_scale;
                a.transform.GetChild(2).GetComponent<CircleCollider2D>().radius = element.radius;
                break;
            case "finish_planet":
            case "FinishPlanet":
                a =MonoBehaviour.Instantiate(finishPlanetPrefab, new Vector3(element.position_x, element.position_y, 0), Quaternion.identity);
                a.transform.localScale = new Vector3(element.scale_x, element.scale_y, 1);
                a.GetComponent<CircleCollider2D>().radius = element.radius;
                break;
            case "BlackHole":
                a = MonoBehaviour.Instantiate(blackholePrefab, new Vector3(element.position_x, element.position_y, 0), Quaternion.identity);
                a.transform.localScale = new Vector3(element.scale_x, element.scale_y, 1);
                a.GetComponent<CircleCollider2D>().radius = element.radius;
                break;
        }
        return a;
    }
}
