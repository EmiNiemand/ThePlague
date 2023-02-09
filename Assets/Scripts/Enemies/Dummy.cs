using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Dummy : Enemy
{
    void Start()
    {
        base.Setup();
    }

    public override void OnDie()
    { 
        Destroy(this.gameObject);
    }
}
