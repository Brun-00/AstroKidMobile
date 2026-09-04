using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;

public class ItemManager : Singleton<ItemManager>
{
    public GameObject coinPfb;

    public SOInt coins;

    private void Start()
    {
        // Reset the coin count when the scene starts.
        Reset();
    }

    private void Reset()
    {
        // Set the shared coin value back to zero.
        coins.value = 0;
    }

    public void AddCoins(int amount)
    {
        // Add the collected amount to the total.
        coins.value += amount;
    }
}