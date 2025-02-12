using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public Collectible[] CollectibleItems;

    private Dictionary<CollectibleType,Collectible> collectibleItemDict =
        new Dictionary<CollectibleType,Collectible>();

    private void Awake()
    {
        foreach (Collectible collectible in CollectibleItems)
        {
            AddItem(collectible);
        }
    }

    private void AddItem(Collectible item)
    {
        if (collectibleItemDict.ContainsKey(item.type))
        {
            collectibleItemDict.Add(item.type, item);
        }
    }

    public Collectible GetItemByType(CollectibleType type)
    {
        if (collectibleItemDict.ContainsKey(type))
        {
            return collectibleItemDict[type];
        }
        return null;
    }
}
