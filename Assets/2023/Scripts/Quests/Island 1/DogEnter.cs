using System.Collections;
using UnityEngine;

public class DogEnter : MonoBehaviour
{
    [SerializeField] GameObject _broomPrefab;
    [SerializeField] Transform _posSpawn;
    [SerializeField] KeyCode _keyInteract = KeyCode.Q;
    [SerializeField] Dog _dog;
    [SerializeField] Transform _enterPos;
    [SerializeField] Transform _exitPos;
    
    private Collider _myCol;
    private QuestUI _questUI;
    public bool broomPicked = false;
    private Manager _gm;
    private LocationQuest _radar;

    [Header("NEXT QUEST")]
    [SerializeField] Collider _colTableQuest;
    private QuestBroom _maryNPC;
    private TableQuest _nextQuest;

    private void Awake()
    {
        _myCol = GetComponent<Collider>();
        _questUI = FindObjectOfType<QuestUI>();
        _gm = FindObjectOfType<Manager>();
        _radar = FindObjectOfType<LocationQuest>();
        _maryNPC = FindObjectOfType<QuestBroom>();
        _nextQuest = FindObjectOfType<TableQuest>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var dog = other.GetComponent<Dog>();
        if (dog != null)
        {
            print("EL PERRO ESTÁ BUSCANDO DENTRO");
            StartCoroutine(Search());
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && broomPicked)
            ActiveNextQuest();
    }

    public void ActiveNextQuest()
    {
        _radar.target = _nextQuest.gameObject.transform;
        Destroy(_broomPrefab);
        _gm.QuestCompleted();
        _colTableQuest.enabled = true;
        Destroy(_maryNPC);
        Destroy(gameObject);
    }

    private IEnumerator Search()
    {
        Destroy(_myCol);
        _dog.gameObject.SetActive(false);
        yield return new WaitForSeconds(5f);
        _radar.target = _maryNPC.gameObject.transform;
        _dog.gameObject.SetActive(true);
        _broomPrefab.SetActive(true);
        _dog._target.transform.position = _exitPos.position;
        _dog.OrderGo();
        _questUI.TaskCompleted(1);
        _questUI.AddNewTask(2, "Returns the broom to its owner");
        broomPicked = true;
    }
}