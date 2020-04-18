using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Apple : MonoBehaviour
{
    //Identifier of an Apple
    //Heals Player for 1 Heart (2 Health)
    [SerializeField] private AudioClip consumedSound;

    [SerializeField] private int healsFor = 2;

    public AudioClip ConsumedSound { get => consumedSound; }

    public int HealsFor { get => healsFor; }
}
