using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public CollectibleType type;
    public Sprite icon;

    public Rigidbody2D rb2d;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Gracz player = collision.GetComponent<Gracz>();

        if(player != null)
        {
            player.inventory.Add(this);
            Destroy(this.gameObject);
        }
    }
}

public enum CollectibleType
{
    NONE,
    GEM,
    GRZYB
}