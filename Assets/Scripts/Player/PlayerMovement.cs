using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Dash")]
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashDuration;
    [Header("Move")]
    [SerializeField] private float moveSpeed;

    public Vector2 moveAxis;

    private float dashTimer;
    private bool dashing;
    private List<string> dashStoppersTags = new List<string>() {
        "Environment", "Obstacles", "NPC", "Doors"};

    private Rigidbody2D playerRigidbody;
    private PlayerUpgrades playerUpgrades;


    void Start()
    {
        playerRigidbody = gameObject.GetComponent<Rigidbody2D>();
        playerUpgrades = gameObject.GetComponent<PlayerUpgrades>();
        moveAxis = new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 scaledMoveAxis = moveAxis * Time.deltaTime * 10;

        // Move player
        // -----------
        playerRigidbody.AddForce(
            scaledMoveAxis * (moveSpeed + playerUpgrades.moveSpeed), 
            ForceMode2D.Impulse);

        // Dash logic
        // ----------
        // TODO: improve this piece of code
        if (dashTimer > 0)
        {
            dashTimer -= Time.deltaTime;

            RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, Vector2.one, transform.rotation.z, moveAxis, 1.5f);
            bool canMove = true;

            foreach (var hit in hits)
            {
                if (dashStoppersTags.Contains(hit.collider.tag))
                {
                    canMove = false; break;
                }
            }
            playerRigidbody.AddForce(scaledMoveAxis * dashSpeed, ForceMode2D.Impulse);

            if (!canMove) { dashTimer = 0; return; }
        }
        else if(dashing)
        {
            GetComponent<PolygonCollider2D>().enabled = true;
            GetComponentInChildren<SpriteRenderer>().color = GetComponentInChildren<SpriteRenderer>().color * new Vector4(1, 1, 1, 5);
            dashing = false;
        }
    }

    public void Dash()
    {
        if(dashing) return;

        dashTimer = dashDuration;
        //TODO: move it outside maybe
        GetComponent<PolygonCollider2D>().enabled = false;
        GetComponentInChildren<SpriteRenderer>().color = GetComponentInChildren<SpriteRenderer>().color * new Vector4(1, 1, 1, 0.2f);
        dashing = true;
    }
}
