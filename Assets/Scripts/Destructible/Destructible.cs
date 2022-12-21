using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    [SerializeField] private Sprite spriteAfterDestruction;

    public void OnDestroy()
    {
        var renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = spriteAfterDestruction;
        renderer.sortingOrder = 4;
        var colliders = GetComponents<Collider2D>();
        foreach (var collider in colliders)
        {
            collider.enabled = false;
        }
    }
}
