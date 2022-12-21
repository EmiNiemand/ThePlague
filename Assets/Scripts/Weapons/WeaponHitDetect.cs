using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Bson;
using UnityEngine;

public class WeaponHitDetect : MonoBehaviour
{
	// set by pattern
	[HideInInspector] public AttackPattern pattern;

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            pattern.WeaponHitDetected(other.gameObject.GetComponent<Enemy>());
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Environment"))
        {
            pattern.WallHit();
        }
        else if(other.CompareTag("Destructible"))
        {
            other.gameObject.GetComponent<Destructible>().OnDestroy();
        }
    }
}
