using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    private Transform target;

    private float dist;
    private float distLerp;
    private Vector3 newPos;
    private Vector3 mousePos;
    private Vector3 mouseVector;

    [SerializeField] private Vector3 cameraOffset;
    [Range(0.0f, 1.0f)]
    [SerializeField] private float cameraFollowStrength;
    [Range(0.0f, 1.0f)]
    [SerializeField] private float cameraLerp;
    [SerializeField] private float mouseXBorder;
    [SerializeField] private float mouseYBorder;
    // Update is called once per frame

    private void Start()
    {
        target = Camera.main.transform;
    }

    private void Update()
    {
        mousePos = Mouse.current.position.ReadValue();
    }

    private void LateUpdate()
    {
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);
        // mousePos.z = Camera.main.nearClipPlane; 

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

        /*dist = Vector3.Distance(mousePos, target.position);

        if (dist >= 1.0f)
            distLerp = (1.0f / dist) * mouseLerp;
        else if (dist > 0.0f)
            distLerp = mouseLerp;

        newPos = Vector3.Lerp(this.transform.position, target.transform.position + mouseVector, cameraLerp - distLerp);
        target.transform.position = newPos + cameraOffset;*/

        newPos = Vector3.Lerp(this.transform.position, target.transform.position + mouseVector, cameraFollowStrength);
        target.transform.position = Vector3.Lerp(target.transform.position, newPos + cameraOffset, cameraLerp);
    }
}
