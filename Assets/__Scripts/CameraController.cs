using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject player;

    private Vector3 offset;

    void Start()
    {
        player = FindObjectOfType<PlayerMovement>().gameObject;
        offset = transform.position - player.transform.position;
    }


    void Update()
    {
        if (player)
        {
            transform.position = player.transform.position + offset;
        }
    }
}
