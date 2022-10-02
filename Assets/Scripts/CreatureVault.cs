using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CreatureVault", menuName = "Sphinx/CreatureVault")]
public class CreatureVault : ScriptableObject
{
    public List<Creature> CreaturePrefabs;


    public void SpwanCreature(EggClass _class)
    {
        int randNum = Random.Range(1, 101);
        int sum = 0;
        foreach (var creature in CreaturePrefabs)
        {
            if (creature.rarityClass != _class)
            {
                continue;
            }
            else
            {
                sum += creature.probabilityInClass;
                if (randNum <= sum)
                {
                    Instantiate(creature.creaturePrefab, null);
                    return;
                }
            }
        }
    }
}
