using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class buttons : MonoBehaviour
{
    [SerializeField] protected int Damage;
    [SerializeField] protected int Uses;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        this.gameObject.tag = "Button";
    }

    public abstract void OnDie();


}
