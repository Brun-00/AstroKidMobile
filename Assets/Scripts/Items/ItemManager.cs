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
        Reset();

    }   


    private void Reset()
    {
        coins.value = 0;

    }

    public void AddCoins(int amount)
    {         
        coins.value += amount;
    }


}
