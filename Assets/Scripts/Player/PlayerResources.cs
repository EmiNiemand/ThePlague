using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceType {
    Coins
}

public class PlayerResources : MonoBehaviour
{
    public Dictionary<ResourceType, int> resourceAmount { get; private set;}

    void Start()
    {
        resourceAmount = new Dictionary<ResourceType, int>();
        foreach(ResourceType resource in System.Enum.GetValues(typeof(ResourceType)))
        {
            resourceAmount.Add(resource, 0);
        }
    }

    public bool ChangeResource(ResourceType resourceType, int amount) 
    {
        if(amount < 0 && resourceAmount[resourceType] < -amount) return false;

        resourceAmount[resourceType] += amount;
        return true;
    }
}
