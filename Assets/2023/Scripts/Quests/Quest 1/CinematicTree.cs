using System.Collections;
using UnityEngine;
using DG.Tweening;

public class CinematicTree : CinematicManager, ICinematic
{
    [SerializeField] Collider _colFirstTree;
    [SerializeField] GameObject _cinematicPlay;
    [SerializeField] Character _player;
    [SerializeField] GameObject _message;

    private Collider _col;
    private TestCinematic _cinematic;
    private TutorialTree _tutorialTree;
    private Manager _gm;
    private TreeRegenerative[] _greenTrees;

    [Header("RADAR")]
    private LocationQuest _radar;

    private void Awake()
    {
        _cinematic = GetComponent<TestCinematic>();
        _col = GetComponent<Collider>();

        _greenTrees = FindObjectsOfType<TreeRegenerative>();
        _tutorialTree = FindObjectOfType<TutorialTree>();
        _radar = FindObjectOfType<LocationQuest>();
        _gm = FindObjectOfType<Manager>();
    }

    private void Start()
    {
        _cinematicPlay.SetActive(false);
        _message.SetActive(false);
        _message.transform.DOScale(0, 0);
    }

    public IEnumerator StarCinematic(float duration)
    {
        _message.SetActive(true);
        _radar.StatusRadar(false);
        ObjStatus(false);
        _cinematic.StartCinematic(_cinematicPlay, durationCinematic);
        _colFirstTree.enabled = true;
        Destroy(_col);

        _message.transform.DOScale(0.5f, 1.5f);

        yield return new WaitForSeconds(duration);

        foreach (var greenTree in _greenTrees)
        {
            if (greenTree.tag != "Purple Tree")
                greenTree.GetComponent<Collider>().enabled = true;
        }

        _player.FreezePlayer(RigidbodyConstraints.FreezeRotation);
        _gm.GreenTreesShader();
        ObjStatus(true);
        //_tutorialTree.gameObject.SetActive(true);
        _message.transform.DOScale(0f, 0.1f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null) StartCoroutine(StarCinematic(durationCinematic));
    }
}