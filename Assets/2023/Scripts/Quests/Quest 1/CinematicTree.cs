using System.Collections;
using UnityEngine;

public class CinematicTree : CinematicManager, ICinematic
{
    [SerializeField] Collider _colFirstTree;
    [SerializeField] GameObject _cinematicPlay;
    [SerializeField] Character _player;

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
    }

    public IEnumerator StarCinematic(float duration)
    {
        _radar.StatusRadar(false);
        ObjStatus(false);
        _cinematic.StartCinematic(_cinematicPlay, durationCinematic);
        _colFirstTree.enabled = true;
        Destroy(_col);
        yield return new WaitForSeconds(duration);

        foreach (var greenTree in _greenTrees)
        {
            if (greenTree.tag != "Purple Tree")
                greenTree.GetComponent<Collider>().enabled = true;
        }

        _player.FreezePlayer(RigidbodyConstraints.FreezeRotation);
        _gm.GreenTreesShader();
        ObjStatus(true);
        _tutorialTree.gameObject.SetActive(true);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null) StartCoroutine(StarCinematic(durationCinematic));
    }
}