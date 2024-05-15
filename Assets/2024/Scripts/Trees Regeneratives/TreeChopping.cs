using UnityEngine;

public class TreeChopping : MonoBehaviour
{
    [SerializeField] GameObject _treeFall;
    [SerializeField] Trunks _trunks;
    [SerializeField] SaplingTree _sapling; 

    private void Start()
    {
        _treeFall.SetActive(false);
        _trunks.gameObject.SetActive(false);
        _sapling.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _treeFall.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}