using UnityEngine;

public class TrunksTree : MonoBehaviour
{
    [SerializeField] float _timeToDestroy = 2.5f;
    [SerializeField] ParticleSystem _smokeTree;
    [SerializeField] FirewoodTrunk _trunks;

    private void Start()
    {
        _trunks.gameObject.SetActive(false);
    }

    void Update()
    {
        SpawnTrunks();
    }

    public void SpawnTrunks()
    {
        _timeToDestroy -= Time.deltaTime;
        if (_timeToDestroy <= 0f)
        {
            _timeToDestroy = 0f;
            _smokeTree.Play();
            _trunks.gameObject.SetActive(true);
            Restart();
            gameObject.SetActive(false);
        }
    }
    
    public void Restart()
    {
        _timeToDestroy = 2.5f;
    }
}