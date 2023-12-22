using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sapling : MonoBehaviour
{
    [SerializeField] private Camera player;
    [SerializeField] private bool isGrowing;
    [SerializeField] private float growTime;
    [SerializeField] private float timer;
    [SerializeField] private float stage1;
    [SerializeField] private float stage2;
    [SerializeField] private GameObject sapling;
    [SerializeField] private GameObject smallTree;
    [SerializeField] private GameObject mediumTree;
    [SerializeField] private GameObject bigTree;
    [SerializeField] private GameObject timerBar;
    [SerializeField] private float scale;
    [SerializeField] private float maxScale;
    [SerializeField] private GameObject backgroundBar;
    [SerializeField] CutTree _treeRegenrated;

    private void Start()
    {
        CaluclateStages();
        player = FindObjectOfType<Camera>();
    }

    private void Update()
    {
        Grow();
        Stages();
    }

    public void setGrowTrue()
    {
        isGrowing = true;
        CaluclateStages();
    }

    public void Grow()
    {
        if (isGrowing)
        {
            timer += Time.deltaTime;
            backgroundBar.transform.LookAt(player.transform);
            scale = Mathf.Lerp(0.05f, maxScale, timer/growTime - 0.5f);
            timerBar.transform.localScale = new Vector3(0.52f,0.52f,scale);
            if (timer >= growTime)
            {
                _treeRegenrated.lifeTree = _treeRegenrated.initialLife;
                sapling.SetActive(false);
                smallTree.SetActive(false);
                mediumTree.SetActive(false);
                isGrowing = false;
                bigTree.SetActive(true);
                timer = 0;
                gameObject.SetActive(false);
            }
        }
    }

    public void Stages()
    {
        if (timer < stage1)
        {
            sapling.SetActive(true);
            smallTree.SetActive(false);
            mediumTree.SetActive(false);
        }

        if (timer > stage1 && timer < stage2)
        {
            sapling.SetActive(false);
            smallTree.SetActive(true);
            mediumTree.SetActive(false);
        }

        if (timer > stage2)
        {
            sapling.SetActive(false);
            smallTree.SetActive(false);
            mediumTree.SetActive(true);
        }
        
    }

    public void CaluclateStages()
    {
        stage1 = growTime / 3;
        stage2 = stage1 * 2;
    }
}
