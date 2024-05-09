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
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            FocusToDig(player);
            if (Input.GetKey(_keyInteractive) && _inventory.shovelSelected)
            {
                if (!_myAudio.isPlaying) _myAudio.Play();

                player.HitDig();
                amountHit--;
                _healthBar.Bar();

                if (amountHit <= 0)
                {
                    amountHit = 0;
                    _quest.baitPicked++;
                    _doTween.ShowLootCoroutine(_rectLoot);
                    StartCoroutine(Respawn());
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null) _healthBar.gameObject.SetActive(false);
    }

    private IEnumerator Respawn()
    {
        _healthBar.gameObject.SetActive(false);
        _myMesh.enabled = false;
        _myCol.enabled = false;
        yield return new WaitForSeconds(_timeToRespawn);
        _myMesh.enabled = true;
        _myCol.enabled = true;
        amountHit = _initialHit;
        _healthBar.Bar();
    }
}