using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CreatureVault", menuName = "Sphinx/CreatureVault")]
public class CreatureVault : ScriptableObject
{
    public List<Creature> CreaturePrefabs;
    public delegate void CreatureSpawnHandler(Creature _creature);
    public event CreatureSpawnHandler CreatureSpawned;

    private int[] creatureCount = new int[4];

    public void SpwanCreature(EggClass _class, Vector3 position)
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
                    var creatureobj = Instantiate(creature.creaturePrefab, null);
                    CreatureSpawned?.Invoke(creature);

                    UpdateCreatureRecord(creature);

                    Vector3 originalPos = creatureobj.transform.position;
                    creatureobj.transform.position = new Vector3(position.x, originalPos.y, position.z);
                    return;
                }
            }
        }
    }

    public int GetCreatureCount(EggClass _class)
    {
        return creatureCount[(int)_class];
    }

    public void InitRecord()
    {
        for (int i = 0; i < creatureCount.Length; i++)
        {
            creatureCount[i] = 0;
        }
        foreach (var one in CreaturePrefabs)
        {
            one.discovered = false;
        }
    }

    private void UpdateCreatureRecord(Creature creature)
    {
        if (!creature.discovered)
        {
            creature.discovered = true;
            creatureCount[(int)creature.rarityClass]++;
        }
    }
}
