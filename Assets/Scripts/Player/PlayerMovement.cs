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
    private Vector2 dashAxis;

    private float dashTimer;
    // Decides how long after dash player stays invincible
    private float dashInvicibilityTime;
    private bool dashing;
    private List<string> dashStoppersTags = new List<string>() {
        "Environment", "Obstacles", "NPC", "Doors"};

    private Rigidbody2D playerRigidbody;
    private PlayerUpgrades playerUpgrades;

    //TODO: move it to PlayerEvents (player is translucent on dash)
    private SpriteRenderer playerSprite;


    void Start()
    {
        playerRigidbody = gameObject.GetComponent<Rigidbody2D>();
        playerUpgrades = gameObject.GetComponent<PlayerUpgrades>();
        moveAxis = new Vector2(0, 0);

        playerSprite = GetComponentInChildren<SpriteRenderer>();
        dashInvicibilityTime = dashDuration;
    }

    // Update is called once per frame
    void Update()
    {
        float axisScaler = Time.deltaTime * 10;

        // Move player
        // -----------
        if(!dashing)
            playerRigidbody.AddForce(
                moveAxis * axisScaler * (moveSpeed + playerUpgrades.moveSpeed), 
                ForceMode2D.Impulse);

        // Dash logic
        // ----------
        // TODO: improve this piece of code
        if(dashTimer > 0) dashTimer -= Time.deltaTime;
        if (dashTimer > dashInvicibilityTime)
        {
            RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, Vector2.one, transform.rotation.z, dashAxis, 1.5f);
            bool canMove = true;

            foreach (var hit in hits)
            {
                if (dashStoppersTags.Contains(hit.collider.tag))
                {
                    canMove = false; break;
                }
            }
            playerRigidbody.AddForce(dashAxis * axisScaler * dashSpeed, ForceMode2D.Impulse);

            if (!canMove) { dashTimer = 0; return; }
        }
        else if(dashTimer <= 0 && dashing)
        {
            GetComponent<PolygonCollider2D>().enabled = true;
            playerSprite.color = playerSprite.color * new Vector4(1, 1, 1, 5);
            dashing = false;
        }
    }

    public void Dash()
    {
        if(dashing) return;
        if(moveAxis == Vector2.zero) return;

        DashSetup();
    }

    // Needed for attacks that influence movement
    public void ForceDash(float scaler = 1, Vector2 direction = new Vector2())
    {
        DashSetup();
        dashAxis = direction.normalized * scaler;
    }


    // Helper functions
    // ----------------

    private void DashSetup()
    {
        dashAxis = moveAxis.normalized;

        dashTimer = dashDuration + dashInvicibilityTime;
        GetComponent<PolygonCollider2D>().enabled = false;
        dashing = true;
        playerRigidbody.velocity = Vector2.zero;

        //TODO: move it outside maybe
        playerSprite.color = playerSprite.color * new Vector4(1, 1, 1, 0.2f);
    }
}
