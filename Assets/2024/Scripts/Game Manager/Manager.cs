using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

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
    [SerializeField] BoxCollider _firstTree;
    //[SerializeField] Button _buttonRope;
    private QuestUI _questUI;

    [Header("GAME OVER")]
    [SerializeField] GameObject _gameOver;
    [SerializeField] GameObject _restart;
    [SerializeField] TMP_Text _textGameOver;
    private CameraOrbit _cam;
    private bool _isGameOver = false;
    private WolfSleeping[] _allWolfs;

    [Header("TREES SHADERS")]
    [SerializeField] Material _greenTreesShader;
    [SerializeField] Material _greenTreeNormal;
    [SerializeField] Material _purpleTreesShader;
    [SerializeField] Material _purpleTreeNormal;
    private GameObject[] _greenTrees;
    private GameObject[] _purpleTrees;
    private TreeRegenerative[] _allTrees;

    [Header("UPGRADES")]
    public bool amountUpgrade = false;
    public bool speedFishUpgrade = false;
    public bool speedHookUpgrade = false;
    private FishingMinigame _fishingGame;
    public int levelFishing = 1;

    [Header("BUSH")]
    private Bush[] _allBush;

    [Header("CHAINS")]
    public bool chainsActive = false;

    [Header("MESSAGE")]
    [SerializeField] RectTransform _boxMessage;
    [SerializeField] TMP_Text _textName;
    [SerializeField] TMP_Text _textMessage;
    [SerializeField, TextArea(4, 6)] string[] _message;
    [SerializeField] Camera _camScenario;
    [SerializeField] AudioClip _soundMessage;

    private void Awake()
    {
        _quest1 = GetComponent<ManagerQuest1>();
        _myAudio = GetComponent<AudioSource>();

        _allTrees = FindObjectsOfType<TreeRegenerative>();
        _player = FindObjectOfType<Character>();
        _cam = FindObjectOfType<CameraOrbit>();
        _questUI = FindObjectOfType<QuestUI>();
        _allWolfs = FindObjectsOfType<WolfSleeping>();
        _fishingGame = FindObjectOfType<FishingMinigame>();
        _allBush = FindObjectsOfType<Bush>();

        _allDecals = GameObject.FindGameObjectsWithTag("Tree Decals");
        _greenTrees = GameObject.FindGameObjectsWithTag("Green Leaves");
        _purpleTrees = GameObject.FindGameObjectsWithTag("Purple Leaves");

    }

    void Start()
    {
        BeginGame();

        foreach (var item in _allBush)
        {
            item.enabled = false;
            item.GetComponent<Collider>().enabled = false;
        }
    }

    private void BeginGame()
    {
        _quest1.enabled = false;
        //_buttonRope.enabled = false;
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
            col.GetComponent<BoxCollider>().enabled = false;
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

    private IEnumerator ShowGameOver(GameObject cinematicGameOver, float time, string messageGameOver, Transform posRestart)
    {
        _cam.gameObject.SetActive(false);
        _player.FreezePlayer();
        cinematicGameOver.SetActive(true);
        yield return new WaitForSeconds(time);
        _gameOver.SetActive(true);
        _textGameOver.text = messageGameOver;
        _isGameOver = true;
        yield return new WaitForSeconds(3f);
        _cam.gameObject.SetActive(true);
        _player.DeFreezePlayer();
        cinematicGameOver.SetActive(false);
        _gameOver.SetActive(false);
        _player.gameObject.transform.position = posRestart.position;
        _restart.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        _restart.SetActive(false);
        _isGameOver = false;
    }

    public IEnumerator Restart(GameObject cinematicGameOver, Transform posRestart)
    {

        foreach (var wolf in _allWolfs)
        {
            wolf.wakeUp = false;
        }

        _cam.gameObject.SetActive(true);
        //_player.speed = _player.speedAux;
        _player.DeFreezePlayer();
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

    public void UpgradeFishing()
    {
        if (!speedFishUpgrade)
        {
            _fishingGame.FishSpeedMult = 0.02f;
            speedFishUpgrade = true;
        }
    }

    public void Upgrade()
    {
        if (levelFishing == 2) _fishingGame.FishSpeedMult = 0.02f;
        if (levelFishing == 3) _fishingGame.HookPower = 0.03f;
    }

    public void ActiveTutorialChain()
    {
        if (!chainsActive) StartCoroutine(TutorialChain());
        else return;
    }

    private IEnumerator TutorialChain()
    {
        chainsActive = true;

        _player.speed = 0;
        _player.FreezePlayer();

        _cam.gameObject.SetActive(false);
        _camScenario.gameObject.SetActive(true);

        _textName.text = "Special Quest";
        _textMessage.text = _message[0];
        _boxMessage.localScale = new Vector3(1, 1, 1);
        _boxMessage.DOAnchorPosY(-1000f, 0f);

        yield return new WaitForSeconds(0.5f);
        _boxMessage.gameObject.SetActive(true);
        _myAudio.PlayOneShot(_soundMessage);
        _boxMessage.DOAnchorPosY(70f, 0.5f);

        yield return new WaitForSeconds(3f);
        _boxMessage.DOAnchorPosY(-1000f, 0.5f);

        yield return new WaitForSeconds(1f);
        _textMessage.text = _message[1];
        _myAudio.PlayOneShot(_soundMessage);
        _boxMessage.DOAnchorPosY(70f, 0.5f);

        yield return new WaitForSeconds(3f);
        _boxMessage.DOAnchorPosY(-1000f, 0.5f);

        yield return new WaitForSeconds(1f);
        _textMessage.text = _message[2];
        _myAudio.PlayOneShot(_soundMessage);
        _boxMessage.DOAnchorPosY(70f, 0.5f);

        yield return new WaitForSeconds(3f);
        _boxMessage.DOAnchorPosY(-1000f, 0.5f);

        _cam.gameObject.SetActive(true);
        Destroy(_camScenario.gameObject);

        _player.speed = _player.speedAux;
        _player.DeFreezePlayer();

        yield return new WaitForSeconds(0.6f);
        _boxMessage.gameObject.SetActive(false);
    }
}