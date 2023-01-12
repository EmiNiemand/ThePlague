using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
    private int hpAmount;

    [Header("Weapons")]
    [Space(2)]
    [SerializeField] private Image firstWeaponImage;
    [SerializeField] private Image secondWeaponImage;
    [Header("HP")]
    [Space(2)]
    [SerializeField] private Slider hpBar;
    [SerializeField] private TextMeshProUGUI hpBarText;
    [Header("Score")]
    [Space(2)]
    [SerializeField] private TextMeshProUGUI floorCounterText;
    
    //TODO: improve this spaghetti
    public void EquipWeapon(Sprite weaponImage)
    {
        // Equip weapon at the first slot
        if(firstWeaponImage.sprite == null) 
        {
            firstWeaponImage.sprite = weaponImage;
            firstWeaponImage.color = Color.white;
        }
        // Equip weapon at the second slot
        else if(secondWeaponImage.sprite == null)
        {
            secondWeaponImage.sprite = weaponImage;
            secondWeaponImage.color = Color.white;
        }
        // When inventory full move first weapon to
        //   second position and equip new weapon
        //   at first
        else
        {
            secondWeaponImage.sprite = firstWeaponImage.sprite;
            firstWeaponImage.sprite = weaponImage;
        }
    }

    public void SwitchWeapons()
    {
        Sprite temp = secondWeaponImage.sprite;
        secondWeaponImage.sprite = firstWeaponImage.sprite;
        firstWeaponImage.sprite = temp;
    }

    public void SetMaxHP(int maxHP) 
    { 
        hpBar.maxValue = maxHP; 
        hpBar.value = maxHP;
        UpdateHPText();
    }

    public void UpdateHP(int currentHP)
    {
        hpBar.value = currentHP;
        UpdateHPText();
    }

    public void UpdateFloorCounter(int currentFloor)
    {
        if(currentFloor > 0)
            floorCounterText.text = "FLOOR "+currentFloor;
        else
            floorCounterText.text = "";
    }

    // Helper functions
    // ----------------

    private void UpdateHPText()
    {
        hpBarText.text = hpBar.value+" / "+hpBar.maxValue;
    }
}
