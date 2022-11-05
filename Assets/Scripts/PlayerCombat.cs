using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
	GameObject playerSprite;
	Camera mainCam;
	Weapon playerWeapon;
	Vector3 cursorPos, lerpCursorPos;
	// Start is called before the first frame update
	void Start()
	{
		playerSprite = transform.GetChild(0).gameObject;

		mainCam = Camera.main;
		cursorPos = mainCam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
		lerpCursorPos = cursorPos;

		playerWeapon = GetComponentInChildren<Weapon>();
	}


	// Update is called once per frame
	void Update()
	{        
		cursorPos = mainCam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
		lerpCursorPos = Vector2.Lerp(lerpCursorPos, cursorPos, 0.04f);

		//Rotate player towards cursor
		playerSprite.transform.up = lerpCursorPos - playerSprite.transform.position;
	}

	public void OnAttack(InputAction.CallbackContext context)
	{
		playerWeapon.Attack();
	}
}
