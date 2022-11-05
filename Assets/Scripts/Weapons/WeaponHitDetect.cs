using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHitDetect : MonoBehaviour
{
	// set by pattern
	[HideInInspector] public AttackPattern pattern;

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.CompareTag("Enemy"))
		{
			pattern.WeaponHitDetected(other.gameObject.GetComponent<Enemy>());
		}
	}
}
