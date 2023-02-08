using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class APScytheSpearFusion : AttackPattern
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
        DashPlayer(0.3f);
		yield return new WaitForSeconds(0.1f);
        weaponCollider.enabled = true;
		weaponRenderer.enabled = true;

		yield return new WaitForSeconds(attackDuration - upgrades.attackSpeed);

        weaponCollider.enabled = false;
		weaponRenderer.enabled = false;
        
		StartCoroutine(Cooldown());
	}

	protected override IEnumerator Cooldown()
	{
		yield return new WaitForSeconds(cooldownDuration);
		isOnCooldown = false;
	}
}
