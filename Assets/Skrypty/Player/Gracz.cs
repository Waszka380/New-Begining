using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gracz : MonoBehaviour
{
    public Inventory inventory;

    private void Awake()
    {
        inventory = new Inventory(27);
    }

    public void DropItem(Collectible item)
    {
        Vector2 spawnLocation = transform.position;

        Vector2 spawnOffset = Random.insideUnitCircle * 1.25f;

        //float randX = Random.Range(-1f, 1f);
        //float randY = Random.Range(-1f, 1f);

        //Vector3 spawnOffset = new Vector3(randX, randY, 0f).normalized;

        Collectible droppedItem = Instantiate(item,spawnLocation + spawnOffset, Quaternion.identity);

        droppedItem.rb2d.AddForce(spawnOffset * .2f, ForceMode2D.Impulse);
    }
}
