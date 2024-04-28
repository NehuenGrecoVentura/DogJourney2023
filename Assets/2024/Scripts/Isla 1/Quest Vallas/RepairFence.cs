using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class RepairFence : MonoBehaviour
{
    [SerializeField] GameObject _iconMaterial;
    [SerializeField] GameObject _cinematic;
    [SerializeField] GameObject _cinematicSeed;
    [SerializeField] GameObject _iconSeed;

    [SerializeField] Image _fadeOut;
    [SerializeField] GameObject _fencesRepared;
    [SerializeField] GameObject _fencesBroken;
    [SerializeField] TMP_Text _textName;
    private CameraOrbit _camPlayer;
    private Character _player;
    private Collider _myCol;
    //private Manager _gm;
    private MeshRenderer _myMesh;

    [SerializeField] RectTransform _message;
    [SerializeField, TextArea(6, 4)] string _messageEnd;
    [SerializeField] TMP_Text _textEnd;

    //[Header("NEXT QUEST")]
    //private FirstMarket _market;
    //private LocationQuest _radar;

    private AudioSource _myAudio;
    [SerializeField] AudioClip _soundMessage;
    [SerializeField] AudioClip _soundConstruct;


    private void Awake()
    {
        _myCol = GetComponent<Collider>();
        _myMesh = GetComponent<MeshRenderer>();
        _myAudio = GetComponent<AudioSource>();

        _camPlayer = FindObjectOfType<CameraOrbit>();
        _player = FindObjectOfType<Character>();
        //_gm = FindObjectOfType<Manager>();
        //_radar = FindObjectOfType<LocationQuest>();
        //_market = FindObjectOfType<FirstMarket>();
    }

    private void Start()
    {
        _iconMaterial.transform.DOScale(0f, 0f);
        _cinematic.SetActive(false);
        _myAudio.Stop();
        _iconSeed.SetActive(false);
        _cinematicSeed.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null) _iconMaterial.transform.DOScale(1.5f, 0.5f);
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && Input.GetKeyDown(KeyCode.Space)) 
            StartCoroutine(Repair());
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null) _iconMaterial.transform.DOScale(0f, 0.5f);
    }

    private IEnumerator Repair()
    {
        _textName.text = "Florist";
        //_myAudio.Play();
        _myAudio.PlayOneShot(_soundConstruct);
        Destroy(_myCol);
        Destroy(_iconMaterial.gameObject);
        _myMesh.enabled = false;
        _player.isConstruct = true;
        _player.speed = 0;
        _player.FreezePlayer(RigidbodyConstraints.FreezeAll);

        _camPlayer.gameObject.SetActive(false);
        _cinematic.SetActive(true);
        _fadeOut.DOColor(Color.black, 2f);

        float elapsedTime = 0f;

        while (elapsedTime < 3f)
        {
            if (elapsedTime > 0f) _player.PlayAnim("Build");

            // Incrementa el tiempo transcurrido
            elapsedTime += Time.deltaTime;

            // Espera un corto tiempo antes de la próxima iteración
            yield return null;
        }


        
        //yield return new WaitForSeconds(4f);
        _fadeOut.DOColor(Color.clear, 2f);
        Destroy(_fencesBroken);
        _fencesRepared.SetActive(true);
        yield return new WaitForSeconds(2);
        _player.isConstruct = false;

        _textEnd.text = _messageEnd;
        _message.DOAnchorPosY(-1000f, 0f);
        _message.localScale = new Vector3(1, 1, 1);
        _message.gameObject.SetActive(true);
        _myAudio.PlayOneShot(_soundMessage);
        _message.DOAnchorPosY(70f, 0.5f);


        yield return new WaitForSeconds(5f);
        _player.speed = _player.speedAux;
        _player.FreezePlayer(RigidbodyConstraints.FreezeRotation);
        _camPlayer.gameObject.SetActive(true);


        _message.gameObject.SetActive(false);
        _message.DOAnchorPosY(-1000f, 0f);


        Destroy(_cinematic);
        //_gm.QuestCompleted();
        //_radar.target = _market.gameObject.transform;
        _iconSeed.SetActive(true);
        //Destroy(this);
        Destroy(gameObject);
    }
}
