using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Rat : Enemy
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
        AwakeDistanceCheck(30);
    }
    void FixedUpdate()
    {
        if (!isAwake) return;
        if (isAttacking) return;

        if (distanceToPlayer > 5) MoveTowardsPlayer(moveSpeed);
        MoveTowardsPlayer(moveSpeed * 2);
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
