using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    PlayerInputActions inputActions;

    [SerializeField] private FixedJoystick moveStick;

    private Rigidbody2D rb;

    [SerializeField] private float moveSpeed = 3f;

    private Vector2 movementInput;

    private bool stopMoving;

    void Awake()
    {
        movementInput = new Vector2();
        inputActions = new PlayerInputActions();
        inputActions.Player.Move.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        stopMoving = true;
    }


    void FixedUpdate()
    {
        //Work around to get Mouse and Joystick Movement
        if (moveStick.Horizontal != 0 && moveStick.Vertical != 0)
        {
            stopMoving = true;
            movementInput = new Vector2(moveStick.Horizontal, moveStick.Vertical);
        }
        else if (stopMoving)
        {
            movementInput = Vector2.zero;
            stopMoving = false;
        }
        Move();
    }

    void Move()
    {
        rb.MovePosition(rb.position + movementInput * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void DisEnable()
    {
        inputActions.Disable();
    }
}
