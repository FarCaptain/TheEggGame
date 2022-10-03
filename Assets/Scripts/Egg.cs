using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    public EggClass eggClass;
    public List<Sprite> eggSpriteSheet = new List<Sprite>();
    public CreatureVault creatureVault;
    public EggSetting eggSetting;

    [SerializeField] private ParticleSystem eggBrokenParticle;

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

    public void SpawnCreature()
    {
        creatureVault.SpwanCreature(eggClass, transform.position);
    }

    private void OnMouseDown()
    {
        if (shellDefenceIdx >= eggSetting.eggShellDefense.Length)
            return;

        if(++clickCount >= eggSetting.eggShellDefense[shellDefenceIdx])
        {
            clickCount = 0;
            if (spriteSheetIdx < eggSpriteSheet.Count - 1)
            {
                renderer.sprite = eggSpriteSheet[++spriteSheetIdx];
            }
            if (shellDefenceIdx++ == eggSetting.eggShellDefense.Length - 1)
            {
                StartCoroutine(OnShellBreak());
            }
        }
    }

    private IEnumerator OnShellBreak()
    {
        transform.parent = null;
        animator.enabled = true;
        var particle = Instantiate(eggBrokenParticle, transform.position, Quaternion.Euler(30f, 0f, 0f));
        particle.Play();
        yield return new WaitForSeconds(0.5f);
        SpawnCreature();
        // particle would self-destroy
        Destroy(gameObject);
    }
}
