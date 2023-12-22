using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaplingTest : MonoBehaviour
{
    Camera player;
    [SerializeField] private bool isGrowing;
    [SerializeField] private float growTime;
    [SerializeField] public float timer;
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
    [SerializeField] private GameObject _circleDecal;
    [SerializeField] TreeRegen _treeRegen;

    private NewMarket _markets;

    private void Awake()
    {
        _markets = FindObjectOfType<NewMarket>();
    }

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
            _circleDecal.SetActive(false);

            if (_markets.activeUpgradeTreeReg) timer += Time.deltaTime * 5;
            else timer += Time.deltaTime;

            backgroundBar.transform.LookAt(player.transform);
            scale = Mathf.Lerp(0, maxScale, timer / growTime);
            timerBar.transform.localScale = new Vector3(0.52f, 0.52f, scale);
            if (timer >= growTime)
            {
                sapling.SetActive(false);
                smallTree.SetActive(false);
                mediumTree.SetActive(false);
                isGrowing = false;
                bigTree.SetActive(true);
                timer = 0;
                _treeRegen.hitDown = _treeRegen.initialHitDown;
                _circleDecal.SetActive(true);
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
