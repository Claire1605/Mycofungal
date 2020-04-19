using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    public List<TreeNutrient> nutrientsNeeded = new List<TreeNutrient>();

    private bool allCompleted = false;

    private AudioSource treeSound;

    private void Start()
    {
        treeSound = GetComponent<AudioSource>();
    }

    public bool CheckNutrientsNeeded(Nutrient nutrient)
    {
        bool needed = false;

        for (int i = 0; i < nutrientsNeeded.Count; i++)
        {
            if (nutrientsNeeded[i].completed == false)
            {
                needed = true;
                nutrientsNeeded[i].CompleteNutrient();
                treeSound.Play();
                break;
            }
        }

        // Check if all nutrients completed
        if (allCompleted == false)
        {
            allCompleted = true;

            for (int i = 0; i < nutrientsNeeded.Count; i++)
            {
                if (nutrientsNeeded[i].completed == false)
                {
                    allCompleted = false;
                    break;
                }
            }

            if (allCompleted)
            {
                treeSound.loop = true;

                if (treeSound.isPlaying == false)
                {
                    treeSound.Play();
                }
            }
        }

        return needed;
    }

    public bool CheckAllCompleted()
    {
        return allCompleted;
    }
}
