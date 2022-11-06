using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public Image firstWeaponImage;
    public Image secondWeaponImage;
    
    public void EquipWeapon(Sprite weaponImage)
    {
        if(firstWeaponImage.sprite != null) {
            secondWeaponImage.sprite = firstWeaponImage.sprite;
            secondWeaponImage.color = Color.white;
        }
        firstWeaponImage.sprite = weaponImage;
        firstWeaponImage.color = Color.white;
    }

    public void SwitchWeapons()
    {
        Sprite temp = secondWeaponImage.sprite;
        secondWeaponImage.sprite = firstWeaponImage.sprite;
        firstWeaponImage.sprite = temp;
    }
}

