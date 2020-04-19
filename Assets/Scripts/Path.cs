using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public Path nextPath;
    public Path previousPath;
    public bool pathOn = true;
    public Sprite offSprite;
    public Sprite onSprite;

    private void Awake()
    {
        if (onSprite == null && GetComponent<SpriteRenderer>())
        {
            onSprite = GetComponent<SpriteRenderer>().sprite;
        }
    }

    private void Start()
    {
        if (pathOn == false)
        {
            SwitchOff();
        }
    }

    public int GetPointCount()
    {
        return transform.childCount;
    }

    public Transform GetPoint(int point)
    {
        if (point < GetPointCount() && point >= 0)
        {
            return transform.GetChild(point);
        }

        return null;
    }

    public float GetDistanceBetweenPoints(int pointA, int pointB)
    {
        float distance = 1.0f;

        if (pointA < GetPointCount() && pointB < GetPointCount())
        {
            distance = Vector3.Distance(GetPoint(pointA).position, GetPoint(pointB).position);
        }

        return distance;
    }

    public Vector3 GetPathPosition(int pointA, int pointB, float t)
    {
        Vector3 position = Vector3.zero;

        if (pointA < GetPointCount() && pointB < GetPointCount())
        {
            position = Vector3.Lerp(GetPoint(pointA).position, GetPoint(pointB).position, t);
        }

        return position;
    }

    public Color GetPathColour(int point)
    {
        Color colour = Color.white;

        if (point < GetPointCount())
        {
            string pointName = GetPoint(point).name.ToLower();

            if (pointName.Contains("yellow"))
            {
                colour = new Color(255, 255, 0);
            }

            if (pointName.Contains("cyan"))
            {
                colour = Color.cyan;
            }

            if (pointName.Contains("white"))
            {
                colour = Color.white;
            }
        }

        return colour;
    }

    public bool GetPathOn()
    {
        return pathOn;
    }

    public void SwitchOn()
    {
        pathOn = true;

        if (GetComponent<SpriteRenderer>() && onSprite)
        {
            GetComponent<SpriteRenderer>().sprite = onSprite;
        }
    }

    public void SwitchOff()
    {
        pathOn = false;

        if (GetComponent<SpriteRenderer>() && offSprite)
        {
            GetComponent<SpriteRenderer>().sprite = offSprite;
        }
    }

    public void SetNextPath(Path path)
    {
        nextPath = path;
    }

    public void SetPreviousPath(Path path)
    {
        previousPath = path;
    }
}
