using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

/** 
    <summary>
        <para>This class is responsible for handling interaction
        between Player classes and any other class</para>

        <para>It's supposed to react to external event, like 
        receiving input or taking damage, and invoke appropiate 
        functions </para> 

        <para>For example, when player takes damage, this class
        will activate ReceiveDamage method in PlayerCombat,
        but also update UI according to new HP amount</para>

        <para>This way, it gets easier to implement additional
        effects/feedback for specific events and every Player's
        class is responsible for its own stuff</para>
    </summary>
**/
public class PlayerEvents : MonoBehaviour
{
    // Player scripts
    // --------------
    private PlayerCombat playerCombat;
    private PlayerMovement playerMovement;
    private PlayerUpgrades playerUpgrades;
    private PlayerResources playerResources;
    private GamepadHaptics haptics;

    // UI
    // --
    private PlayerUI playerUI;
	private GameUI gameUI;

    // Class variables
    // ---------------
    public bool isUsePressed = false;
    public GameObject damageIndicator;
    // Pass-through event
    public UnityEvent onDie;


    void Start()
    {
        playerCombat = GetComponent<PlayerCombat>();
        playerMovement = GetComponent<PlayerMovement>();
        playerUpgrades = GetComponent<PlayerUpgrades>();
        playerResources = GetComponent<PlayerResources>();
        haptics = GetComponent<GamepadHaptics>();

		playerUI = GetComponentInChildren<PlayerUI>();
		gameUI = GameObject.FindObjectOfType<GameUI>();

        playerCombat.onDie.AddListener(OnDie);
        gameUI.SetMaxHP(playerCombat.maxHP);
    }

    public void OnPickupIndicator(bool activate)
    {
        
    }

	public void OnEquipWeapon(GameObject weaponPrefab)
	{
		if(playerCombat.EquipWeapon(weaponPrefab))
			gameUI.EquipWeapon(weaponPrefab.GetComponent<AttackPattern>().weaponIcon);
    }

    public void OnUpgradeMoveSpeed(float value) { playerUpgrades.AddMoveSpeed(value); }
    public void OnUpgradeAttackSpeed(float value) { playerUpgrades.AddAttackSpeed(value); }

    public void OnWeaponHit() { haptics.Attack(); }
    public void OnDie() { onDie.Invoke(); }

    #region Input events

    // Events class
    // ------------
    public void OnUse(InputAction.CallbackContext context)
    {
        if (context.started)
            isUsePressed = true;
        else if (context.canceled)
            isUsePressed = false;
    }

    // Movement
    // --------
    public void OnDash(InputAction.CallbackContext context)
    {
        if(context.started) playerMovement.Dash();
    }

    //TODO: improve, this is a duct tape solution
    public void ForceDash(float scaler = 1) { playerMovement.ForceDash(scaler, playerCombat.GetPlayerDirection()); }

    public void OnMove(InputAction.CallbackContext context)
    {
        playerMovement.moveAxis = context.ReadValue<Vector2>();
    }

    // Combat
    // ------
    public void OnAttack(InputAction.CallbackContext context)
	{
		playerCombat.Attack();
	}

    public void OnSwitchWeapon(InputAction.CallbackContext context)
	{
		if(playerCombat.SwitchWeapon()) gameUI.SwitchWeapons();
	}

    public void OnReceiveDamage(int damage)
    {
        if(playerCombat.ReceiveDamage(damage))
        {
            gameUI.UpdateHP(playerCombat.HP);
            GameObject damageInstance = GameObject.Instantiate(damageIndicator, transform.position, Quaternion.identity);
            damageInstance.GetComponent<DamageIndicator>().ShowDamage(damage, Color.red);
        }
    }

    // Resources
    // ---------
    public bool OnResourceChange(ResourceType resourceType, int amount)
    {
        if(playerResources.ChangeResource(resourceType, amount))
        {
            switch(resourceType)
            {
                case ResourceType.Coins:
                    gameUI.UpdateCoins(playerResources.resourceAmount[ResourceType.Coins]);
                    break;
                default: break;
            }
            return true;
        }
        return false;
    }

    // Upgrades
    // --------
    public bool OnFullHeal(int price)
    {
        if(OnResourceChange(ResourceType.Coins, -price))
        {
            playerCombat.Heal(playerCombat.maxHP - playerCombat.HP);
            gameUI.UpdateHP(playerCombat.HP);
            return true;
        }
        return false;
    }

    public bool OnPlayerSpeedUpgrade(int value, int price)
    {
        Debug.Log("YEEEEEEEEEEEEEEEET");
        if(OnResourceChange(ResourceType.Coins, -price))
        {
        Debug.Log("lolololololol");
            OnUpgradeMoveSpeed(value);
            return true;
        }
        return false;
    }

    // Other
    // -----
    public void OnReset(InputAction.CallbackContext context)
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

    public void OnQuit(InputAction.CallbackContext context)
    {
        Application.Quit(0);
    }
    #endregion
}
