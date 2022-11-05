using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackPattern : MonoBehaviour
{
	protected GamepadHaptics haptics;
	protected bool isOnCooldown;
	protected GameObject weaponInstance;
	[SerializeField] protected GameObject weaponPrefab;
    [SerializeField] protected int damage;

    public abstract IEnumerator StartPattern();
    protected abstract IEnumerator Cooldown();
    public void WeaponHitDetected(Enemy enemy)
	{
		enemy.OnReceiveDamage(damage);
		haptics.Attack();
	}

    protected void Setup()
    {
		haptics = GetComponentInParent<GamepadHaptics>();
		isOnCooldown = false;
    }
}
