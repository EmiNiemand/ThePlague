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
        GetComponent<SpriteRenderer>().sprite = spriteAfterDestruction;
        var colliders = GetComponents<Collider2D>();
        foreach (var collider in colliders)
        {
            collider.enabled = false;
        }
    }
}
