using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpPanel : MonoBehaviour
{
    [SerializeField] private Transform panel;
    [SerializeField] private SpriteRenderer creatureSprite;
    [SerializeField] private Text creatureName;
    [SerializeField] private Text rarity;

    [SerializeField] private CreatureVault vault;

    private void OnEnable()
    {
        vault.InitRecord();
        vault.CreatureSpawned += PopUp;
    }

    private void OnDisable()
    {
        vault.CreatureSpawned -= PopUp;
    }

    public void PopUp(Creature creature)
    {
        if (creature.discovered)
            return;
        panel.gameObject.SetActive(true);
        Sprite _sprite = creature.creaturePrefab.GetComponentInChildren<SpriteRenderer>().sprite;
        creatureSprite.sprite = _sprite;

        creatureName.text = "You Found: " + creature.name + "!";
        rarity.text = "Rarity: " + Enum.GetName( typeof(EggClass), creature.rarityClass) + "!";
        //Time.timeScale = 0f;
    }

    public void CloseWindow()
    {
        panel.gameObject.SetActive(false);
        //Time.timeScale = 1f;
    }
}
