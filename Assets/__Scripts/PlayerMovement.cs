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
        float vMovement = joystick.Vertical;
        float hMovement = joystick.Horizontal;
        rb.velocity = new Vector2(hMovement * speed, vMovement * speed);
    }

    public void Respawn()
    {
        transform.position = startPos;
    }
}
