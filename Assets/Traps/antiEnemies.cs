using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class antiEnemies : buttons
{

    protected override void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D col)

    {
        if (col.gameObject.CompareTag("Player") && col != null)
        {
            col.GetComponent<Enemy>().OnReceiveDamage(Damage);
            this.OnDie();
        }
    }
    public override void OnDie()
    {
        Destroy(this.gameObject);
    }
}