using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Rat : Enemy
{
    [SerializeField] private float moveSpeed;
    private GameObject player;
    private Rigidbody2D rb2D;

    private bool isAwake = false;
    private bool isAttacking = false;
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
        if (distance < 20)
        {
            isAwake = true;
        }
    }
    void FixedUpdate()
    {
        if (!isAwake) return;
        if (isAttacking) return;

        if (distance > 5) rb2D.AddForce((player.transform.position - transform.position).normalized * moveSpeed, ForceMode2D.Impulse);

        rb2D.AddForce((player.transform.position - transform.position).normalized * moveSpeed * 2, ForceMode2D.Impulse);
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col != null && col.gameObject.CompareTag("Player"))
        {
            if (!isAttacking) StartCoroutine(OnAttack(col.gameObject));
        }
    }

    private IEnumerator OnAttack(GameObject player)
    {
        isAttacking = true;
        player.GetComponent<PlayerCombat>().OnReceiveDamage(Damage);
        yield return new WaitForSeconds(1);
        isAttacking = false;
    }

    public override void OnDie()
    {
        Destroy(this.gameObject);
    }
}
