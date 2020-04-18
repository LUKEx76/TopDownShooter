using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    //Identifier for Coin

    [SerializeField] private AudioClip collectedSound;
    [SerializeField] private int coinValue = 1;

    public AudioClip CollectedSound { get => collectedSound; }
    public int CoinValue { get => coinValue; }
}
