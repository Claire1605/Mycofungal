using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nutrient : MonoBehaviour
{
    public Path initialPath;
    public int initialDirection = 1;
    public float moveSpeed;

    private float currentT = 0.0f;
    private Path currentPath;
    private int lastPoint = 0;
    private int currentPoint = 1;
    private int currentDirection = 0;

    private Vector3 initialScale;
    
    void Start()
    {
        initialScale = transform.localScale;

        Reset();

        if (currentDirection != 1 && currentDirection != -1)
        {
            Debug.LogError("invalid direction: " + currentDirection, gameObject);
        }
    }

    void Update()
    {
        if (currentPath.GetPathOn() == false)
        {
            Reset();
        }

        currentT += (moveSpeed / GetCurrentDistance()) * Time.deltaTime;

        if (currentT > 1.0f)
        {
            currentT = 0.0f;

            GoToNextPoint();

            GetComponent<SpriteRenderer>().color = GetCurrentColour();
        }

        // Always update position
        transform.position = GetCurrentPosition();

        // wobble scale
        transform.localScale = new Vector3(initialScale.x * (1.0f + Mathf.PerlinNoise(Time.time, 0.0f) * 0.1f), initialScale.y * (1.0f + Mathf.PerlinNoise(0.0f, Time.time) * 0.1f), transform.localScale.z);
    }

    private void Reset()
    {
        currentT = 0.0f;
        currentPath = initialPath;
        currentDirection = initialDirection;

        if (currentDirection == 1)
        {
            lastPoint = 0;
            currentPoint = 1;
        }
        else
        {
            lastPoint = currentPath.GetPointCount() - 1;
            currentPoint = currentPath.GetPointCount() - 2;
        }

        transform.position = GetCurrentPosition();

        GetComponent<SpriteRenderer>().color = GetCurrentColour();
    }

    void GoToNextPoint()
    {
        // go to next point based on direction
        lastPoint = currentPoint;
        currentPoint += currentDirection;
        
        if (currentDirection == 1)
        {
            if (currentPoint >= currentPath.GetPointCount())
            {
                if (currentPath.nextPath != null)
                {
                    // if next path exists, switch to it and restart
                    currentPath = currentPath.nextPath;
                    currentPoint = 1;
                    lastPoint = 0;
                }
                else
                {
                    // if there is no next path, change direction and go to end
                    currentDirection = -1;
                    currentPoint = currentPath.GetPointCount() - 2;
                    lastPoint = currentPath.GetPointCount() - 1;
                }
            }
        }
        else
        {
            if (currentPoint < 0)
            {
                if (currentPath.previousPath != null)
                {
                    // if previous path exists, switch to it and go to end
                    currentPath = currentPath.previousPath;
                    currentPoint = currentPath.GetPointCount() - 2;
                    lastPoint = currentPath.GetPointCount() - 1;
                }
                else
                {
                    // if there is no previous path, change direction and restart
                    currentDirection = 1;
                    currentPoint = 1;
                    lastPoint = 0;
                }
            }
        }
    }

    Vector3 GetCurrentPosition()
    {
        return currentPath.GetPathPosition(lastPoint, currentPoint, currentT);
    }

    float GetCurrentDistance()
    {
        return currentPath.GetDistanceBetweenPoints(lastPoint, currentPoint);
    }

    Color GetCurrentColour()
    {
        if (currentDirection == 1)
        {
            return currentPath.GetPathColour(currentPoint);
        }
        else
        {
            return currentPath.GetPathColour(lastPoint);
        }
    }
}
