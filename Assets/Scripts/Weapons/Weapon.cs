using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
	private AttackPattern pattern;

	[SerializeField] protected WeaponIndicator weaponIndicator;

	// Start is called before the first frame update
	void Start()
	{
        SetUp();
    }

	public void Attack()
	{
        if (pattern != null)
        {
            StartCoroutine(pattern.StartPattern());
        }
    }

    public void SetUp()
    {
        if (GetComponentInChildren<AttackPattern>() != null)
        {
            pattern = GetComponentInChildren<AttackPattern>();
            pattern.weaponIndicator = weaponIndicator;
        }
        else
        {
            pattern = null;
        }
    }
}
