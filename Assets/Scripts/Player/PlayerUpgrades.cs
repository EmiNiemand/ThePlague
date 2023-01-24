using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgrades : MonoBehaviour
{
    public float moveSpeed {get; private set;}
    public float attackSpeed {get; private set;}

    public PlayerUpgrades() {moveSpeed=0; attackSpeed=0;}
    
    public void AddMoveSpeed(float amount) 
    {
        moveSpeed += amount;
    }
    public void AddAttackSpeed(float amount) 
    {
        attackSpeed += amount;
    }
}
