using UnityEngine;
using System.Linq;

public class SaplingTree : MonoBehaviour
{
    [SerializeField] Camera _camPlayer;
    [SerializeField] TreeRegenerative _tree;

    public float growTime;
    [HideInInspector] public float initialGrowTime;
    public float timer;
    [SerializeField] float stage1;
    [SerializeField] float stage2;
    [SerializeField] float scale;
    [SerializeField] float maxScale;

    [SerializeField] GameObject sapling;
    [SerializeField] GameObject smallTree;
    [SerializeField] GameObject mediumTree;
    [SerializeField] GameObject bigTree;
    [SerializeField] GameObject timerBar;
    [SerializeField] GameObject backgroundBar;
    [SerializeField] GameObject _circleDecal;

    public bool isUpgraded = false;

    private void Awake()
    {

        _camPlayer = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
    }


    private void Start()
    {
        CaluclateStages();
        initialGrowTime = growTime;
    }

    private void Update()
    {
        Grow();
        Stages();
    }

    private void Grow()
    {
        _circleDecal.SetActive(false);
        if (isUpgraded) timer += Time.deltaTime * 5;
        else timer += Time.deltaTime;
        backgroundBar.transform.LookAt(_camPlayer.transform);
        scale = Mathf.Lerp(0, maxScale, timer / growTime);
        timerBar.transform.localScale = new Vector3(0.52f, 0.52f, scale);

        if (timer >= growTime)
        {
            sapling.SetActive(false);
            smallTree.SetActive(false);
            mediumTree.SetActive(false);
            bigTree.SetActive(true);
            timer = 0;
            Restart();
            gameObject.SetActive(false);
        }
    }

    private void Stages()
    {
        if (timer < stage1)
        {
            sapling.SetActive(true);
            smallTree.SetActive(false);
            mediumTree.SetActive(false);
        }

        else if (timer < stage2)
        {
            sapling.SetActive(false);
            smallTree.SetActive(true);
            mediumTree.SetActive(false);
        }

        else
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

    private void Restart()
    {
        growTime = initialGrowTime;
        timer = 0;
        scale = 0;
        _tree.RestartAmount();
    }
}