using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    [Header("Follow parameters")]
    [SerializeField] private Vector3 cameraOffset;
    [Range(0.0f, 1.0f)]
    [SerializeField] private float cameraFollowStrength;
    [Range(0.0f, 1.0f)]
    [SerializeField] private float cameraLerp;
    [Header("Border")]
    [SerializeField] private Vector2 mouseBorder = new Vector2(10, 10);

    private Transform target;

    private float dist;
    private float distLerp;
    private Vector3 newPos;
    private Vector3 mousePos;
    private Vector3 mouseVector;


    private void Start()
    {
        target = Camera.main.transform;
    }
    
    private void Update()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        mousePos.x -= this.transform.position.x;
        mousePos.y -= this.transform.position.y;

        if (mousePos.x < mouseBorder.x
            && mousePos.x > -mouseBorder.x)
            mouseVector.x = mousePos.x;
        else if (mousePos.x >= mouseBorder.x)
            mouseVector.x = mouseBorder.x;
        else if (mousePos.x <= -mouseBorder.x)
            mouseVector.x = -mouseBorder.x;

        if (mousePos.y < mouseBorder.y
            && mousePos.y > -mouseBorder.y)
            mouseVector.y = mousePos.y;
        else if (mousePos.y >= mouseBorder.y)
            mouseVector.y = mouseBorder.y;
        else if (mousePos.y <= -mouseBorder.y)
            mouseVector.y = -mouseBorder.y;

        newPos = Vector3.Lerp(this.transform.position, target.transform.position + mouseVector, cameraFollowStrength);
        target.transform.position = Vector3.Lerp(target.transform.position, newPos + cameraOffset, cameraLerp);
    }
}
