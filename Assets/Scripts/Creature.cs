using System;
using UnityEngine;

[Serializable]
public class Creature
{
    public string name;
    public GameObject creaturePrefab;
    public EggClass rarityClass;
    public int probabilityInClass;

    public bool discovered = false;
}