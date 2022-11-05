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

    [SerializeField] private float MoveSpeed;
    // Start is called before the first frame update
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        Rigidbody.AddForce(MoveAxis * MoveSpeed, ForceMode2D.Impulse);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveAxis = context.ReadValue<Vector2>();
    }
}
