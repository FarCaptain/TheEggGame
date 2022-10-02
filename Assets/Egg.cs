using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    public EggClass eggClass;
    public List<Sprite> eggSpriteSheet = new List<Sprite>();
    public int[] phaseDuration;
    public CreatureVault creatureVault;
    public EggSetting eggSetting;

    private Animator animator;
    private new SpriteRenderer renderer;
    private int spriteSheetIdx = 0;
    private int shellDefenceIdx = 0;

    private int clickCount = 0;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {

    }

    public void SpawnCreature()
    {
        creatureVault.SpwanCreature(eggClass);
    }

    private void OnMouseDown()
    {
        if(++clickCount >= eggSetting.eggShellDefense[spriteSheetIdx])
        {
            clickCount = 0;
            if (++spriteSheetIdx >= eggSetting.eggShellDefense.Length)
            {
                // sound, vfx
                SpawnCreature();
                Destroy(gameObject);
            }
            else if (spriteSheetIdx < eggSpriteSheet.Count)
            {
                renderer.sprite = eggSpriteSheet[spriteSheetIdx++];
            }
        }
    }
}
