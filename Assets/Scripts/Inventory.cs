using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    [SerializeField] private List<Item> items;
    [SerializeField] private UIManager uiManager;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool AddItem(Item.itemType type, int quantity)
    {
        //if adding negative quantity returns false
        if(quantity < 0)
        {
            return false;
        }

        //check if item already exists
        for (int i =0; i<items.Count; i++)
        {
            if (items[i].type == type)
            {
                items[i].quantity += quantity;

                uiManager.UpdateItemUI(items[i]);

                return true;
            }
        }

        return false;
    }

    public bool RemoveItem(Item.itemType type, int quantity)
    {
        //if subtracting negative quantity returns false
        if (quantity < 0)
        {
            return false;
        }

        //check if item already exists
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].type == type)
            {
                if (items[i].quantity - quantity < 0)
                {
                    return false;
                }

                items[i].quantity -= quantity;

                uiManager.UpdateItemUI(items[i]);

                return true;
            }
        }

        return false;
    }

    public int GetItemQuantity(Item.itemType type)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].type == type)
            {
                return items[i].quantity;
            }
        }

        return 0;
    }

}
