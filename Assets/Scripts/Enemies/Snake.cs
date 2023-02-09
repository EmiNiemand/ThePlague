using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : Enemy
{
    private bool isAttackOnCooldown = false;

    // Start is called before the first frame update
    void Start()
    {
        base.Setup();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerCheck()) return;
        AwakeDistanceCheck(15);
    }

    void FixedUpdate()
    {
        if (!isAwake) return;
        if (isAttacking) return;
        if (!PlayerCheck()) return;

        Vector2 moveForce = (player.transform.position - transform.position).normalized * moveSpeed;
        if (distanceToPlayer > 5) rb2D.AddForce(moveForce, ForceMode2D.Impulse);

        if (isAttackOnCooldown) return;
        StartCoroutine(OnAttack());
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<PlayerEvents>().OnReceiveDamage(Damage);
        }
    }

    private IEnumerator OnAttack()
    {
        isAttacking = true;
        Vector2 destination = (player.transform.position - transform.position);
        rb2D.AddForce(destination.normalized * moveSpeed * 75, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.5f);
        isAttacking = false;
        StartCoroutine(OnAttackCooldown());
    }

    private IEnumerator OnAttackCooldown()
    {
        isAttackOnCooldown = true;
        yield return new WaitForSeconds(1.5f);
        isAttackOnCooldown = false;
    }

    public override void OnDie()
    {
        base.OnDie();
    }
}
