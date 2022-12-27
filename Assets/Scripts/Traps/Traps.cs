using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Traps : MonoBehaviour
{
    [SerializeField] private int damage;
    
    // Start is called before the first frame update
    protected virtual void Start()
    {
        gameObject.tag = "Trap";
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if(col != null && col.gameObject.CompareTag("Player"))
        {
            StartCoroutine(OnDamage());
            col.gameObject.GetComponent<PlayerCombat>().OnReceiveDamage(damage);
        }
        
        else if(col != null && col.gameObject.CompareTag("Enemy"))
        {
            StartCoroutine(OnDamage());
            col.gameObject.GetComponent<Enemy>().OnReceiveDamage(damage);
        }
    }

    protected abstract IEnumerator OnDamage();
}
