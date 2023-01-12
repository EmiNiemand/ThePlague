using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerCombat : MonoBehaviour
{
	private Vector3 cursorPos, lerpCursorPos;
	private GameObject playerSprite;
	private Weapon playerWeapon;
	private GameUI gameUI;
    private PlayerUI playerUI;

    private float additionalMoveSpeed = 0;
    private float additionalAttackSpeed = 0;

    private int HP;
    [SerializeField] private int maxHP;
    [SerializeField] private float cooldownTime;
    private bool isOnCooldown;

    private bool canAttack = true;


    // Start is called before the first frame update
    void Start()
    {
        HP = maxHP;
		playerSprite = transform.GetChild(0).gameObject;
		cursorPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
		lerpCursorPos = cursorPos;

		playerWeapon = GetComponentInChildren<Weapon>();
		gameUI = GameObject.Find("_GameUI").GetComponent<GameUI>();
        gameUI.SetMaxHP(maxHP);
		playerUI = GetComponentInChildren<PlayerUI>();
    }


	// Update is called once per frame
	void Update()
    {
        cursorPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
		lerpCursorPos = Vector2.Lerp(lerpCursorPos, cursorPos, 0.04f);

		//Rotate player towards cursor
		playerSprite.transform.up = lerpCursorPos - playerSprite.transform.position;
		GetComponent<Collider2D>().transform.up = lerpCursorPos - playerSprite.transform.position;
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
		if (canAttack) playerWeapon.Attack();
	}

	public void OnSwitchWeapon(InputAction.CallbackContext context)
	{
		if(playerWeapon.SwitchWeapons())
			gameUI.SwitchWeapons();
	}

	public void OnReset(InputAction.CallbackContext context)
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

    public void OnQuit(InputAction.CallbackContext context)
    {
        Application.Quit(0);
    }

    public void OnHeal(int heal)
    {
        if (HP + heal > maxHP)
        {
            HP = maxHP;
        }
        else
        {
            HP += heal;
        }
    }

    public void OnReceiveDamage(int damage)
    {
        if (isOnCooldown) return;
        
        StartCoroutine(DamageCooldown());

        HP -= damage;
        gameUI.UpdateHP(HP);
    }

    public void OnUpgradeHealth(int value)
    {
        HP += value;
        maxHP += value;
    }

    public void OnUpgradeMoveSpeed(float value)
    {
        additionalMoveSpeed += value;
    }

    public void OnUpgradeAttackSpeed(float value)
    {
        additionalAttackSpeed += value;
    }

    private IEnumerator DamageCooldown()
    {
        //temp
        SpriteRenderer spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        isOnCooldown = true;

        //temp
        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(cooldownTime);

        //temp
        spriteRenderer.color = Color.white;

        isOnCooldown = false;
    }

    public float GetAdditionalMoveSpeed()
    {
        return additionalMoveSpeed;
    }

    public float GetAdditionalAttackSpeed()
    {
        return additionalAttackSpeed;
    }

    public int GetHP()
    {
        return HP;
    }

    public void SetCanAttack(bool bCanAttack)
    {
	    canAttack = bCanAttack;
    }
}
