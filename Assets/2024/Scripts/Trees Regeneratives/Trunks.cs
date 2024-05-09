using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;
using System.Collections;

public class Trunks : MonoBehaviour
{
    [SerializeField] GameObject _iconInteractive;
    [SerializeField] KeyCode _buttonInteractive;
    [HideInInspector] public bool isUpgraded = false;
    private CharacterInventory _inventory;

    private DoTweenTest _doTweenMessage;
    private DoTweenManager _doTweenManager;
    [SerializeField] GameObject _posJump;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _duration;   
    [SerializeField] int _counts = 1;
    public GameObject[] _logs;

    private Vector3[] _initiaPos;
    private Collider _myCol;
    private Manager _gm;
    private Vector3 _initialSize;

    [SerializeField] NavMeshAgent _trolleyNavMesh;

    private void Awake()
    {
        _myCol = GetComponent<Collider>();
        _inventory = FindObjectOfType<CharacterInventory>();
        _doTweenMessage = FindObjectOfType<DoTweenTest>();
        _doTweenManager = FindObjectOfType<DoTweenManager>();
        _gm = FindObjectOfType<Manager>();
        _posJump = GameObject.Find("TroleyJumpPos");
    }

    void Start()
    {
        _iconInteractive.SetActive(false);
        _iconInteractive.transform.DOScale(0, 0);
        SetInitialPos();


        _initialSize = new Vector3(3.879673f, 2.363775f, 4.577576f);

        if (_myCol is BoxCollider)
        {
            ((BoxCollider)_myCol).size = _initialSize;
        }
    }

    private void SetInitialPos()
    {
        _initiaPos = new Vector3[_logs.Length];

        for (int i = 0; i < _logs.Length; i++)
        {
            _initiaPos[i] = _logs[i].transform.position;
        }
    }

    public void UpgradeTrolley()
    {
        // Si pertenece al arbol verde...
        if (gameObject.layer == 3)
        {
            // Si no compr� el upgrade, entonces junta de a uno.
            //if (!isUpgraded)
            if (!_gm.amountUpgrade)
            {
                _inventory.greenTrees++;
                //_doTweenMessage.ShowUI("+ 1");
                _doTweenMessage.ShowUIWood("+ 1");
            }

            // Sino junta de a 10 (SI EST� EN LA QUEST 6 SUMA DE A UNO PARA EL CONTADOR)
            else
            {
                //_inventory.greenTrees += 10;
                //_doTweenMessage.ShowUI("+ 10");
                int random = Random.Range(2, 5);
                _inventory.greenTrees += random;
                //_doTweenMessage.ShowUI(random.ToString());
                _doTweenMessage.ShowUIWood(random.ToString());
            }
        }

        // Si es del arbol p�rpura, usa la misma l�gica que la del verde.
        else
        {
            //if (!isUpgraded)
            if (!_gm.amountUpgrade)
            {
                _inventory.purpleTrees++;
                //_doTweenMessage.ShowUI("+ 1");
                _doTweenMessage.ShowUIWood("+ 1");
            }

            else
            {
                //_inventory.purpleTrees += 10;
                //_doTweenMessage.ShowUI("+ 10");
                int random = Random.Range(2, 5);
                _inventory.greenTrees += random;
                //_doTweenMessage.ShowUI(random.ToString());
                _doTweenMessage.ShowUIWood(random.ToString());
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var dog = other.GetComponent<Dog>();

        if (dog != null)
        {
            _trolleyNavMesh.enabled = false;
            UpgradeTrolley();
            _myCol.enabled = false;

            for (int i = 0; i < _logs.Length; i++)
            {
                //_doTweenManager.JumpEffect(_logs[i].transform, _trolley.gameObject.transform.position, _jumpForce, _counts, _duration);
                _doTweenManager.JumpEffect(_logs[i].transform, _posJump.transform.position, _jumpForce, _counts, _duration);
            }

            StartCoroutine(RecollectLogs());
        }
    }
    
    private IEnumerator RecollectLogs()
    {
        yield return new WaitForSeconds(_duration);

        for (int i = 0; i < _logs.Length; i++)
        {
            _logs[i].transform.position = _initiaPos[i];
        }

        if (_myCol is BoxCollider)
        {
            ((BoxCollider)_myCol).size = _initialSize;
        }

        _myCol.enabled = true;
        _trolleyNavMesh.enabled = true;
        gameObject.SetActive(false);
    }


    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();

        if (player != null)
        {
            if (!Input.GetKeyDown(_buttonInteractive))
            {
                _iconInteractive.SetActive(true);
                _iconInteractive.transform.DOScale(1.25f, 0.5f);
            }
                
            else
            {
                _iconInteractive.SetActive(false);

                Vector3 newSize = new Vector3(8f, 8f, 8f);

                if (_myCol is BoxCollider)
                {
                    ((BoxCollider)_myCol).size = newSize;
                }
            }
                
                
                
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null) _iconInteractive.transform.DOScale(0f, 0.5f);
    }
}