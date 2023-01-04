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
        if (col != null)
        {
            StartCoroutine(OnEnter(col));
        }
    }

    protected virtual IEnumerator OnEnter(Collider2D col)
    {
        yield break;
    }
}
