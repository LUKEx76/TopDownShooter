using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PrototypPlayerMovement : MonoBehaviour
{
    //PRIVATE PROPERTIES
    private Rigidbody2D rb;
    [SerializeField] private float speed = 3f;

    [SerializeField] private Joystick moveStick;

    [SerializeField] private Joystick aimStick;

    [SerializeField] private Vector3 startPos = Vector3.zero;


    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }

    //Migrate rotation from Dual Sick in Movement Script


    void Update()
    {
        Vector2 lookDir;
        float angle;
        if (aimStick.gameObject.activeSelf)   //If AimStick exists make Player rotate according to AimStick
        {
            if (aimStick.Horizontal != 0f || aimStick.Vertical != 0f)
            {

                lookDir = new Vector2(aimStick.Horizontal, aimStick.Vertical);
                angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;

                rb.rotation = angle;
            }
        }
        else if (moveStick.Horizontal != 0f || moveStick.Vertical != 0f)
        {
            lookDir = new Vector2(moveStick.Horizontal, moveStick.Vertical);
            angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;

            rb.rotation = angle;
        }

        rb.velocity = new Vector2(moveStick.Horizontal * speed, moveStick.Vertical * speed);
    }

    public void Respawn()
    {
        transform.position = startPos;
    }
}
