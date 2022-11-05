using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class APBoomerang : AttackPattern
{
    private Vector3 cursorPos;
    private Camera mainCam;
    
    private void Start()
    {
        mainCam = Camera.main;
        
    }

    public override IEnumerator StartPattern()
    {
        cursorPos = mainCam.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        yield return new WaitUntil(()=>isOnPosition(cursorPos));

    }
    
    bool isOnPosition(Vector3 destination)
    {
        transform.position = Vector3.Lerp(transform.position, destination, 0.2f);
        return Vector3.Distance(transform.position, destination) < 0.1f;
    }
}
