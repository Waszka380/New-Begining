using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps; 

[CreateAssetMenu(fileName = "Biome", menuName = "Biome")]
public class Biome : ScriptableObject
{
    public string biomeName;
    public Color biomeColor;
    public GameObject[] biomePrefabs;
    public TileBase[] biomeTiles;
}