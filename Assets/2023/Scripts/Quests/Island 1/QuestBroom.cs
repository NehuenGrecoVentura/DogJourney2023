using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestBroom : NPCManager
{
    [SerializeField] DogEnter _dogEnter;
    private Collider _col;

    private void Awake()
    {
        _col = _dogEnter.gameObject.GetComponent<Collider>();
    }

    private void Start()
    {
        _col.enabled = false;
    }



}