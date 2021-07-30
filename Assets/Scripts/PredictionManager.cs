using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PredictionManager : MonoBehaviour
{
    private List<GameObject> levelElements = new List<GameObject>();
    public int maxIterations;
    public int divider;
    public GameObject dotPrefab;
    private bool hasCopied = false;
    private GameObject[] dots;

    private Scene currentScene;
    private Scene predictionScene;

    private PhysicsScene2D currentPhysicsScene;
    private PhysicsScene2D predictionPhysicsScene;

    private List<GameObject> dummyObstacles = new List<GameObject>();

    //private LineRenderer lineRenderer;
    private GameObject dummy;

    void Start()
    {
        Physics2D.autoSimulation = false;
        dots = new GameObject[maxIterations/divider];
        float slope = (1 - (50f / 255f)) / dots.Length;
        for (int i = 0; i < dots.Length; ++i)
        {
            dots[i] = Instantiate(dotPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
            dots[i].transform.parent = gameObject.transform;
            Color temp = dots[i].GetComponent<SpriteRenderer>().color;
            temp.a = 1 - (i * slope);
            dots[i].GetComponent<SpriteRenderer>().color = temp;
            dots[i].SetActive(false);
        }
        currentScene = SceneManager.GetActiveScene();
        currentPhysicsScene = currentScene.GetPhysicsScene2D();

        CreateSceneParameters parameters = new CreateSceneParameters(LocalPhysicsMode.Physics2D);
        predictionScene = SceneManager.CreateScene("Prediction", parameters);
        predictionPhysicsScene = predictionScene.GetPhysicsScene2D();

        //lineRenderer = GetComponent<LineRenderer>();
        Color c1 = Color.white;
        Color c2 = new Color(1, 1, 1, 0);
        //lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        //lineRenderer.startColor = c1;
        //lineRenderer.endColor = c2;




    }

    void FixedUpdate()
    {
        if (currentPhysicsScene.IsValid())
        {
            if (!hasCopied)
            {
                copyAllObstacles();
                hasCopied = true;
            }
            currentPhysicsScene.Simulate(Time.fixedDeltaTime);
        }
    }

    public void copyAllObstacles()
    {
        foreach (GameObject go in levelElements)
        {
            Transform t = go.transform;
            if (t.gameObject.tag!="Rocket" && t.gameObject.tag != "FinishPlanet")
            {
                GameObject fakeT = Instantiate(t.gameObject);
                fakeT.transform.position = t.position;
                fakeT.transform.rotation = t.rotation;
                Renderer fakeR = fakeT.GetComponent<Renderer>();
                if (fakeR)
                {
                    fakeR.enabled = false;
                }
                SceneManager.MoveGameObjectToScene(fakeT, predictionScene);
                dummyObstacles.Add(fakeT);
            }
        }
    }
    void killAllObstacles()
    {
        foreach (var o in dummyObstacles)
        {
            Destroy(o);
        }
        dummyObstacles.Clear();
    }

    public void predict(GameObject subject, Vector3 currentPosition, Vector2 launchVelocity)
    {
        if (currentPhysicsScene.IsValid() && predictionPhysicsScene.IsValid())
        {
            if (dummy == null)
            {
                dummy = Instantiate(subject);
                SceneManager.MoveGameObjectToScene(dummy, predictionScene);
            }

            dummy.transform.position = currentPosition;
            dummy.GetComponent<Rigidbody2D>().velocity = launchVelocity;
            //lineRenderer.positionCount = 0;
            //lineRenderer.positionCount = maxIterations;

            
            for (int i = 0; i < maxIterations; i++){
                predictionPhysicsScene.Simulate(Time.fixedDeltaTime);
                if (i % divider == 0){
                    dots[i/divider].transform.position = dummy.transform.position;

                }
                //lineRenderer.SetPosition(i, dummy.transform.position);
            }

            Destroy(dummy);
        }
    }
    public void addElement(GameObject elem){
        levelElements.Add(elem);
    }

    void OnDestroy()
    {
        killAllObstacles();
    }

    public void showDots()
    {
        for (int i =0; i < dots.Length; ++i) {
            dots[i].SetActive(true);
        }
    }
    public void hideDots(){
        for (int i = 0; i < dots.Length; ++i)
        {
            dots[i].SetActive(false);
        }
    }
    public GameObject[] getDots()
    {
        return dots;
    }

}