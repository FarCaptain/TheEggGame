using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpPanel : MonoBehaviour
{
    [SerializeField] private Transform panel;
    [SerializeField] private Image creatureSprite;
    [SerializeField] private Text creatureName;

    [SerializeField] private CreatureVault vault;

    private void OnEnable()
    {
        vault.CreatureSpawned += PopUp;
    }

    private void OnDisable()
    {
        vault.CreatureSpawned -= PopUp;
    }

    public void PopUp(Creature creature)
    {
        panel.gameObject.SetActive(true);
        Sprite _sprite = creature.creaturePrefab.GetComponentInChildren<SpriteRenderer>().sprite;
        creatureSprite.sprite = _sprite;

        creatureName.text = "You Found: " + creature.name + "!";
        Time.timeScale = 0f;
    }

    public void CloseWindow()
    {
        panel.gameObject.SetActive(false);
        Time.timeScale = 1f;
    }
}
