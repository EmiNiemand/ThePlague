using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class APSpearBoomerangFusion : AttackPattern
{
	private Collider2D weaponCollider;
	private SpriteShapeRenderer weaponRenderer;

	[Range(0, 1.0f)]
	[SerializeField] private float attackDuration;

    private void Start()
	{
		Setup();

		GetComponent<WeaponHitDetect>().pattern = this;

		weaponCollider = GetComponent<PolygonCollider2D>();
		weaponRenderer = GetComponent<SpriteShapeRenderer>();

		weaponCollider.enabled = false;
		weaponRenderer.enabled = false;
	}

	public override IEnumerator StartPattern()
	{
		if(isOnCooldown) yield break;
        
		isOnCooldown = true;
        DashPlayer(0.5f);
		yield return new WaitForSeconds(0.1f);
        weaponCollider.enabled = true;
		weaponRenderer.enabled = true;

		yield return new WaitForSeconds(attackDuration - upgrades.attackSpeed);

        weaponCollider.enabled = false;
		weaponRenderer.enabled = false;

        DashPlayer(-0.5f);
        
		StartCoroutine(Cooldown());
	}

	protected override IEnumerator Cooldown()
	{
		yield return new WaitForSeconds(cooldownDuration);
		isOnCooldown = false;
	}
}
