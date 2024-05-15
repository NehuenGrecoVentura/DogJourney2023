using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPause : MonoBehaviour
{
    [SerializeField] GameObject _canvas;
    [SerializeField] GameObject _menuPause;
    [SerializeField] GameObject _optionsMenu;
    [SerializeField] GameObject _panel;
    [SerializeField] private bool _isFreeze, _isPause = false;

    [Header("PLAYER")]
    [SerializeField] private CameraOrbit _cam;
    [SerializeField] private Character _player;

    void Start()
    {
        _canvas.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!_isFreeze && !_isPause) ShowPause();
            else if (_isPause) Continue();
        }
    }

    public void ShowPause()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        _panel.SetActive(true);
        _canvas.SetActive(true);
        _menuPause.SetActive(true);
        _optionsMenu.SetActive(false);
        Time.timeScale = 0;
        _cam.sensitivity = new Vector2(0, 0);
        _isPause = true;
    }

    public void Continue()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _panel.SetActive(false);
        _menuPause.SetActive(false);
        _optionsMenu.SetActive(false);
        Time.timeScale = 1;
        _cam.sensitivity = new Vector2(3, 3);
        _isPause = false;
    }

    public void Options()
    {
        _panel.SetActive(true);
        _menuPause.SetActive(false);
        _optionsMenu.SetActive(true);
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void Freeze()
    {
        _player.speed = 0;
        _player.FreezePlayer();
        _isFreeze = true;
        _cam.sensitivity = new Vector2(0,0);
    }

    public void Defreeze()
    {
        _player.speed = _player.speedAux;
        _player.DeFreezePlayer();
        _isFreeze = false;
        _cam.sensitivity = new Vector2(3, 3);
    }
}