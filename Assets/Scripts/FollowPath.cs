using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    public Path initialPath;
    public float moveSpeed;
    public int direction = 1;

    private Path currentPath;
    private int lastPoint = 0;
    private int currentPoint = 1;
    private float currentT = 0.0f;

    private Vector3 initialScale;
    
    void Start()
    {
        initialScale = transform.localScale;

        currentPath = initialPath;

        if (direction == -1)
        {
            currentT = 1.0f;
        }

        transform.position = GetPosition();
    }
    
    void Update()
    {
        currentT += (moveSpeed / GetDistance()) * Time.deltaTime * direction;

        if ((direction == 1 && currentT > 1.0f) || (direction == -1 && currentT < 0.0f))
        {
            NextPoint();

            GetComponent<SpriteRenderer>().color = GetColour();
        }

        transform.position = GetPosition();

        transform.localScale = new Vector3(initialScale.x * (1.0f + Mathf.PerlinNoise(Time.time, 0.0f) * 0.1f), initialScale.y * (1.0f + Mathf.PerlinNoise(0.0f, Time.time) * 0.1f), transform.localScale.z);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            lastPoint = 0;
            currentPoint = 1;
            currentT = 0.0f;

            GetComponent<SpriteRenderer>().color = GetColour();
            transform.position = GetPosition();
        }
    }

    void NextPoint()
    {
        switch (direction)
        {
            case 1:
                lastPoint = currentPoint;
                currentPoint += 1;
                
                if (currentPoint < currentPath.GetPointCount())
                {
                    currentT = 0.0f;
                }
                else
                {
                    if (currentPath.nextPath != null)
                    {
                        currentPath = currentPath.nextPath;
                        currentT = 0.0f;
                        currentPoint = 1;
                        lastPoint = 0;
                    }
                    else
                    {
                        direction = -1;
                        currentT = 1.0f;
                        currentPoint = currentPath.GetPointCount() - 2;
                        lastPoint = currentPath.GetPointCount() - 1;
                    }
                }

                break;
            case -1:
                lastPoint = currentPoint;
                currentPoint -= 1;

                if (currentPoint >= 0)
                {
                    currentT = 1.0f;
                }
                else
                {
                    if (currentPath.previousPath != null)
                    {
                        currentPath = currentPath.previousPath;
                        currentT = 1.0f;
                        currentPoint = currentPath.GetPointCount() - 2;
                        lastPoint = currentPath.GetPointCount() - 1;
                    }
                    else
                    {
                        direction = 1;
                        currentT = 0.0f;
                        currentPoint = 1;
                        lastPoint = 0;
                    }
                }
                break;
            default: Debug.Log("direction is invalid: " + direction, gameObject);
                break;
        }
    }

    float GetDistance()
    {
        float currentDistance = 1.0f;

        if (currentPath.pathPoints != null && lastPoint < currentPath.GetPointCount() && currentPoint < currentPath.GetPointCount())
        {
            currentDistance = Vector3.Distance(currentPath.GetPoint(lastPoint).transform.position, currentPath.GetPoint(currentPoint).transform.position);
        }

        return currentDistance;
    }

    Vector3 GetPosition()
    {
        Vector3 currentPosition = transform.position;

        if (currentPath != null && currentPath.pathPoints != null && lastPoint < currentPath.GetPointCount() && currentPoint < currentPath.GetPointCount())
        {
            currentPosition = Vector3.Lerp(currentPath.GetPoint(lastPoint).transform.position, currentPath.GetPoint(currentPoint).transform.position, currentT);
        }

        return currentPosition;
    }

    Color GetColour()
    {
        Color currentColour = GetComponent<SpriteRenderer>().color;

        if (currentPath != null && currentPath.pathPoints != null && currentPoint < currentPath.GetPointCount())
        {
            if (currentPath.GetPoint(currentPoint).name.ToLower().Contains("yellow"))
            {
                currentColour = Color.yellow;
            }

            if (currentPath.GetPoint(currentPoint).name.ToLower().Contains("cyan"))
            {
                currentColour = Color.cyan;
            }

            if (currentPath.GetPoint(currentPoint).name.ToLower().Contains("white"))
            {
                currentColour = Color.white;
            }
        }

        return currentColour;
    }
}
