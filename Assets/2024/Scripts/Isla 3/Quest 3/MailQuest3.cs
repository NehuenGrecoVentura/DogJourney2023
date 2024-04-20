using UnityEngine;

public class MailQuest3 : MonoBehaviour
{
    [SerializeField] GameObject[] _objsToDestroy;
    [SerializeField] KeyCode _buttonInteractive = KeyCode.Return;
    [SerializeField] GameObject _canvasQuest3;

    [SerializeField] AudioClip _acceptSound;
    private AudioSource _myAudio;

    [SerializeField] GameObject _mapGO;

    private Pause _pause;
    private bool _isReading = false;
    private Quest3 _quest3;
    private GateOil _gateOil;

    //[SerializeField] BoxCollider[] _colTreesPurples;
    //[SerializeField] GameObject[] _decalTrees;

    private void Awake()
    {
        _myAudio = GetComponent<AudioSource>();
        _quest3 = GetComponent<Quest3>();
        _pause = FindObjectOfType<Pause>();
        _gateOil = FindObjectOfType<GateOil>();
    }

    private void Start()
    {
        _quest3.enabled = false;
        _isReading = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(_buttonInteractive) && _isReading)
        {
            //foreach (var col in _colTreesPurples)
            //    col.enabled = true;

            //foreach (var decal in _decalTrees)
            //    decal.SetActive(true);

            
            _mapGO.SetActive(false);
            _myAudio.PlayOneShot(_acceptSound);
            _pause.Defrize();
            foreach (var item in _objsToDestroy)
                Destroy(item.gameObject);

            _canvasQuest3.SetActive(true);
            _quest3.enabled = true;
            Destroy(this);
        }
    }

    

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            _objsToDestroy[0].SetActive(true); // MUESTRO EL MAIL DE LA QUEST
            _pause.Freeze();
            _isReading = true;
        }
    }
}