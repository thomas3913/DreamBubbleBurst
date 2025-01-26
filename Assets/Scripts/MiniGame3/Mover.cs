using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour
{
    private Vector3 startPosition;
    private Vector3 targetPosition;

    private float[,] speedsOut = { { 2.0f, 2.0f, 2.0f }, { 4.0f, 4.0f, 4.0f }, { 8.0f, 8.0f, 8.0f } };
    private float speedIn = 5.0f;

    private float[,] distancesX = { { -1.0f, 0.0f, 1.0f }, { -1.0f, 0.0f, 1.0f }, { -1.0f, 0.0f, 1.0f  } };
    private float[,] distancesY = { { -3.0f, -3.0f, -3.0f }, { -3.0f, -3.0f, -3.0f  }, { -3.0f, -3.0f, -3.0f  } };

    private float rotationSpeed = 5f;
    private float speed;
    private float[] distance = { 0.0f, 0.0f};

    private float line;
    private float[] lines = { 5.0f, 5.0f};

    private int level = 0;

    private float timer = 0.0f;
    private float[] wait = {1.0f, 2.0f, 3.0f};

    private bool go = true;
    private bool smack = false;

    public GameObject smackground;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position;

        Out();
    }

    // Update is called once per frame
    void Update()
    {
        // float temp1 = speed;
        // float temp2 = rotationSpeed;  

        if (timer > 0)
        {
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
            speed = speedIn;
            rotationSpeed = 5.0f;
        //     // stop = true;
        //     // return;
        }

        Rotate();
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (transform.position[1] <= line) 
        {
            // Debug.Log("Now!");
        }

        if (Input.GetMouseButtonDown(0)) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            if (transform.position[1] <= line && hit.collider != null && hit.collider.gameObject != smackground)
            {
                if(smack == false)
                {
                    Debug.Log("Yes!");
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
                level=0;
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

        line = lines[Random.Range(0, lines.Length)];

        if (level < speedsOut.GetLength(0))
        {
            Debug.Log(level);
            speed = speedsOut[level,Random.Range(0, speedsOut.GetLength(1))];

            distance[0] = distancesX[level,Random.Range(0, distancesX.GetLength(1))];
            distance[1] = distancesY[level,Random.Range(0, distancesY.GetLength(1))];
        }

        // line = Random.Range(distance[1]-2, distance[1]-1); ???

        targetPosition = new Vector3(startPosition.x+distance[0], startPosition.y+distance[1], 0.0f);
    }

    void In()
    {
        speed = speedIn;
        targetPosition = startPosition;
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
