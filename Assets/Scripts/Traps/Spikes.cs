using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Spikes : Traps
{
    protected override IEnumerator OnEnter()
    {
        yield return StartCoroutine(OnCooldown());
    }

    private IEnumerator OnCooldown()
    {
        isOnCooldown = true;
        //TODO: add animation to spikes
        yield return new WaitForSeconds(cooldown);
        isOnCooldown = false;
    }
}
