using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEntry : MonoBehaviour
{
    private GameObject _doors;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _doors = GameObject.FindGameObjectWithTag("Doors");
            _doors.SetActive(true);
        }
    }

}
