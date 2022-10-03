using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    public CreatureVault vault;
    public Text scoreText;

    private void OnEnable()
    {
        vault.InitRecord();
    }

    private void OnDisable()
    {
    }

    public void UpdateScore(Creature creature)
    {
        if (creature.discovered)
            return;

        string txt =
            "Creature Found:\n<color=red> SSR: " + vault.GetCreatureCount(EggClass.SSR) + "/1 </color>\n" +
            "<color=yellow> SR: " + vault.GetCreatureCount(EggClass.SR) + "/2 </color>\n" +
            "<color=blue> R: " + vault.GetCreatureCount(EggClass.R) + "/5 </color>\n" +
            "<color=brown> N: " + vault.GetCreatureCount(EggClass.N) + "/6 </color> \n";

        scoreText.text = txt;
    }

    private void Update()
    {
        string txt =
        "Creature Found:\n<color=red> SSR: " + vault.GetCreatureCount(EggClass.SSR) + "/1 </color>\n" +
        "<color=yellow> SR: " + vault.GetCreatureCount(EggClass.SR) + "/2 </color>\n" +
        "<color=blue> R: " + vault.GetCreatureCount(EggClass.R) + "/5 </color>\n" +
        "<color=brown> N: " + vault.GetCreatureCount(EggClass.N) + "/6 </color> \n";

        scoreText.text = txt;
    }
}
