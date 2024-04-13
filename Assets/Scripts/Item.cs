using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Inventory;

public class Item : MonoBehaviour
{
    public enum itemType
    {
        gold,
        apple,
        banana,
        pork,
        oliveOil,
        rockSalt,
        hotSauce,
        healthPotion,
        manaPotion,
        tea,
        knife,
        alcohol,
        book,
        candle,
    }

    public itemType type;
    public int quantity =0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
