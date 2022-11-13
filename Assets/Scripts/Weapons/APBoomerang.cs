using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class APBoomerang : AttackPattern
{
	private Vector3 cursorPos;
	private Camera mainCam;

	[Range(0.0f, 1.0f)]
	[SerializeField] private float speed;
	private bool wallHitDetected;

    private void Start()
	{
		Setup();

		mainCam = Camera.main;
		wallHitDetected = false;
	}

	public override IEnumerator StartPattern()
	{
		if(isOnCooldown) yield break;
        if(weaponInstance != null) yield break;

        StartCoroutine(Cooldown());

		weaponInstance = Instantiate(weaponPrefab, transform.position, Quaternion.identity);
		weaponInstance.GetComponent<WeaponHitDetect>().pattern = this;

		weaponIndicator.targetObject = weaponInstance;
		
		cursorPos = mainCam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
		cursorPos.z = 0;

		yield return new WaitUntil(()=>isOnPosition(cursorPos) || wallHitDetected);
		yield return new WaitUntil(()=>isOnPosition(transform.position));
		wallHitDetected = false;

		weaponIndicator.targetObject = null;
		Destroy(weaponInstance);
	}

	protected override IEnumerator Cooldown()
	{
		isOnCooldown = true;
		yield return new WaitUntil(()=>weaponInstance == null);
		yield return new WaitForSeconds(cooldownDuration);
		isOnCooldown = false;
	}

	
	private bool isOnPosition(Vector3 destination)
	{
		weaponInstance.transform.position = Vector3.MoveTowards(weaponInstance.transform.position, destination, speed);
		weaponInstance.transform.Rotate(0, 0, 100.0f);

		return Vector3.Distance(weaponInstance.transform.position, destination) < 0.1f;
	}

	public override void WallHit()
	{
		wallHitDetected = true;
	}
}
