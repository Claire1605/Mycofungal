using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Junction : MonoBehaviour
{
    public Path previousPath;

    [Header("Must have equal number of junction paths and next paths and junction sprites")]
    public Path[] junctionPaths;
    public Path[] nextPaths;
    public Sprite[] junctionSprites;

    public int currentJunction = 0;
    
    void Start()
    {
        UpdateJunctions();
    }

    private void OnMouseDown()
    {
        SwitchJunction(currentJunction + 1);
    }

    void SwitchJunction(int junction)
    {
        currentJunction = junction;

        if (currentJunction >= junctionPaths.Length)
        {
            currentJunction = 0;
        }

        UpdateJunctions();
    }

    void UpdateJunctions()
    {
        if (currentJunction < junctionPaths.Length && currentJunction > 0)
        {
            previousPath.nextPath = junctionPaths[currentJunction];

            GetComponent<SpriteRenderer>().sprite = junctionSprites[currentJunction];

            for (int junction = 0; junction < junctionPaths.Length; junction++)
            {
                if (currentJunction == junction)
                {
                    junctionPaths[junction].SwitchOn();
                    junctionPaths[junction].nextPath = nextPaths[junction];
                    junctionPaths[junction].previousPath = previousPath;

                    nextPaths[junction].previousPath = junctionPaths[junction];
                }
                else
                {
                    junctionPaths[junction].SwitchOff();
                    junctionPaths[junction].nextPath = null;
                    junctionPaths[junction].previousPath = null;

                    nextPaths[junction].previousPath = null;
                }
            }
        }
        else
        {
            Debug.LogError("Incorrect number of junction paths or junction out of range: " + currentJunction, gameObject);
        }
    }
}
