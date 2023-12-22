using System.Collections;
using UnityEngine;

public class TestCinematic : MonoBehaviour
{
    [SerializeField] Camera _camPlayer;
    private Character _player;

    private void Awake()
    {
        _player = FindObjectOfType<Character>();
    }

    public void StartCinematic(GameObject cinematic, float timeCinematic)
    {
        StartCoroutine(PlayCinematic(cinematic, timeCinematic));
    }

    private IEnumerator PlayCinematic(GameObject cinematic, float timeCinematic)
    {
        _camPlayer.gameObject.SetActive(false);
        cinematic.SetActive(true);
        _player.FreezePlayer(RigidbodyConstraints.FreezePosition);
        _player.speed = 0;
        yield return new WaitForSeconds(timeCinematic);
        _camPlayer.gameObject.SetActive(true);
        _player.FreezePlayer(RigidbodyConstraints.FreezeRotation);
        _player.speed = _player.speedAux;
        Destroy(cinematic);
    }
}