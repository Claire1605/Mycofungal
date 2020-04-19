using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public Path nextPath;
    public Path previousPath;
    public Transform pathPoints;

    public int GetPointCount()
    {
        return pathPoints.childCount;
    }

    public Transform GetPoint(int point)
    {
        if (point < GetPointCount() && point > 0)
        {
            return pathPoints.GetChild(point);
        }

        return null;
    }
}
