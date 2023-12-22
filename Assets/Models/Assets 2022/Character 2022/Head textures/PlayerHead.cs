using System.Collections;
using UnityEngine;

public class PlayerHead : MonoBehaviour
{
    [SerializeField] float _timeInCloseEyes = 0.5f;
    [SerializeField] Material _openEyes;
    [SerializeField] Material _closeEyes;
    private MeshRenderer _myMesh;

    private void Awake()
    {
        _myMesh = GetComponent<MeshRenderer>();
    }

    void Start()
    {
        StartCoroutine(Eyes());
    }

    IEnumerator Eyes()
    {
        while (true)
        {
            yield return new WaitForSeconds(_timeInCloseEyes);
            _myMesh.material = _closeEyes;
            yield return new WaitForSeconds(0.1f);
            _myMesh.material = _openEyes;
        }
    }
}