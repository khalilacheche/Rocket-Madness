using UnityEngine;

public class mvmnthandler : MonoBehaviour
{
    private bool isPressing;
    private bool canLaunch;
    private Vector3 startPos;
    private Vector3 mousePos;
    public float launchspeed;
    private Vector2 launchVelocity;
    private float initialOffset;
    private GameObject rocket;
    private GameObject rotatingPlatform;
    private LineRenderer inputLine;
    private GameManager gm;
    private GameObject trajectory;
    private GameObject ghostTrajectory;
    void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        rotatingPlatform = GameObject.FindGameObjectWithTag("RotatingPlatform");
        rocket = GameObject.FindGameObjectWithTag("Rocket");
        trajectory = GameObject.FindGameObjectWithTag("trajectory");
        initialOffset = Vector2.Distance(rotatingPlatform.transform.position , rocket.transform.position);
        inputLine = gameObject.GetComponent<LineRenderer>();
        inputLine.positionCount = 2;
        inputLine.material = new Material(Shader.Find("Sprites/Default"));
        ghostTrajectory = GameObject.Find("Ghost_Trajectory");
    }


    void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (gm.rocketLaunched || !isPressing || startPos == mousePos) {
            inputLine.enabled = false;
            return;
        }
        inputLine.enabled = true;
        float normDirection = Vector2.Distance(mousePos, startPos);
        inputLine.SetPosition(0, new Vector3(startPos.x,startPos.y,1));
        inputLine.SetPosition(1, new Vector3(mousePos.x, mousePos.y, 1));
        Vector3 velocityDirection = (mousePos - startPos) / normDirection;
        float rotationZ = Vector2.Angle(Vector2.left, velocityDirection)* (Mathf.Sign(startPos.y-mousePos.y));
        velocityDirection = new Vector3(Mathf.Min(0,velocityDirection.x),velocityDirection.x>0?rotationZ>90?-1:1:velocityDirection.y,0);
        float clampedRotation = Mathf.Clamp(rotationZ, -90, 90);
        rotatingPlatform.transform.rotation = Quaternion.Euler(0.0f, 0.0f, clampedRotation + 90);
        rocket.transform.rotation = Quaternion.Euler(0.0f, 0.0f, clampedRotation - 90);
        rocket.transform.position = rotatingPlatform.transform.position - velocityDirection*initialOffset;
        if (normDirection < 1)
        {
            canLaunch = false;
            inputLine.startColor = Color.gray;
            inputLine.endColor = Color.gray;
            trajectory.GetComponent<PredictionManager>().hideDots();
            //trajectory.GetComponent<LineRenderer>().enabled = false;
        }
        else
        {
            canLaunch = true;
            inputLine.endColor = Color.white;
            inputLine.startColor = Color.white;
            float intensity = normDirection - 1f;
            launchVelocity = -intensity* launchspeed  * velocityDirection;
            trajectory.GetComponent<PredictionManager>().predict(rocket, rocket.transform.position, launchVelocity);
            trajectory.GetComponent<PredictionManager>().showDots();
            //trajectory.GetComponent<LineRenderer>().enabled = true;
        }


    }
    void OnMouseDown()
    {
        if (gm.rocketLaunched)
            return;
        isPressing = true;
        startPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

    }
    void OnMouseUp()
    {
        isPressing = false;
        LaunchRocket();
    }
    private void LaunchRocket(){
        if (gm.rocketLaunched)
            return;
        if (!canLaunch)
            return;
        rocket.GetComponent<Rigidbody2D>().velocity = launchVelocity;
        gm.rocketLaunched = true;
        rocket.transform.GetChild(1).GetComponent<Animator>().SetTrigger("launch-rocket");
        rocket.transform.GetChild(2).GetComponent<Animator>().SetTrigger("launch-rocket");
        ghostTrajectory?.GetComponent<GhostTrajectoryManager>().Migrate(trajectory?.GetComponent<PredictionManager>().getDots());
    }
    public Vector2 getLaunchVelocity(){
        return new Vector2(launchVelocity.x,launchVelocity.y);
    }
}
