using UnityEngine;
using System.Collections;

public class MouseMover : MonoBehaviour
{
    private float[] speeds = {5.0f, 3.0f, 1.0f};
    private float[] distances_x = {-1.0f, 0.0f, 1.0f};
    private float[] distances_y = {-3.0f, -2.0f, -1.0f};
    private float[] aims = {-2.5f, -1.0f, -0.5f};
    private float[] wait = {0.0f, 1.0f};

    private Vector3 start;
    private Vector3 targetPosition;
    private float speed;
    private float distance_x;
    private float distance_y;

    private float arrivalThreshold = 0.1f;

    private int level;
    private float aim;

    private bool stop = false;

    

    void Start()
    {
        level = 0;
        aim = -0.5f;
        distance_x = distances_x[Random.Range(0, distances_x.Length)];
        distance_y = distances_y[Random.Range(0, distances_y.Length)];
        speed = speeds[Random.Range(0, speeds.Length)];
        start = transform.position;
        // distance_x = start.x;
        targetPosition = new Vector3(distance_x, distance_y, 0.0f);
    }

    void Update()
    {

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (Input.GetMouseButtonDown(0) || Vector3.Distance(transform.position, targetPosition) < arrivalThreshold)
        {
            if (Input.GetMouseButtonDown(0)) 
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

                if (transform.position[1] <= aim && hit.collider != null)
                {
                    Debug.Log("Got it! ≽^•⩊•^≼");
                    Debug.Log(level);
                    level++;
                    aim++;
                }
                else if (transform.position[1] > aim && hit.collider != null)
                {
                    Debug.Log("Oh noooooo! ≽^╥⩊╥^≼");
                    Debug.Log(level);
                    level=0;
                }
            }
            if (distance_x != start.x && distance_y != start.y)
            {
                distance_x = start.x;
                distance_y = start.y;
                speed = 5.0f;
            }
            else
            {
                if (stop == false)
                {
                    StartCoroutine(ResetVariableForTime());
                }
                distance_x = distances_x[Random.Range(0, distances_x.Length)];
                distance_y = distances_y[Random.Range(0, distances_y.Length)];
                speed = speeds[Random.Range(0, speeds.Length)]+(float)level;
            }

            targetPosition = new Vector3(distance_x, distance_y, 0.0f);
        }

        if (level == 3)
        {
            Debug.Log("You win! Mjam! ฅ^>⩊<^ฅ");
            Destroy(gameObject, 1);
        }

        IEnumerator ResetVariableForTime()
        {
            stop = true;
            yield return new WaitForSeconds(wait[Random.Range(0, wait.Length)]);
            stop = false;
        }
    }
}