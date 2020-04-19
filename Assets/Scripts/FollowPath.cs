using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPath : MonoBehaviour
{
    public Transform points;
    public float moveSpeed;

    private int lastPoint = 0;
    private int currentPoint = 1;
    private float currentT = 0.0f;

    private Vector3 initialScale;
    
    void Start()
    {
        initialScale = transform.localScale;

        transform.position = GetPosition();
    }
    
    void Update()
    {
        if (currentPoint < points.childCount)
        {
            currentT += (moveSpeed / GetDistance()) * Time.deltaTime;

            if (currentT > 1.0f)
            {
                currentT = 0.0f;
                lastPoint = currentPoint;
                currentPoint += 1;

                //GetComponent<SpriteRenderer>().color = GetColour();
            }

            transform.position = GetPosition();
        }

        //transform.localScale = new Vector3(initialScale.x * (1.0f + Mathf.PerlinNoise(Time.time, 0.0f) * 0.1f), initialScale.y * (1.0f + Mathf.PerlinNoise(0.0f, Time.time) * 0.1f), transform.localScale.z);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            lastPoint = 0;
            currentPoint = 1;
            currentT = 0.0f;

            //GetComponent<SpriteRenderer>().color = GetColour();
            transform.position = GetPosition();
        }
    }

    float GetDistance()
    {
        float currentDistance = 1.0f;

        if (points != null && lastPoint < points.childCount && currentPoint < points.childCount)
        {
            currentDistance = Vector3.Distance(points.GetChild(lastPoint).transform.position, points.GetChild(currentPoint).transform.position);
        }

        return currentDistance;
    }

    Vector3 GetPosition()
    {
        Vector3 currentPosition = transform.position;

        if (points != null && lastPoint < points.childCount && currentPoint < points.childCount)
        {
            currentPosition = Vector3.Lerp(points.GetChild(lastPoint).transform.position, points.GetChild(currentPoint).transform.position, currentT);
        }

        return currentPosition;
    }

    Color GetColour()
    {
        Color currentColour = GetComponent<SpriteRenderer>().color;

        if (points != null && currentPoint < points.childCount)
        {
            if (points.GetChild(currentPoint).name.ToLower().Contains("yellow"))
            {
                currentColour = Color.yellow;
            }

            if (points.GetChild(currentPoint).name.ToLower().Contains("cyan"))
            {
                currentColour = Color.cyan;
            }

            if (points.GetChild(currentPoint).name.ToLower().Contains("white"))
            {
                currentColour = Color.white;
            }
        }

        return currentColour;
    }
}
