using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Network : MonoBehaviour
{
    public Tree[] trees;
    public GameObject[] otherDancers;
    public Nutrient[] nutrients;
    bool networkComplete = false;
    Vector3 initialScale;

    private void Start()
    {
        initialScale = trees[0].transform.localScale;
    }

    private void Update()
    {
        if (networkComplete == false)
        {
            networkComplete = true;

            foreach (Tree tree in trees)
            {
                if (tree.CheckAllCompleted() == false)
                {
                    networkComplete = false;
                    break;
                }
            }

            if (networkComplete)
            {
                foreach (SpriteRenderer sprite in FindObjectsOfType<SpriteRenderer>())
                {
                    sprite.color = Color.green;
                }

                foreach (Nutrient nutrient in nutrients)
                {
                    nutrient.OverrideColour(Color.green);
                }
            }
        }
        else
        {
            foreach (Tree tree in trees)
            {
                tree.transform.localScale = new Vector3(initialScale.x * (1.0f + Mathf.PerlinNoise(Time.time, 0.0f) * 0.1f), initialScale.y * (1.0f + Mathf.PerlinNoise(0.0f, Time.time) * 0.2f), tree.transform.localScale.z);
            }

            foreach (GameObject dancer in otherDancers)
            {
                dancer.transform.localScale = new Vector3(dancer.transform.localScale.z * (1.0f + Mathf.PerlinNoise(Time.time, 0.0f) * 0.1f), dancer.transform.localScale.z * (1.0f + Mathf.PerlinNoise(0.0f, Time.time) * 0.2f), dancer.transform.localScale.z);
            }
        }
    }
}
