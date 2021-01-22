using UnityEngine;

public class mvmnthandler : MonoBehaviour
{
    bool isPressing;
    Vector3 startPos;
    Vector3 mousePos;
    public float launchspeed;
    public Vector2 launchVelocity;
    private float initialOffset;
    private GameObject rocket;
    private GameObject rotatingPlatform;
    GameManager gm;
    GameObject trajectory;
    float timeSinceLaunch;
    void Start()
    {
//        Debug.Log("straight distance : " + Vector2.Distance(rocket.transform.position, GameObject.FindGameObjectWithTag("FinishPlanet").transform.position));
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        rotatingPlatform = GameObject.FindGameObjectWithTag("RotatingPlatform");
        rocket = GameObject.FindGameObjectWithTag("Rocket");
        timeSinceLaunch = 0;
        trajectory = GameObject.FindGameObjectWithTag("trajectory");
        initialOffset = Vector2.Distance(rotatingPlatform.transform.position , rocket.transform.position);
    }


    void Update()
    {
        if(gm.rocketLaunched){
            timeSinceLaunch += Time.deltaTime;
           // print(timeSinceLaunch);

        }
        trajectory.SetActive(true);
        if (isPressing)
        {
            mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            float normDirection = Vector2.Distance(mousePos, startPos);
            Vector3 velocityDirection = (mousePos-startPos)/normDirection;
            
            Debug.DrawLine(startPos,mousePos);
            if (Vector2.Distance(mousePos, startPos) < 0.01)
                return;
            float rotationZ = Vector2.Angle(Vector2.right, velocityDirection) * (mousePos.y > startPos.y ? 1 : -1);
            rotatingPlatform.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ - 90);
            rocket.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ + 90);
            rocket.transform.position = rotatingPlatform.transform.position - velocityDirection*initialOffset;
            float intensity = Mathf.Clamp(normDirection, 0, 1f);
            launchVelocity = -launchspeed * intensity * velocityDirection;
            trajectory.GetComponent<PredictionManager>().predict(rocket,rocket.transform.position,launchVelocity);
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
        if (launchVelocity.magnitude < 1)
            return;
        rocket.GetComponent<Rigidbody2D>().velocity = launchVelocity;
        gm.rocketLaunched = true;
        rocket.transform.GetChild(1).GetComponent<Animator>().SetTrigger("launch-rocket");
        timeSinceLaunch = 0;
    }
    public Vector2 getLaunchVelocity(){
        return new Vector2(launchVelocity.x,launchVelocity.y);
    }
}
