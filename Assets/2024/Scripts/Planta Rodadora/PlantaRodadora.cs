using System.Collections;
using UnityEngine;

public class PlantaRodadora : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _rotSpeed;
    [SerializeField] private float _timeRespawn;
    private Vector3 _dir;

    void Start()
    {
        StartCoroutine(Play());
    }

    void Update()
    {
        transform.Rotate(Vector3.forward * _rotSpeed * Time.deltaTime);
        //transform.Translate(-Vector3.right * _speed * Time.deltaTime);
        transform.Translate(_dir * _speed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, 1.095f, transform.position.z);
    }

    private IEnumerator Play()
    {
        while (true)
        {
            _dir.x = 1;
            transform.Rotate(Vector3.forward * _rotSpeed * Time.deltaTime);

            yield return new WaitForSeconds(_timeRespawn);
            _dir.x = -1;
            yield return new WaitForSeconds(_timeRespawn);
            _dir.x = 1;
        }
    }
}