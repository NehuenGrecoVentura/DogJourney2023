using System.Collections;
using UnityEngine;

public class DigBait : MonoBehaviour
{
    [SerializeField] KeyCode _keyInteractive = KeyCode.Mouse0;
    public float amountHit = 200f;
    [SerializeField] RectTransform _rectLoot;
    [SerializeField] WormHealthBar _healthBar;
    private CharacterInventory _inventory;
    private FishingQuest2 _quest;
    private DoTweenTest _doTween;
    private AudioSource _myAudio;

    [Header("RESPAWN")]
    [SerializeField] float _timeToRespawn = 5f;
    private SpawnRandom _random;
    private Collider _myCol;
    private MeshRenderer _myMesh;
    private float _initialHit;

    private void Awake()
    {
        _myAudio = GetComponent<AudioSource>();
        _myCol = GetComponent<Collider>();
        _myMesh = GetComponent<MeshRenderer>();

        _inventory = FindObjectOfType<CharacterInventory>();
        _quest = FindObjectOfType<FishingQuest2>();
        _doTween = FindObjectOfType<DoTweenTest>();
        _random = FindObjectOfType<SpawnRandom>();
    }

    private void Start()
    {
        _healthBar.gameObject.SetActive(false);
        _initialHit = amountHit;
    }

    private void FocusToDig(Character player)
    {
        Vector3 pos = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
        player.gameObject.transform.LookAt(pos);
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null) _healthBar.gameObject.SetActive(true);
    }

    private void OnTriggerStay(Collider other)
    {
        //var player = other.GetComponent<Character>();
        //if (player != null && _myCol.enabled)
        //{
        //    FocusToDig(player);
        //    if (Input.GetKey(_keyInteractive) && _inventory.shovelSelected)
        //    {
        //        if (!_myAudio.isPlaying) _myAudio.Play();

        //        player.HitDig();
        //        amountHit--;
        //        _healthBar.Bar();

        //        if (amountHit <= 0)
        //        {
        //            amountHit = 0;
        //            _myAudio.Stop();
        //            _quest.baitPicked++;
        //            _inventory.baits++;
        //            _doTween.ShowLootCoroutine(_rectLoot);
        //            player.isConstruct = false;
        //            player.DeFreezePlayer();
        //            StartCoroutine(Respawn());
        //        }
        //    }
        //}

        //else if (player != null && !Input.GetKey(_keyInteractive))
        //{
        //    _myAudio.Stop();
        //    player.isConstruct = false;
        //    player.DeFreezePlayer();
        //}

        var player = other.GetComponent<Character>();
        if (player != null && _myCol.enabled)
        {
            if (!Input.GetKey(_keyInteractive))
            {
                _healthBar.gameObject.SetActive(true);
                player.enabled = true;
                player.MainAnim();
            }

            else
            {
                if (_inventory.shovelSelected)
                {
                    FocusToDig(player);
                    player.HitDig();
                    player.enabled = false;
                    amountHit--;
                    if (!_myAudio.isPlaying) _myAudio.Play();
                    _healthBar.Bar();
                }
            }

            if (amountHit <= 0)
            {
                amountHit = 0;
                _myAudio.Stop();
                _quest.baitPicked++;
                _inventory.baits++;
                _doTween.ShowLootCoroutine(_rectLoot);
                _healthBar.gameObject.SetActive(false);
                StartCoroutine(Respawn());
                player.DeFreezePlayer();
                player.enabled = true;
                player.MainAnim();
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //var player = other.GetComponent<Character>();
        //if (player != null)
        //{
        //    _myAudio.Stop();
        //    _healthBar.gameObject.SetActive(false);
        //    player.isConstruct = false;
        //    player.DeFreezePlayer();
        //}

        var player = other.GetComponent<Character>();
        if (player != null && _myCol.enabled)
        {
            _myAudio.Stop();
            _healthBar.gameObject.SetActive(false);
            player.enabled = true;
            player.MainAnim();
        }
    }

    private IEnumerator Respawn()
    {
        _healthBar.gameObject.SetActive(false);
        _myMesh.enabled = false;
        _myCol.enabled = false;
        yield return new WaitForSeconds(_timeToRespawn);
        _random.SpawnObject(transform);
        _myMesh.enabled = true;
        _myCol.enabled = true;
        amountHit = _initialHit;
        _healthBar.Bar();
    }
}