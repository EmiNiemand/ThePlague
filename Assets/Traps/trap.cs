using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class trap : buttons
    
{

    protected override void Start()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D col)

    {
        if (col.gameObject.CompareTag("Player") && col!= null)
        {
            col.GetComponent<PlayerCombat>().OnReceiveDamage(Damage);
            this.OnDie();
        }
    }
    public override void OnDie() {
       Destroy(this.gameObject);
    }
}
