using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Dummy : Enemy
{
    public override void OnDie()
    { 
        Destroy(this.gameObject);
    }
}
