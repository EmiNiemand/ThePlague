using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    [SerializeField] private Sprite spriteAfterDestruction;
    [SerializeField] private GameObject resourceAfterDestruction;

    public void DestroyObject()
    {
        var renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = spriteAfterDestruction;
        renderer.sortingOrder = 4;

        var colliders = GetComponents<Collider2D>();
        foreach (var collider in colliders)
        {
            collider.enabled = false;
        }

        GameObject.Instantiate(resourceAfterDestruction, transform.position, Quaternion.identity);
    }
}
