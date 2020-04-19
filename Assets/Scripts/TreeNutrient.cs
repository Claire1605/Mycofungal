using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeNutrient : MonoBehaviour
{
    public bool completed = false;
    public Sprite empty;
    public Sprite filled;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void CompleteNutrient()
    {
        completed = true;
        spriteRenderer.sprite = filled;
    }
}
