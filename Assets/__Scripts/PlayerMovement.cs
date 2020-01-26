using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    //PRIVATE PROPERTIES
    private Rigidbody2D rb;
    [SerializeField] private float speed = 3f;

    [SerializeField] private Joystick joystick;

    [SerializeField] private Vector3 startPos;


    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            Vector2 lookDir = new Vector2(joystick.Horizontal, joystick.Vertical);
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;

            rb.rotation = angle;
        }

        rb.velocity = new Vector2(joystick.Horizontal * speed, joystick.Vertical * speed);
    }

    public void Respawn()
    {
        transform.position = startPos;
    }
}
