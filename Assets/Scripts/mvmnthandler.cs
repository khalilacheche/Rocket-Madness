using UnityEngine;

public class mvmnthandler : MonoBehaviour
{
    bool isPressing;
    Vector3 startPos;
    Vector3 mousePos;
    public GameObject rocket;
    public float launchspeed;
    public Vector2 force;
    GameObject rotatingPlatform;
    GameManager gm;
    GameObject trajectory;
    float timeSinceLaunch;
    void Start()
    {
//        Debug.Log("straight distance : " + Vector2.Distance(rocket.transform.position, GameObject.FindGameObjectWithTag("FinishPlanet").transform.position));
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        rotatingPlatform = GameObject.FindGameObjectWithTag("RotatingPlatform");
        timeSinceLaunch = 0;
        trajectory = GameObject.FindGameObjectWithTag("trajectory");
    }


    void Update()
    {
        if (isPressing)
        {
            trajectory.SetActive(true);
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 mvmntVect = mousePos-startPos;
            if (Vector2.Distance(mousePos, startPos) < 0.01)
                return;
            float rotationZ = Vector2.Angle(Vector2.right, mvmntVect) * (mousePos.y > startPos.y ? 1 : -1);
            rotatingPlatform.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ - 90);
            rocket.transform.parent.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ + 90);
            Vector3 direction = rotatingPlatform.transform.position - rocket.transform.GetChild(0).transform.position;
            float intensity = Vector2.Distance(mousePos, startPos);
            intensity = Mathf.Clamp(intensity, 0, 1f);
            force = -launchspeed * intensity * direction;
            trajectory.GetComponent<trajectory>().CalcTraj();
        }
        else
        {
            //print("Time since launch: "+ (Time.time - timeSinceLaunch));
            trajectory.SetActive(true);


        }
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            // Handle finger movements based on touch phase.
            switch (touch.phase)
            {
                // Record initial touch position.
                case TouchPhase.Began:
                    startPos = touch.position;
                    isPressing = true;
                    break;

                // Determine direction by comparing the current touch position with the initial one.
                case TouchPhase.Moved:
                    break;

                // Report that a direction has been chosen when the finger is lifted.
                case TouchPhase.Ended:
                    isPressing = false;
                    break;
            }
        }
    }
    void OnMouseDown()
    {
        if (gm.rocketLaunched)
            return;
        isPressing = true;
        startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //GameObject.FindGameObjectWithTag("trajectory").SetActive(true);

    }
    void OnMouseUp()
    {
        isPressing = false;
        LaunchRocket();
    }
    void LaunchRocket(){
        if (gm.rocketLaunched)
            return;
        if (force.magnitude < 5)
            return;
        rocket.GetComponent<Rigidbody2D>().AddForce(force);
        gm.rocketLaunched = true;
        rocket.transform.GetChild(1).GetComponent<Animator>().SetTrigger("launch-rocket");
        timeSinceLaunch = Time.time;
    }
}
