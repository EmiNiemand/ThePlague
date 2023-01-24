using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    [SerializeField] private GameObject weaponPrefab;


    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerEvents>().OnPickupIndicator(true);
            if (other.GetComponent<PlayerEvents>().isUsePressed == true)
            {
                if(other.GetComponent<PlayerCombat>().EquipWeapon(weaponPrefab))
                    Destroy(gameObject);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerEvents>().OnPickupIndicator(false);
        }
    }
}
