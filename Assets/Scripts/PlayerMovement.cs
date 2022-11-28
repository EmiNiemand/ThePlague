using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using Vector2 = UnityEngine.Vector2;

public class PlayerMovement : MonoBehaviour
{
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
    void Update()
    {

    }

    void FixedUpdate()
    {
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
}
