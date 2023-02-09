using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Weapon : MonoBehaviour
{
	//temp
	private string firstWeapon;
	private string secondWeapon;
	public AttackPattern equippedWeapon;
	[SerializeField] private List<GameObject> weapons;
	[SerializeField] private WeaponIndicator weaponIndicator;

	// Start is called before the first frame update
	void Start()
	{
		firstWeapon = "";
		secondWeapon = "";
    }

	public void Attack()
	{
        if (equippedWeapon != null)
        {
            StartCoroutine(equippedWeapon.StartPattern());
        }
    }

	private void SelectWeapon()
	{
		if(equippedWeapon != null)
		{
			Destroy(equippedWeapon.gameObject);
			equippedWeapon = null;
		}

		string fusionName = firstWeapon + secondWeapon;
		foreach (GameObject weapon in weapons)
		{
			if(weapon.name == fusionName)
			{
				equippedWeapon = Instantiate(weapon, transform).GetComponent<AttackPattern>();
				equippedWeapon.weaponIndicator = weaponIndicator;
				return;
			}
		}
	}

	public bool EquipWeapon(string weaponName)
	{
		if(weaponName.Equals(firstWeapon) || weaponName.Equals(secondWeapon))
			return false;

		if(firstWeapon == "")
		{
			firstWeapon = weaponName;
		}
		else if(secondWeapon == "")
		{
			secondWeapon = weaponName;
		}
		else
		{
			secondWeapon = firstWeapon;
			firstWeapon = weaponName;
		}
		
		SelectWeapon();

		return true;
	}

	public bool SwitchWeapons()
	{
		if(firstWeapon == "" || secondWeapon == "" || equippedWeapon.isOnCooldown) return false;

		string temp = secondWeapon;
		secondWeapon = firstWeapon;
		firstWeapon = temp;
		SelectWeapon();

		return true;
	}
}
