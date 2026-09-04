using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollectableCoin : ItemCollectableBase
{
    public SOInt price;

    protected override void OnCollect()
    {
        // Run the base collection effects first.
        base.OnCollect();

        // Add the coin's value to the player's total.
        ItemManager.Instance.AddCoins(price.value);
    }
}