using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameObject player;

    private Vector3 offset;

    void Start()
    {
        try
        {
            player = FindObjectOfType<PlayerMovement>().gameObject;
            offset = transform.position - player.transform.position;
        }
        catch
        {
            player = null;
            offset = new Vector3(0f, 0f, -10f);
        }

    }


    void Update()
    {
        if (player)
        {
            transform.position = player.transform.position + offset;
        }
    }
}
