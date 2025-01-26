using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour
{
    private Vector3 startPosition;
    private Vector3 targetPosition;
    private Vector3 upPosition;

    private float[,] speedsOut = { { 2.0f, 2.0f, 2.0f }, { 4.0f, 4.0f, 6.0f }, { 8.0f, 10.0f, 12.0f } };
    private float speedIn = 5.0f;

    private float[,] distancesX = { { -0.75f, -0.75f, 0.0f, 0.0f }, { -0.75f, 0.0f, 0.0f, 0.75f }, { -1.0f, -0.75f, 0.0f, 1.0f  } };
    private float[,] distancesY = { { -2f, -2f, -2f }, { -1.5f, -2f, -2f  }, { -1.0f, -1.5f, -2f } };

    private float rotationSpeed = 5f;
    private float speed;
    private float[] distance = { 0.0f, 0.0f};

    private float line;
    private float[] lines = { 5.0f, 5.0f};

    private int level = 0;

    private float timer = 0.0f;
    private float[] wait = {0.0f, 0.0f, 0.0f, 0.5f, 0.5f, 1.0f, 1.0f, 2f};

    private bool go = true;
    private bool smack = false;

    public GameObject smackground;

    public GameObject[] levellst;

    public Sprite mouse1;  // The new sprite to switch to
    public Sprite mouse2;  // The new sprite to switch to
    public Sprite mouse3;  // The new sprite to switch to
    public Sprite empty;

    private SpriteRenderer spriteRenderer;  // Reference to the SpriteRenderer component
    private SpriteRenderer spriteRenderer2; 

    public GameObject endScreen;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position;

        upPosition = startPosition;
        upPosition.y = upPosition.y-1;

        spriteRenderer = GetComponent<SpriteRenderer>();

        Up();
    }

    // Update is called once per frame
    void Update()
    {
        // float temp1 = speed;
        // float temp2 = rotationSpeed;  

        if (timer > 0)
        {
            spriteRenderer.sprite = mouse2;
            timer -= Time.deltaTime;
            speed = 0;
            rotationSpeed = 0;
            // if (go == false)
            // {
            //     rotationSpeed = 0;
            // }
            // return;
        }
        else
        {
            if (transform.position[1] <= line && smack == false && Vector3.Distance(transform.position, startPosition) > 0.25)
            {
                spriteRenderer.sprite = mouse3;
            }

            if (transform.position[1] > line) 
            {
                spriteRenderer.sprite = mouse1;
            }

            speed = speedIn;
            rotationSpeed = 5.0f;
        //     // stop = true;
        //     // return;
        }

        Rotate();
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (Input.GetMouseButtonDown(0)) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            if (transform.position[1] <= line && hit.collider != null && hit.collider.gameObject != smackground)
            {
                if(smack == false && Vector3.Distance(transform.position, startPosition) > 0.25)
                {
                    Debug.Log("Yes!");
                    if (level < levellst.Length)
                    {
                        spriteRenderer2 = levellst[level].GetComponent<SpriteRenderer>();
                        spriteRenderer2.sprite = empty;
                    }

                    level++;
                    timer = 1.0f;
                    smack = true;

                }

                // if (stop == false)
                // {
                //     StartCoroutine(Freeze2());
                // }
            }
            else if (transform.position[1] > line && hit.collider != null && hit.collider.gameObject != smackground)
            {
                Debug.Log("No!");
                // level=0;
                endScreen.SetActive(true);
            }

            In();
        }

        else if (Vector3.Distance(transform.position, targetPosition) < 0.1) 
        {
            go = false;
            In();
        }

        if (Vector3.Distance(transform.position, startPosition) < 0.1)
        {
            if(go == false)
            {
                timer = wait[Random.Range(0, wait.Length)];
                go = true;
            }
            // if (stop == false)
            // {
            //     StartCoroutine(Freeze1());
            // }
            smack = false;

            Out();
        }

        if (level == 3) 
        {
            End();
            level = 0;
        }
    }

    void Out() 
    {
        //wait[Random.Range(0, wait.Length)];
        spriteRenderer.sprite = mouse1;

        Vector3 tempos = startPosition;
        tempos.y = tempos.y-2;

        // transform.position = Vector3.MoveTowards(transform.position, tempos, speed * Time.deltaTime);

        // line = lines[Random.Range(0, lines.Length)];

        if (level < speedsOut.GetLength(0))
        {
            Debug.Log(level);
            speed = speedsOut[level,Random.Range(0, speedsOut.GetLength(1))];

            distance[0] = distancesX[level,Random.Range(0, distancesX.GetLength(1))];
            distance[1] = distancesY[level,Random.Range(0, distancesY.GetLength(1))];
        }

        line = Random.Range(distance[1]+1f, distance[1]+0.25f); // ???

        targetPosition = new Vector3(startPosition.x+distance[0], startPosition.y+distance[1], 0.0f);
    }

    void In()
    {
        speed = speedIn;
        targetPosition = startPosition;
    }

    void Up()
    {
        targetPosition = upPosition;
    }

    void Rotate()
    {
        Vector2 direction = (Vector2)targetPosition - (Vector2)transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90;

        Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    void End()
    {
        Debug.Log("You win!");
        Destroy(gameObject, 1);

        SceneManager.Instance.loadScene("MainScene");

        
    }

    // IEnumerator Freeze1()
    // {
    //         Debug.Log("Freeze1!");
    //         stop = true;
    //         yield return new WaitForSeconds(wait[Random.Range(0, wait.Length)]);
    //         stop = false;
    // }

    // IEnumerator Freeze2()
    // {
    //         Debug.Log("Freeze2!");
    //         stop = true;
    //         yield return new WaitForSeconds(3.0f);
    //         stop = false;
    // }
}
