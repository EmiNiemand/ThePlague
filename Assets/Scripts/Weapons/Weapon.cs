using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
	private AttackPattern pattern;

	// Start is called before the first frame update
	void Start()
	{
		pattern = GetComponentInChildren<AttackPattern>();
	}

	public void Attack()
	{
		StartCoroutine(pattern.StartPattern());
	}
}
