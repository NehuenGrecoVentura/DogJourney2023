using System.Collections;
using UnityEngine;
using DG.Tweening;

public class CinematicTree : CinematicManager, ICinematic
{
    [SerializeField] Collider _colFirstTree;
    [SerializeField] GameObject _cinematicPlay;
    [SerializeField] Character _player;
    [SerializeField] RectTransform _message;

    private Collider _col;
    private TestCinematic _cinematic;
    private Manager _gm;
    private TreeRegenerative[] _greenTrees;

    [Header("RADAR")]
    private LocationQuest _radar;

    private void Awake()
    {
        _cinematic = GetComponent<TestCinematic>();
        _col = GetComponent<Collider>();

        _greenTrees = FindObjectsOfType<TreeRegenerative>();
        _radar = FindObjectOfType<LocationQuest>();
        _gm = FindObjectOfType<Manager>();
    }

    private void Start()
    {
        _cinematicPlay.SetActive(false);
        _message.gameObject.SetActive(false);
        _message.DOAnchorPosY(-1000f, 1f);
    }

    public IEnumerator StarCinematic(float duration)
    {
        _message.gameObject.SetActive(true);
        _radar.StatusRadar(false);
        ObjStatus(false);
        _cinematic.StartCinematic(_cinematicPlay, durationCinematic);
        _colFirstTree.enabled = true;
        Destroy(_col);
        _message.DOAnchorPosY(-170f, 1f);

        yield return new WaitForSeconds(duration);

        foreach (var greenTree in _greenTrees)
        {
            if (greenTree.tag != "Purple Tree")
                greenTree.GetComponent<Collider>().enabled = true;
        }

        _player.FreezePlayer(RigidbodyConstraints.FreezeRotation);
        _gm.GreenTreesShader();
        ObjStatus(true);
        _message.transform.DOScale(0f, 0.1f);

        _message.DOAnchorPosY(-1000f, 1f);
        _message.gameObject.SetActive(false);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null) StartCoroutine(StarCinematic(durationCinematic));
    }
}