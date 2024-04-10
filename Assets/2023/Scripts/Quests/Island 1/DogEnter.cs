using System.Collections;
using UnityEngine;

public class DogEnter : MonoBehaviour
{
    [SerializeField] Transform _posSpawn;
    [SerializeField] GameObject _prefab;
    [SerializeField] KeyCode _keyInteract = KeyCode.Q;
    [SerializeField] Dog _dog;
    [SerializeField] Transform _enterPos;
    [SerializeField] Transform _exitPos;
    private Collider _myCol;
    private QuestUI _questUI;

    private void Awake()
    {
        _myCol = GetComponent<Collider>();
        _questUI = FindObjectOfType<QuestUI>();
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

    private IEnumerator Search()
    {
        Destroy(_myCol);
        _dog.gameObject.SetActive(false);
        yield return new WaitForSeconds(5f);
        _dog.gameObject.SetActive(true);
        Instantiate(_prefab, _posSpawn);
        _dog._target.transform.position = _exitPos.position;
        _dog.OrderGo();
        _questUI.TaskCompleted(1);
        _questUI.AddNewTask(2, "Returns the broom to its owner");
    }
}