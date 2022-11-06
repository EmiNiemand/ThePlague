using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    [SerializeField] private GameObject weaponPrefab;
 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<PlayerMovement>().isUsePressed == true)
            {
                other.GetComponent<PlayerCombat>().EquipWeapon(weaponPrefab);
                Destroy(gameObject);
            }
        }
    }
}
