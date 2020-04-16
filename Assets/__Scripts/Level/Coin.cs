using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    //Identifier for Coin

    [SerializeField] private int coinValue = 1;

    public int CoinValue { get => coinValue; }
}
