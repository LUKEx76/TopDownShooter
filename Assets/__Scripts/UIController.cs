using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] Button fireButton;

    [SerializeField] Joystick aimStick;

    void Start()
    {
        aimStick.gameObject.SetActive(false);
        fireButton.gameObject.SetActive(true);
    }


    public void SwapControlls()
    {
        if (fireButton.gameObject.activeSelf)
        {
            fireButton.gameObject.SetActive(false);
            aimStick.gameObject.SetActive(true);
        }
        else if (aimStick.gameObject.activeSelf)
        {
            aimStick.gameObject.SetActive(false);
            fireButton.gameObject.SetActive(true);
        }
        else
        {
            aimStick.gameObject.SetActive(true);
        }
    }
}
