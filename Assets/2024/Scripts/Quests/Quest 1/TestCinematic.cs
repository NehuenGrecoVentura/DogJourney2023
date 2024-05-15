using System.Collections;
using UnityEngine;

public class TestCinematic : MonoBehaviour
{
    [SerializeField] Camera _camPlayer;
    private Character _player;
    private QuestUI _questUI;

    private void Awake()
    {
        _player = FindObjectOfType<Character>();
        _questUI = FindObjectOfType<QuestUI>();
    }

    public void StartCinematic(GameObject cinematic, float timeCinematic)
    {
        StartCoroutine(PlayCinematic(cinematic, timeCinematic));
    }

    private IEnumerator PlayCinematic(GameObject cinematic, float timeCinematic)
    {
        _questUI.UIStatus(false);
        _camPlayer.gameObject.SetActive(false);
        cinematic.SetActive(true);
        _player.FreezePlayer();
        _player.speed = 0;
        _player.PlayAnim("Idle");
        yield return new WaitForSeconds(timeCinematic);
        _questUI.UIStatus(true);
        _camPlayer.gameObject.SetActive(true);
        _player.DeFreezePlayer();
        _player.speed = _player.speedAux;
        Destroy(cinematic);
    }
}