using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private GameObject pickUpIndicator;

    // Start is called before the first frame update
    void Start()
    {
        pickUpIndicator = transform.Find("PickUpIndicator").gameObject;
        pickUpIndicator.active = false;
    }

    public void OnPickUpIndicator(bool activate)
    {
        pickUpIndicator.active = activate;
    }
}