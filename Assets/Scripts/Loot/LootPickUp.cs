using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootPickUp : MonoBehaviour
{

    // Accessed by Player script
    // -------------------------
    [HideInInspector] public int amount;

    // Set per instance
    // ----------------
    public ResourceType resourceType;
    public int minAmount = 5;
    public int maxAmount = 20;
    [Range(0, 100)]
    public int spawnProbability = 100;

    void Start()
    {
        amount = Random.Range(minAmount, maxAmount);
        if(Random.Range(0, 100) > spawnProbability) Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.tag);
        if(other.CompareTag("Player")) 
            if(other.GetComponent<PlayerResources>().ChangeResource(resourceType, amount))
                Destroy(this.gameObject);
    }
}
