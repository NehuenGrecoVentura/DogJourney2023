using System.Collections;
using UnityEngine;
using TMPro;

public class Dig : MonoBehaviour
{
    //[SerializeField] DigHealthBar _healthBar;
    [SerializeField] HitBarFlower _hitBar;
    
    [SerializeField] KeyCode _inputInteractive = KeyCode.Mouse0;
    private AudioSource _myAudio;

    [Header("HIT")]
    public float amountHit = 200f;
    private float _initialHit;

    [Header("INVENTORY UI")]
    [SerializeField] RectTransform _boxSlide;
    [SerializeField] TMP_Text _textAmount;
    private DoTweenManager _message;
    private CharacterInventory _invetory;

    [Header("RESPAWN")]
    [SerializeField] float _timeToRespawn = 5f;
    [SerializeField] MeshRenderer[] _myMeshes;
    private SpawnRandom _random;
    private Collider _myCol;
    

    private void Awake()
    {
        _myAudio = GetComponent<AudioSource>();
        _myCol = GetComponent<Collider>();

        _message = FindObjectOfType<DoTweenManager>();
        _invetory = FindObjectOfType<CharacterInventory>();
        _random = FindObjectOfType<SpawnRandom>();
    }

    private void Start()
    {
        //_healthBar.gameObject.SetActive(false);
        _hitBar.gameObject.SetActive(false);
        _initialHit = amountHit;
    }

    private void FocusToFlower(Character player)
    {
        Vector3 pos = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
        player.gameObject.transform.LookAt(pos);
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null) _hitBar.gameObject.SetActive(true);
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && Input.GetKey(_inputInteractive) && _invetory.shovelSelected)
        {
            if (!_myAudio.isPlaying) _myAudio.Play();
            FocusToFlower(player);
            player.HitDig();
            amountHit--;
            _hitBar.Bar();

            if (amountHit <= 0)
            {
                amountHit = 0;
                //_message.ShowUI("+1", _boxMessage, _textAmount);
                //int randomIndex = Random.Range(0, 2);

                //switch (randomIndex)
                //{
                //    case 0:
                //        // Incrementa la variable seeds
                //        _invetory.seeds++;
                //        break;

                //    case 1:
                //        // Incrementa la variable flowers
                //        _invetory.flowers++;
                //        break;

                //    default:
                //        Debug.LogError("Index fuera de rango.");
                //        break;
                //}

                _myAudio.Stop();
                player.isConstruct = false;
                player.DeFreezePlayer();
                _invetory.flowers++;
                if (_invetory.flowers < 4) _message.ShowUI("+1", _boxSlide, _textAmount); // CAMBIAR CUANDO EXPANDAMOS A LA ISLA 2 ESTA LINEA.
                StartCoroutine(Respawn());
            }
        }

        else if (player != null && !Input.GetKey(_inputInteractive))
        {
            _myAudio.Stop();
            player.isConstruct = false;
            player.DeFreezePlayer();
        }
            
            
            
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            _myAudio.Stop();
            _hitBar.gameObject.SetActive(false);
            player.isConstruct = false;
            player.DeFreezePlayer();
        }
    }

    private IEnumerator Respawn()
    {
        _hitBar.gameObject.SetActive(false);

        foreach (var item in _myMeshes)
        {
            item.enabled = false;
        }

        _myCol.enabled = false;
        yield return new WaitForSeconds(_timeToRespawn);
        _random.SpawnObject(transform);

        foreach (var item in _myMeshes)
        {
            item.enabled = true;
        }

        _myCol.enabled = true;
        amountHit = _initialHit;
        _hitBar.Bar();
    }
}