using UnityEngine;

public class antiEnemies : buttons
{
    private GameObject enemy;
    private GameObject player;

    protected override void Start()
    {
        enemy = GameObject.Find("Enemy");
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerEnter2D(Collider2D col)

    {
        if (col != null && col.CompareTag("Player"))
        {
            enemy.GetComponent<Enemy>().OnReceiveDamage(Damage);
            this.OnDie();
        }
    }
    
    public override void OnDie()
    {
        Destroy(this.gameObject);
    }
}