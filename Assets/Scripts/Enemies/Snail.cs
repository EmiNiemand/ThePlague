using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snail : Enemy
{
    // Start is called before the first frame update
    void Start()
    {
        base.Setup();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerCheck()) return;
        AwakeDistanceCheck(25);
    }
    void FixedUpdate()
    {
        if (!isAwake) return;
        if (isAttacking) return;

        Vector2 moveForce = (player.transform.position - transform.position).normalized * moveSpeed;
        if (distanceToPlayer > 5) rb2D.AddForce(moveForce, ForceMode2D.Impulse);

        rb2D.AddForce(moveForce, ForceMode2D.Impulse);
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            if (!isAttacking) StartCoroutine(OnAttack(col.gameObject));
        }
    }

    private IEnumerator OnAttack(GameObject player)
    {
        isAttacking = true;
        player.GetComponent<PlayerEvents>().OnReceiveDamage(Damage);
        yield return new WaitForSeconds(1);
        isAttacking = false;
    }

    public override void OnDie()
    {
        base.OnDie();
    }
}
