using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.InputSystem;

[System.Serializable]
public struct WeaponList
{
	public string name;
	public GameObject weaponPrefab;
}

public class Weapon : MonoBehaviour
{
	//temp
	public string firstWeapon;
	public string secondWeapon;
	public AttackPattern equippedWeapon;
	[SerializeField] private List<WeaponList> weapons;
	[SerializeField] protected WeaponIndicator weaponIndicator;

	// Start is called before the first frame update
	void Start()
	{
		SelectWeapon(firstWeapon, secondWeapon);
    }

	public void Attack()
	{
        if (equippedWeapon != null)
        {
            StartCoroutine(equippedWeapon.StartPattern());
        }
    }

	private void SelectWeapon(string firstWeapon, string secondWeapon)
	{
		string fusionName = firstWeapon + secondWeapon;
		Debug.Log(fusionName);
		foreach (WeaponList weapon in weapons)
		{
			if(weapon.name == fusionName)
			{
				equippedWeapon = Instantiate(weapon.weaponPrefab, transform.position, Quaternion.identity, transform).GetComponent<AttackPattern>();
				equippedWeapon.weaponIndicator = weaponIndicator;
			}
		}
	}
}
