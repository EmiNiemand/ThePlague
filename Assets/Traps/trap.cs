using UnityEngine;

public class trap : buttons
{
    private GameObject player;

    protected override void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col != null && col.CompareTag("Player"))
        {
            player.GetComponent<PlayerCombat>().OnReceiveDamage(Damage);
            this.OnDie();
        }
    }

    public override void OnDie() {
       Destroy(this.gameObject);
    }
}
