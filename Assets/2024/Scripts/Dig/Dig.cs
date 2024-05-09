using System.Collections;
using UnityEngine;
using TMPro;

public class Dig : MonoBehaviour
{
    [SerializeField] DigHealthBar _healthBar;
    
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
    private SpawnRandom _random;
    private Collider _myCol;
    private MeshRenderer _myMesh;

    private void Awake()
    {
        _myAudio = GetComponent<AudioSource>();
        _myCol = GetComponent<Collider>();
        _myMesh = GetComponent<MeshRenderer>();

        _message = FindObjectOfType<DoTweenManager>();
        _invetory = FindObjectOfType<CharacterInventory>();
        _random = FindObjectOfType<SpawnRandom>();
    }

    private void Start()
    {
        _healthBar.gameObject.SetActive(false);
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
        if (player != null) _healthBar.gameObject.SetActive(true);
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
            _healthBar.Bar();

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

                _invetory.flowers++;
                if (_invetory.flowers < 4) _message.ShowUI("+1", _boxSlide, _textAmount); // CAMBIAR CUANDO EXPANDAMOS A LA ISLA 2 ESTA LINEA.
                StartCoroutine(Respawn());
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
        _random.SpawnObject(transform);
        _myMesh.enabled = true;
        _myCol.enabled = true;
        amountHit = _initialHit;
        _healthBar.Bar();
    }
}