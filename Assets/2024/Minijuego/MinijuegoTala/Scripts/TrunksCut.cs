using System.Collections;
using UnityEngine;

public class TrunksCut : MonoBehaviour
{
    [SerializeField] float _timeActive = 0.5f;
    private Vector3[] _initialPos;
    private GameObject[] _trunksCuts;

    void Start()
    {
        int childCount = transform.childCount;
        _trunksCuts = new GameObject[childCount];
        _initialPos = new Vector3[childCount];

        for (int i = 0; i < childCount; i++)
        {
            _trunksCuts[i] = transform.GetChild(i).gameObject;
            _initialPos[i] = _trunksCuts[i].transform.position;
        }
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(_timeActive);

        for (int i = 0; i < _trunksCuts.Length; i++)
        {
            _trunksCuts[i].transform.position = _initialPos[i];
        }

        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        StartCoroutine(Timer());
    }
}