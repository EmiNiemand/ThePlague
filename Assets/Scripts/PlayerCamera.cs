using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    public Transform target;
    private Vector3 camLerp;
    private Vector3 mousePos;
    private Vector3 mouseVector;

    [Range(0.0f, 1.0f)]
    [SerializeField] private float lerpValue;
    [SerializeField] private float mouseXBorder;
    [SerializeField] private float mouseYBorder;
    [SerializeField] private Vector3 cameraOffset;

    private void Start()
    {
        //cameraOffset = new Vector3(0, 0, -15);
    }

    private void Update()
    {
        mousePos = Mouse.current.position.ReadValue();
    }

    private void LateUpdate()
    {
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        mousePos.z = Camera.main.nearClipPlane;
        
        if (mousePos.x < mouseXBorder && mousePos.x > -mouseXBorder)
            mouseVector.x = mousePos.x;
        else if (mousePos.x >= mouseXBorder)
            mouseVector.x = mouseXBorder;
        else if (mousePos.x <= -mouseXBorder)
            mouseVector.x = -mouseXBorder;
        
        if (mousePos.y < mouseYBorder && mousePos.y > -mouseYBorder)
            mouseVector.y = mousePos.y;
        else if (mousePos.y >= mouseYBorder)
            mouseVector.y = mouseYBorder;
        else if (mousePos.y <= -mouseYBorder)
            mouseVector.y = -mouseYBorder;
        
        camLerp = Vector3.Lerp(this.transform.position, target.transform.position + mouseVector, lerpValue);
        target.transform.position = camLerp + cameraOffset;
    }
}
