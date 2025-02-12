using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory_UI : MonoBehaviour
{
    public GameObject inventoryPanel;
    public Gracz player;
    public List<Slots_UI> slots = new List<Slots_UI>();


    private void Awake()
    {
        inventoryPanel.SetActive(false);
    }

    void Update()
    {
        Refresh();
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ToggleInventory();
        }

        //Chwilowy Test dropowanie itemu na 1 slocie bo zrobienie selektora na pasek itp by za d³ugo trwa³o narazie 
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Remove(0);
            print("Remove 0 sie wywolalo");
        }
    }

    public void ToggleInventory()
    {
        if (!inventoryPanel.activeSelf)
        {
            inventoryPanel.SetActive(true);
            Refresh();
        }
        else
        {
            inventoryPanel.SetActive(false);
        }
    }

    void Refresh()
    {
        if(slots.Count == player.inventory.slots.Count)
        {
            for(int i = 0; i < slots.Count; i++)
            {
                if(player.inventory.slots[i].type != CollectibleType.NONE)
                {
                    slots[i].SetItem(player.inventory.slots[i]);
                }
                else
                {
                    slots[i].SetEmpty();
                }
            }
        }
    }

    public void Remove(int slotID)
    {
        Collectible itemToDrop = GameManager.instance.itemManager.GetItemByType(player.inventory.slots[slotID].type);

        if(itemToDrop != null)
        {
            print("itemToDrop != null");
            player.DropItem(itemToDrop);
            player.inventory.Remove(slotID);
            Refresh();
        }
        
    }
}
