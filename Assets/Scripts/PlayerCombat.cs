using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
	private Camera mainCam;
	private Vector3 cursorPos, lerpCursorPos;
	private GameObject playerSprite;
	private Weapon playerWeapon;
	private GameUI gameUI;
    private PlayerUI playerUI;

	// Start is called before the first frame update
	void Start()
	{
		playerSprite = transform.GetChild(0).gameObject;

		mainCam = Camera.main;
		cursorPos = mainCam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
		lerpCursorPos = cursorPos;

		playerWeapon = GetComponentInChildren<Weapon>();
		gameUI = GameObject.Find("_GameUI").GetComponent<GameUI>();
		playerUI = GetComponentInChildren<PlayerUI>();
	}


	// Update is called once per frame
	void Update()
	{        
		cursorPos = mainCam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
		lerpCursorPos = Vector2.Lerp(lerpCursorPos, cursorPos, 0.04f);

		//Rotate player towards cursor
		playerSprite.transform.up = lerpCursorPos - playerSprite.transform.position;
	}

    public void PickUpIndicator(bool activate)
    {
		playerUI.OnPickUpIndicator(activate);
    }

	public bool EquipWeapon(GameObject weaponPrefab)
	{
		if(playerWeapon.EquipWeapon(weaponPrefab.name))
		{
			gameUI.EquipWeapon(weaponPrefab.GetComponent<AttackPattern>().weaponIcon);
			return true;
		}
		return false;
    }

	public void OnAttack(InputAction.CallbackContext context)
	{
		playerWeapon.Attack();
	}

	public void OnSwitchWeapon(InputAction.CallbackContext context)
	{
		if(playerWeapon.SwitchWeapons())
			gameUI.SwitchWeapons();
	}
}
