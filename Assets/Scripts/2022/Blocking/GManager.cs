using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GManager : MonoBehaviour
{
    [HideInInspector] public AudioSource _myAudio;
    public AudioClip _winSound;
    public Text _winText;
    Character2022 _player;
    public int level = 0;
    public float timeTextInScreen = 2f;

    [Header("RESTART CONFIG")]
    [SerializeField] GameObject _canvasGamveOver;
    public Transform _posCheckpoint;
    public bool gameOver = false;

    [SerializeField] GameObject _canvasLoadBar;
    [SerializeField] GameObject _fadeAnim;

    CameraOrbit _cam;

    private void Awake()
    {
        _myAudio = GetComponent<AudioSource>();
        _player = FindObjectOfType<Character2022>();
        _cam = FindObjectOfType<CameraOrbit>();
    }

    void Start()
    {
        _winText.gameObject.SetActive(false);
        _canvasGamveOver.SetActive(false);
        _canvasLoadBar.SetActive(false);
        _fadeAnim.SetActive(false);
    }

    public void LevelCompleted()
    {
        level++;
        _player.EjecuteAnim("Win");
        _myAudio.PlayOneShot(_winSound);
        _winText.gameObject.SetActive(true);
        StartCoroutine(WinTimeInScreen());
    }

    IEnumerator WinTimeInScreen()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeTextInScreen);
            _winText.gameObject.SetActive(false);
            StopCoroutine(WinTimeInScreen());
        }
    }

    public void GameOver()
    {
        gameOver = true;
        _canvasGamveOver.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        _cam.sensitivity = new Vector2(0, 0);
    }

    public void RestartCheckpoint()
    {
        //_player.GetComponent<Animator>().SetFloat("Speed", 0);
        _fadeAnim.SetActive(true);
        StartCoroutine(FadeAnimRestart());
        _player.gameObject.transform.position = _posCheckpoint.position;
        _cam.sensitivity = new Vector2(3, 3);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        _canvasGamveOver.SetActive(false);
        gameOver = false;
    }

    IEnumerator FadeAnimRestart() // Esta corrutina es muy importante, NO BORRAR, funciona como fix para hacer que no bloquee la pantalla en el momento de usar la tienda o el menu de pausa.
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            _fadeAnim.SetActive(false);
            //_player.GetComponent<Animator>().SetFloat("Speed", 0);
            //_player.GetComponent<Animator>().Play("Idle");
            StopCoroutine(FadeAnimRestart());
        }
    }
}