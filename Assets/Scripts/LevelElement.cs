using System;
[Serializable]
public class LevelElement
{
    public string type;
    public float position_x;
    public float position_y;
    public float scale_x;
    public float scale_y;
    public float gravity_scale;
    public float radius;
    public LevelElement(UnityEngine.GameObject param){
        position_x = param.transform.position.x;
        position_y = param.transform.position.y;
        switch(param.tag){
            case "Planet":
                scale_x = param.transform.GetChild(0).transform.localScale.x;
                scale_y = param.transform.GetChild(0).transform.localScale.y;
                radius = param.transform.GetChild(2).GetComponent<UnityEngine.CircleCollider2D>().radius;
                gravity_scale = param.transform.GetChild(2).GetComponent<gravity>().gravityScale;
                break;
            case "FinishPlanet":
                scale_x = param.transform.localScale.x;
                scale_y = param.transform.localScale.y;
                radius = param.GetComponent<UnityEngine.CircleCollider2D>().radius;
                break;
            case "BlackHole":
                scale_x = param.transform.localScale.x;
                scale_y = param.transform.localScale.y;
                radius = param.GetComponent<UnityEngine.CircleCollider2D>().radius;
                break;
        }

        type = param.tag;
    }
}
