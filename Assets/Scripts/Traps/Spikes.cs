using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spikes : Traps
{
    protected override IEnumerator OnEnter(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player") && !isOnCooldown)
        {
            col.gameObject.GetComponent<PlayerEvents>().OnReceiveDamage(damage);
        }
        
        else if(col.gameObject.CompareTag("Enemy") && !isOnCooldown)
        {
            col.gameObject.GetComponent<Enemy>().OnReceiveDamage(damage);
        }
        if(!isOnCooldown) yield return StartCoroutine(OnCooldown());
    }

    private IEnumerator OnCooldown()
    {
        isOnCooldown = true;
        //TODO: add animation to spikes
        yield return new WaitForSeconds(cooldown);
        isOnCooldown = false;
    }
}
