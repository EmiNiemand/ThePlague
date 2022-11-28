using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class AttackPattern : MonoBehaviour
{
	public Sprite weaponIcon;
    protected PlayerCombat combat;
	protected GamepadHaptics haptics;
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
		haptics.Attack();
	}

    protected void Setup()
    {
		combat = gameObject.GetComponentInParent<PlayerCombat>();
		haptics = GetComponentInParent<GamepadHaptics>();
		isOnCooldown = false;
    }
}
