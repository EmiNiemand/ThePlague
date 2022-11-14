using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Indicates object position when targetObject is not null
public class WeaponIndicator : MonoBehaviour
{
    [HideInInspector] public GameObject targetObject;
    private RawImage image;

    void Start()
    {
        image = GetComponent<RawImage>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(targetObject != null) 
        {
            image.enabled = true;
            transform.up = targetObject.transform.position - transform.parent.position;
        }
        else
        {
            image.enabled = false;
        }
    }
}
