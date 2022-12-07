using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heal : buttons
{
    protected override void Start()
    {
    }


    private void OnTriggerEnter2D(Collider2D col)

            {
        if (col.gameObject.CompareTag("Player") && col != null)
        {
            col.GetComponent<PlayerCombat>().OnHeal(Damage);
            this.OnDie();
        }
    }

    public override void OnDie()
    {
        Destroy(this.gameObject);
    }
}
