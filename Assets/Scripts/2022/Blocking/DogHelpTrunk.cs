using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class DogHelpTrunk : MonoBehaviour
{
    NavMeshAgent _myAgent;
    [SerializeField] Animator _myAnim;
    [Header("POINTS SPAWNER TRUNKS")]
    [SerializeField] Transform _pointTrunk;
    [SerializeField] Transform _pointTrunk2;
    [Header("TRUNKS GAMEOBJECTS")]
    [SerializeField] GameObject _trunks;
    [SerializeField] GameObject _trunks2;
    [Header("TEXTS HINTS")]
    [SerializeField] Text _hint1;
    [SerializeField] Text _hint2;
    [Header("ICON DIALOGUE HINT")]
    [SerializeField] GameObject _iconDialogueHint;
    [Header("AUDIO CONFIG")]
    [HideInInspector] public AudioSource myAudio;
    [Header("TIME HINTS IN SCREEN")]
    [SerializeField] float _timeTextHint = 3f;
    [SerializeField] float _timeTextHint2 = 3f;

    void Start()
    {
        _iconDialogueHint.gameObject.SetActive(false);
        _myAgent = GetComponent<NavMeshAgent>();
        _trunks.gameObject.SetActive(false);
        _trunks2.gameObject.SetActive(false);
        _hint1.gameObject.SetActive(false);
        _hint2.gameObject.SetActive(false);
        myAudio = GetComponent<AudioSource>();
        
    }

    public void HelpTrunk30()
    {
        myAudio.Play();
        _hint1.gameObject.SetActive(true);
        _iconDialogueHint.gameObject.SetActive(true);
        _timeTextHint -= Time.deltaTime;
        if(_timeTextHint <= 0f)
        {
            _timeTextHint = 0f;
            _hint1.gameObject.SetActive(false);
            _iconDialogueHint.gameObject.SetActive(false);
        }
        _trunks.gameObject.SetActive(true);
        _myAgent.destination = _pointTrunk.position;
        _myAnim.SetBool("Idle", false);
        _myAnim.SetBool("Walk", true);
    }

    public void HelpTrunk60()
    {
        myAudio.Play();
        _hint2.gameObject.SetActive(true);
        _iconDialogueHint.gameObject.SetActive(true);
        _timeTextHint2 -= Time.deltaTime;
        if (_timeTextHint2 <= 0f)
        {
            _timeTextHint2 = 0f;
            _hint2.gameObject.SetActive(false);
            _iconDialogueHint.gameObject.SetActive(false);
        }
        _trunks2.gameObject.SetActive(true);
        _myAgent.destination = _pointTrunk2.position;
        _myAnim.SetBool("Idle", false);
        _myAnim.SetBool("Walk", true);
    }
}