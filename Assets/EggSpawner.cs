using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggSpawner : MonoBehaviour
{
    public List<GameObject> NEggPrefabs = new List<GameObject>();
    public List<GameObject> REggPrefabs = new List<GameObject>();
    public List<GameObject> SREggPrefabs = new List<GameObject>();
    public List<GameObject> SSREggPrefabs = new List<GameObject>();

    public GameObject EggHolderPrefab;

    private GameObject egg;
    private GameObject eggHolder;
    private Animator rollerAnimator;
    private delegate void initList(List<GameObject> list);

    public void InitEggHolder(EggClass eggClass)
    {
        eggHolder = Instantiate(EggHolderPrefab, null);
        rollerAnimator = eggHolder.transform.GetComponent<Animator>();

        initList initEggList = (List<GameObject> _list) =>
        {
            int index = UnityEngine.Random.Range(0, _list.Count);
            egg = Instantiate(_list[index], eggHolder.transform);
            egg.transform.localPosition = Vector3.zero;
            egg.transform.localRotation = Quaternion.identity;
            rollerAnimator.SetTrigger("Roll");
        };

        switch (eggClass)
        {
            case EggClass.N:
                initEggList(NEggPrefabs);
                break;
            case EggClass.R:
                initEggList(REggPrefabs);
                break;
            case EggClass.SR:
                initEggList(SREggPrefabs);
                break;
            case EggClass.SSR:
                initEggList(SSREggPrefabs);
                break;
            default:
                break;
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.N))
        {
            InitEggHolder(EggClass.N);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            InitEggHolder(EggClass.R);
        }
    }
}

public enum EggClass
{
    N,
    R,
    SR,
    SSR
}
