using System.Collections;
using UnityEngine;

public class IntroGame : MonoBehaviour
{
    [SerializeField] Camera _camIntro;
    [SerializeField] Camera _camPlayer;
    [SerializeField] float _timeIntro = 14f;
    [SerializeField] GameObject[] _objsHidden;
    [SerializeField] KeyCode _buttonSkip = KeyCode.Space;
    private Cheats _cheats;
    private Character2022 _player;

    [Header("ICONS")]
    private Bilboard[] _billboards;
    private BillboardGrass[] _billboardsGrass;
    private IconsInteractive[] _icons;
    private LocationQuest _map;

    private void Awake()
    {
        _billboards = FindObjectsOfType<Bilboard>();
        _billboardsGrass = FindObjectsOfType<BillboardGrass>();
        _icons = FindObjectsOfType<IconsInteractive>();
        _map = FindObjectOfType<LocationQuest>();
        _cheats = FindObjectOfType<Cheats>();
        _player = FindObjectOfType<Character2022>();
    }

    void Start()
    {
        _camPlayer.gameObject.SetActive(false);
        _camIntro.gameObject.SetActive(true);
        IntroStatus(false);
        StartCoroutine(CloseIntro());
    }

    void IntroStatus(bool active)
    {
        _map.enabled = active;
        _cheats.enabled = active;
        _player.enabled = active;

        foreach (var item in _billboards)
            item.enabled = active;

        foreach (var item in _billboardsGrass)
            item.enabled = active;

        foreach (var item in _icons)
            item.enabled = active;

        foreach (var obj in _objsHidden)
            obj.gameObject.SetActive(active);
    }

    void Update()
    {
        if (Input.GetKeyDown(_buttonSkip))
        {
            _camPlayer.gameObject.SetActive(true);
            _camIntro.gameObject.SetActive(false);
            IntroStatus(true);
            Destroy(gameObject);
        }
    }

    IEnumerator CloseIntro()
    {
        while (true)
        {
            yield return new WaitForSeconds(_timeIntro);
            _camPlayer.gameObject.SetActive(true);
            _camIntro.gameObject.SetActive(false);
            IntroStatus(true);
            Destroy(gameObject);
        }
    }
}