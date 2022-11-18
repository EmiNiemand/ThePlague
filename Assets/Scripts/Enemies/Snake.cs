using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class Snake : Enemy
{
    [SerializeField] private float moveSpeed;
    private Rigidbody2D rb2D;
    private GameObject player;

    private bool isAwake = false;
    private bool isAttacking = false;
    private bool isAttackOnCooldown = false;
    private float distance;

    // Start is called before the first frame update
    protected override void Start()
    {
        player = GameObject.Find("Player");
        rb2D = GetComponent<Rigidbody2D>();
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance < 15)
        {
            isAwake = true;
        }
    }
    void FixedUpdate()
    {
        if (!isAwake) return;
        if (isAttacking) return;

        if (distance > 5) rb2D.AddForce((player.transform.position - transform.position).normalized * moveSpeed, ForceMode2D.Impulse);

        if (isAttackOnCooldown) return;
        StartCoroutine(OnAttack());
    }

    void OnCollisionStay2D(Collision2D col)
    {
        if (col != null && col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<PlayerCombat>().OnReceiveDamage(Damage);
        }
    }

    private IEnumerator OnAttack()
    {
        isAttacking = true;
        Vector2 destination = (player.transform.position - transform.position);
        rb2D.AddForce(destination.normalized * moveSpeed * 100, ForceMode2D.Impulse);
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
        Destroy(this.gameObject);
    }
}
