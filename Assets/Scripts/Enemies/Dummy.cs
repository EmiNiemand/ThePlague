using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Dummy : Enemy
{
    private SpriteRenderer Renderer;
    private PolygonCollider2D Collider;

    // Start is called before the first frame update
    void Start()
    {
        Renderer = GetComponent<SpriteRenderer>();
        Collider = GetComponent<PolygonCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void OnDie()
    { 
        Destroy(this.gameObject);
    }
}
