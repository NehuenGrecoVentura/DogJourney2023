using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Manager : MonoBehaviour
{
    private ManagerQuest1 _quest1;
    private AudioSource _myAudio;
    private Character _player;
    private GameObject[] _allDecals;

    [Header("WIN")]
    [SerializeField] AudioClip _soundWin;
    [SerializeField] Text _winText;
    [SerializeField] float _winTextTime;
    [SerializeField] Animator[] _doorsGatesAnims;
    [SerializeField] GameObject[] _objsToHide;
    [SerializeField] Collider _firstTree;
    [SerializeField] Button _buttonRope;
    private QuestUI _questUI;

    [Header("GAME OVER")]
    [SerializeField] GameObject _gameOver;
    [SerializeField] GameObject _restart;
    [SerializeField] TMP_Text _textGameOver;
    private CameraOrbit _cam;
    private bool _isGameOver = false;

    [Header("TREES SHADERS")]
    [SerializeField] Material _greenTreesShader;
    [SerializeField] Material _greenTreeNormal;
    [SerializeField] Material _purpleTreesShader;
    [SerializeField] Material _purpleTreeNormal;
    private GameObject[] _greenTrees;
    private GameObject[] _purpleTrees;
    private TreeRegenerative[] _allTrees;

    private void Awake()
    {
        _quest1 = GetComponent<ManagerQuest1>();
        _myAudio = GetComponent<AudioSource>();

        _allTrees = FindObjectsOfType<TreeRegenerative>();
        _player = FindObjectOfType<Character>();
        _cam = FindObjectOfType<CameraOrbit>();
        _questUI = FindObjectOfType<QuestUI>();

        _allDecals = GameObject.FindGameObjectsWithTag("Tree Decals");
        _greenTrees = GameObject.FindGameObjectsWithTag("Green Leaves");
        _purpleTrees = GameObject.FindGameObjectsWithTag("Purple Leaves");
    }

    void Start()
    {
        BeginGame();
    }

    private void BeginGame()
    {
        _quest1.enabled = false;
        _buttonRope.enabled = false;
        _firstTree.enabled = false;
        _winText.gameObject.SetActive(false);
        _gameOver.SetActive(false);
        _restart.SetActive(false);

        foreach (var obj in _objsToHide)
            obj.SetActive(false);

        foreach (var gate in _doorsGatesAnims)
            gate.enabled = false;

        foreach (var decal in _allDecals)
            decal.SetActive(false);

        foreach (var col in _allTrees)
            col.GetComponent<Collider>().enabled = false;
    }

    public void QuestCompleted()
    {
        _questUI.UIStatus(false);
        _player.PlayAnim("Win");
        _myAudio.PlayOneShot(_soundWin);
        _winText.gameObject.SetActive(true);
        StartCoroutine(WinTimeInScreen());
    }

    public void GreenTreesShader()
    {
        foreach (var greenTree in _greenTrees)
            greenTree.GetComponent<MeshRenderer>().material = _greenTreesShader;
    }

    public void GreenTreesNormal()
    {
        foreach (var greenTree in _greenTrees)
            greenTree.GetComponent<MeshRenderer>().material = _greenTreeNormal;
    }

    public void PurpleTreesShader()
    {
        foreach (var purpleTree in _purpleTrees)
            purpleTree.GetComponent<MeshRenderer>().material = _purpleTreesShader;
    }

    public void PurpleTreesNormal()
    {
        foreach (var purpleTree in _purpleTrees)
            purpleTree.GetComponent<MeshRenderer>().material = _purpleTreeNormal;
    }

    private void RestartCheckpoint(GameObject cinematicGameOver, Transform posRestart)
    {
        if (_gameOver) StartCoroutine(Restart(cinematicGameOver, posRestart));
    }

    public void GameOver(GameObject cinematicGameOver, float time, string textGameOver, Transform posRestart)
    {
        StartCoroutine(ShowGameOver(cinematicGameOver, time, textGameOver, posRestart));
    }

    private IEnumerator ShowGameOver(GameObject cinematicGameOver, float time, string textGameOver, Transform posRestart)
    {
        _cam.gameObject.SetActive(false);
        _player.enabled = false;
        cinematicGameOver.SetActive(true);
        yield return new WaitForSeconds(time);
        _gameOver.SetActive(true);
        _textGameOver.text = textGameOver;
        _isGameOver = true;
        yield return new WaitForSeconds(3f);
        RestartCheckpoint(cinematicGameOver, posRestart);
    }

    private IEnumerator Restart(GameObject cinematicGameOver, Transform posRestart)
    {
        _cam.gameObject.SetActive(true);
        _player.enabled = true;
        cinematicGameOver.SetActive(false);
        _gameOver.SetActive(false);
        _player.gameObject.transform.position = posRestart.position;
        _restart.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        _restart.SetActive(false);
        _isGameOver = false;
    }

    private IEnumerator WinTimeInScreen()
    {
        while (true)
        {
            yield return new WaitForSeconds(_winTextTime);
            _winText.gameObject.SetActive(false);
            StopCoroutine(WinTimeInScreen());
        }
    }
}