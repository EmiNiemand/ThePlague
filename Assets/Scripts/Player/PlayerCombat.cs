using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerCombat : MonoBehaviour
{
    public int HP { get;private set; }
    [field: SerializeField] public int maxHP { get; private set; }
    [SerializeField] private float cooldownTime;

    [HideInInspector] public UnityEvent onDie;

	private GameObject playerSprite;
	private Weapon playerWeapon;

    private bool isOnCooldown;
    private bool canAttack = true;
    private Vector3 cursorPos, lerpCursorPos;

    // Start is called before the first frame update
    void Awake()
    {
        HP = maxHP;
		playerSprite = transform.GetChild(0).gameObject;
		cursorPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
		lerpCursorPos = cursorPos;

		playerWeapon = GetComponentInChildren<Weapon>();
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

	public bool EquipWeapon(GameObject weaponPrefab)
	{
        return playerWeapon.EquipWeapon(weaponPrefab.name);
    }

	public void Attack() {
		if(canAttack) playerWeapon.Attack();
	}

	public bool SwitchWeapon()
	{
		return playerWeapon.SwitchWeapons();
	}

    public void Heal(int heal)
    {
        HP += heal;
        if (HP > maxHP)
            HP = maxHP;
    }

    public bool ReceiveDamage(int damage)
    {
        if (isOnCooldown) return false;
        
        StartCoroutine(DamageCooldown());
        HP -= damage;

        if(HP <= 0) onDie.Invoke();

        return true;
    }

    public void UpgradeHealth(int value)
    {
        HP += value;
        maxHP += value;
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

    public Vector2 GetPlayerDirection()
    {
        return playerSprite.transform.up;
    }

    public void SetCanAttack(bool bCanAttack)
    {
	    canAttack = bCanAttack;
    }
}
