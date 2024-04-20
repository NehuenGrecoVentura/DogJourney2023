using UnityEngine;

public class TreeFall : MonoBehaviour
{
    [SerializeField] float _timeToOff = 2.5f;
    private float _initialTime;

    [SerializeField] Trunks _trunks;
    [SerializeField] ParticleSystem _smokeParticle;
    [SerializeField] SaplingTree _sapling;

    public bool _isFall;

    void Start()
    {
        _initialTime = _timeToOff;
    }

    private void Update()
    {
        if (_isFall)
        {
            _timeToOff -= Time.deltaTime;

            if (_timeToOff <= 0)
            {
                _timeToOff = 0;
                _smokeParticle.Play();
                _trunks.gameObject.SetActive(true);
                _sapling.gameObject.SetActive(true);
                _isFall = false;
            }
        }

        else
        {
            _timeToOff = _initialTime;
            gameObject.SetActive(false);
        }
    }
}