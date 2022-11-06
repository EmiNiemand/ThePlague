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
            if (other.GetComponent<PlayerMovement>().isUsePressed == true)
            {
                if(other.GetComponent<PlayerCombat>().EquipWeapon(weaponPrefab))
                    Destroy(gameObject);
            }
        }
    }
}
