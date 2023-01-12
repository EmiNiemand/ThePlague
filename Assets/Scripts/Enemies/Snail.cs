using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;

public class Snail : Enemy
{
    [SerializeField] private float moveSpeed;
    private Rigidbody2D rb2D;
    private GameObject player;

    private bool isAwake = false;
    private bool isAttacking = false;
    private float distance;

    // Start is called before the first frame update
    protected override void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb2D = GetComponent<Rigidbody2D>();
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            return;
        }
        
        distance = Vector2.Distance(rb2D.transform.position, player.transform.position);
        if (distance < 25)
        {
            isAwake = true;
        }
        
    }
    void FixedUpdate()
    {
        if (!isAwake) return;
        if (isAttacking) return;

        if (distance > 5) rb2D.AddForce((player.transform.position - transform.position).normalized * moveSpeed, ForceMode2D.Impulse);

        rb2D.AddForce((player.transform.position - transform.position).normalized * moveSpeed, ForceMode2D.Impulse);
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
        Destroy(this);
    }
}
