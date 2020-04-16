using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    //Identifier of an Apple
    //Heals Player for 1 Heart (2 Health)

    [SerializeField] private int healsFor = 2;

    public int HealsFor { get => healsFor; }
}
