using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class AttackPattern : MonoBehaviour
{
	public Sprite weaponIcon;
    protected PlayerUpgrades upgrades;
	protected PlayerEvents playerEvents;
	public bool isOnCooldown;
	protected GameObject weaponInstance;
	[SerializeField] protected GameObject weaponPrefab;
    [SerializeField] protected int damage;
    [Range(0, 2.0f)]
    [SerializeField] protected float cooldownDuration;
	[HideInInspector] public WeaponIndicator weaponIndicator;

    public abstract IEnumerator StartPattern();
    protected abstract IEnumerator Cooldown();
	public virtual void WallHit() {}
    public void WeaponHitDetected(Enemy enemy)
	{
		enemy.OnReceiveDamage(damage);
		playerEvents.OnWeaponHit();
	}

    protected void Setup()
    {
		upgrades = gameObject.GetComponentInParent<PlayerUpgrades>();
		playerEvents = GetComponentInParent<PlayerEvents>();
		isOnCooldown = false;
    }
}
