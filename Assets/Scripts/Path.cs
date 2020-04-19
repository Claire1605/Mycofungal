using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public Path nextPath;
    public Path previousPath;
    public Transform pathPoints;
    public bool pathOn = true;

    public int GetPointCount()
    {
        return pathPoints.childCount;
    }

    public Transform GetPoint(int point)
    {
        if (point < GetPointCount() && point >= 0)
        {
            return pathPoints.GetChild(point);
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
                colour = Color.yellow;
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
        gameObject.SetActive(true);
    }

    public void SwitchOff()
    {
        pathOn = false;
        gameObject.SetActive(false);
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
