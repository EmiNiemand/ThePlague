using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public Image firstWeaponImage;
    public Image secondWeaponImage;
    
    //TODO: improve this spaghetti
    public void EquipWeapon(Sprite weaponImage)
    {
        if(firstWeaponImage.sprite == null) 
        {
            firstWeaponImage.sprite = weaponImage;
            firstWeaponImage.color = Color.white;
        }
        else if(secondWeaponImage.sprite == null)
        {
            secondWeaponImage.sprite = weaponImage;
            secondWeaponImage.color = Color.white;
        }
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
}

