using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Animation fadeOut;
    public Image gameOver;
    public Rigidbody rbPlayer;
    private AudioSource[] _aS;

    private void Start()
    {
        gameOver.gameObject.SetActive(false);
        fadeOut.Stop("FadeOut");
        _aS = FindObjectsOfType<AudioSource>();
    }

    public void GameOver()
    {
        fadeOut.Play("FadeOut");
        gameOver.gameObject.SetActive(true);
        rbPlayer.constraints = RigidbodyConstraints.FreezeAll;
        foreach (var audio in _aS)
        {
            audio.Stop();
        }
    }
}