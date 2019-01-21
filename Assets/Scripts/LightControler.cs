using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class LightControler : MonoBehaviour
{
    public float maxLightSpeed = 15f;
    public float delta = 0.15f;
    public float multiplier = 1f;

    public GameObject deathEffect;

    //public Slider sliderSensitivity;  


    private bool isMoving = false;
    private bool needToMove = false;
    private Vector3 targetPos;

    private Camera cam;
    private Vector3 previousLightPos;
    private Vector3 previousMousePos;
    private Vector3 currentMousePos;
    private CircleCollider2D circleCollider;

    private Vector3 world;


    private void Awake()
    {
        multiplier = PlayerPrefs.GetFloat("multiplier", multiplier);
        //sliderMultiplier.value = multiplier;
    }

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        circleCollider = GetComponent<CircleCollider2D>();

        world = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0.0f));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartMoving();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopMoving();
        }

        if(isMoving)
        {
            //GetCurrentMousePos();

            previousMousePos = currentMousePos;
            currentMousePos = cam.ScreenToWorldPoint(Input.mousePosition);

            Vector2 move = currentMousePos - previousMousePos;

            SetLightTargetPos(move);
        }

        if (needToMove)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPos, maxLightSpeed * Time.deltaTime);
        }
    }

    private void GetCurrentMousePos()
    {
        currentMousePos = cam.ScreenToWorldPoint(Input.mousePosition);
    }

    private void StartMoving()
    {
        isMoving = true;
        currentMousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        needToMove = true;
    }

    private void StopMoving()
    {
        isMoving = false;
    }

    private void SetLightTargetPos(Vector3 movementVector)
    {
        Vector3 lightPosScreen = transform.position + (movementVector * multiplier);
        // x
        if (lightPosScreen.x >= world.x)
        {
            lightPosScreen.x = world.x;
        }
        else if (lightPosScreen.x <= - world.x)
        {
            lightPosScreen.x = - world.x;
        }
        // y
        if (lightPosScreen.y >= world.y)
        {
            lightPosScreen.y = world.y;
        }
        else if (lightPosScreen.y <= -world.y)
        {
            lightPosScreen.y = -world.y;
        }

        targetPos = lightPosScreen;
        needToMove = true;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Debug.Log("collision"); // affD

        if (col.tag == "MovingBlock")   // we get hit by a block
        {
            Debug.Log("collision with movingBlock"); // affD
            GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, 1f);
        }
    }
}


//using UnityEngine;

//[RequireComponent(typeof(Rigidbody2D))]
//[RequireComponent(typeof(CircleCollider2D))]
//public class LightControler : MonoBehaviour
//{
//    public float lightSpeed = 10f;
//    public float delta = 0.25f;
//    public float multiplier = 1f;

//    //public Slider sliderSensitivity;  


//    private bool isMoving = false;
//    private bool needToMove = false;
//    private Vector3 targetPos;

//    private Camera cam;
//    private Vector3 previousLightPos;
//    private Vector3 startMousePos;
//    private Vector3 currentMousePos;
//    private Rigidbody2D rb;
//    private CircleCollider2D circleCollider;

//    private Vector3 world;


//    private void Awake()
//    {
//        multiplier = PlayerPrefs.GetFloat("multiplier", multiplier);
//        //sliderMultiplier.value = multiplier;
//    }

//    // Start is called before the first frame update
//    void Start()
//    {
//        cam = Camera.main;
//        rb = GetComponent<Rigidbody2D>();
//        circleCollider = GetComponent<CircleCollider2D>();

//        world = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0.0f));
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (Input.GetMouseButtonDown(0))
//        {
//            StartMoving();
//        }
//        else if (Input.GetMouseButtonUp(0))
//        {
//            StopMoving();
//        }

//        if (isMoving)
//        {
//            GetCurrentMousePos();
//        }

//        if (needToMove)
//        {
//            Vector3 direction = targetPos - transform.position;
//            transform.Translate(direction.normalized * lightSpeed * Time.deltaTime, Space.World);

//            if (Vector3.Distance(transform.position, targetPos) <= delta)
//            {
//                needToMove = false;
//            }
//        }
//    }

//    private void GetCurrentMousePos()
//    {
//        currentMousePos = cam.ScreenToWorldPoint(Input.mousePosition);
//    }

//    private void StartMoving()
//    {
//        isMoving = true;
//        startMousePos = cam.ScreenToWorldPoint(Input.mousePosition);
//    }

//    private void StopMoving()
//    {
//        isMoving = false;
//        Vector2 move = currentMousePos - startMousePos;
//        //MoveLight(move);
//        SetLightTargetPos(move);
//        needToMove = true;
//    }

//    private void SetLightTargetPos(Vector3 movementVector)
//    {
//        Vector3 lightPosScreen = transform.position + (movementVector * multiplier);
//        // x
//        if (lightPosScreen.x >= world.x)
//        {
//            lightPosScreen.x = world.x;
//        }
//        else if (lightPosScreen.x <= -world.x)
//        {
//            lightPosScreen.x = -world.x;
//        }
//        // y
//        if (lightPosScreen.y >= world.y)
//        {
//            lightPosScreen.y = world.y;
//        }
//        else if (lightPosScreen.y <= -world.y)
//        {
//            lightPosScreen.y = -world.y;
//        }

//        targetPos = lightPosScreen;
//    }

//    //private void MoveLight(Vector3 movementVector)
//    //{
//    //    transform.position = transform.position + movementVector;
//    //}
//}

