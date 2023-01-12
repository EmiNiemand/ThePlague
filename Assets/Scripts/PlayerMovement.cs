using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration;

    private float dashTimer;
    private bool dashing;

    private Vector2 MoveAxis;
    Rigidbody2D Rigidbody;
    public bool isUsePressed = false;

    [SerializeField] private float MoveSpeed;

    private PlayerCombat playerCombat;


    // Start is called before the first frame update
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
        playerCombat = GetComponent<PlayerCombat>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (dashTimer > 0)
        {
            dashTimer -= Time.deltaTime;
            RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, Vector2.one, transform.rotation.z, MoveAxis, 1.5f);
            bool canMove = true;

            foreach (var hit in hits)
            {
                if (hit.collider.gameObject.CompareTag("Environment") || hit.collider.gameObject.CompareTag("Obstacles") 
                || hit.collider.gameObject.CompareTag("NPC") || hit.collider.gameObject.CompareTag("Doors"))
                {
                    canMove = false; break;
                }
            }
            if (!canMove)
            {
                GetComponent<PolygonCollider2D>().enabled = true;
                Rigidbody.AddForce(MoveAxis * dashSpeed, ForceMode2D.Impulse);
                return;
            }
            GetComponent<PolygonCollider2D>().enabled = false;
            Rigidbody.AddForce(MoveAxis * dashSpeed, ForceMode2D.Impulse); 
        }
        else
        {
            GetComponent<PolygonCollider2D>().enabled = true;
            dashing = false;
        }
        Rigidbody.AddForce(MoveAxis * (MoveSpeed + playerCombat.GetAdditionalMoveSpeed()), ForceMode2D.Impulse);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveAxis = context.ReadValue<Vector2>();
    }

    public void OnUse(InputAction.CallbackContext context)
    {
        if (context.started)
        {
                isUsePressed = true;
        }
        else if (context.canceled)
        {
            isUsePressed = false;
        }
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            {
                if (!dashing)
                {
                    Dash();
                }
            }
        }
    }

    private void Dash()
    {
        dashTimer = dashDuration;
        dashing = true;
    }


}
