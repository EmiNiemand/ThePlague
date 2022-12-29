using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental;
using UnityEngine;

public abstract class Traps : MonoBehaviour
{
    [SerializeField] protected int damage;
    [SerializeField] protected float cooldown;
    [SerializeField] protected float attackTime;
    protected bool isOnCooldown;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        isOnCooldown = false;
        gameObject.tag = "Trap";
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player") && isOnCooldown == false)
        {
            StartCoroutine(OnEnter());
            col.gameObject.GetComponent<PlayerCombat>().OnReceiveDamage(damage);
        }
        
        else if(col.gameObject.CompareTag("Enemy") && isOnCooldown == false)
        {
            StartCoroutine(OnEnter());
            col.gameObject.GetComponent<Enemy>().OnReceiveDamage(damage);
        }
    }

    protected virtual IEnumerator OnEnter()
    {
        yield break;
    }
}
