using UnityEngine;
using UnityEngine.SceneManagement;


public class Pause : MonoBehaviour
{
    [SerializeField] GameObject _canvasPause;
    [SerializeField] GameObject _canvasOptionsPause;
    [SerializeField] GameObject _menuMain;
    [SerializeField] GameObject _canvas;
    bool _isPause;
    bool _isFreeze; // Esta variable sirve para freezear la pausa en los mails y en las tiendas.
    CameraOrbit _cam;

    private void Awake()
    {
        _cam = FindObjectOfType<CameraOrbit>();
    }

    void Start()
    {
        HidePause();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!_isPause && !_isFreeze) ShowPause();
            //else HidePause();
            else if (_isPause) HidePause();
        }
    }

    //void PauseState(bool canvas, bool cursorVisible, CursorLockMode cursorMode, int timeScale, bool isPause)
    //{
    //    _canvasPause.SetActive(canvas);
    //    Cursor.visible = cursorVisible;
    //    Cursor.lockState = cursorMode;
    //    Time.timeScale = timeScale;
    //    _isPause = isPause;
    //}

    public void ShowPause()
    {
        //PauseState(true, true, CursorLockMode.None, 0, true);
        //_canvasOptionsPause.SetActive(false);
        //_menuMain.SetActive(true);
        //_cam.sensitivity = new Vector2(0, 0);
        
        _canvas.SetActive(true);
        _menuMain.SetActive(true);
        _canvasOptionsPause.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        _cam.enabled = false;
        Time.timeScale = 0;
    }

    public void HidePause()
    {
        //PauseState(false, false, CursorLockMode.Locked, 1, false);
        //_canvasOptionsPause.SetActive(false);
        //_cam.sensitivity = new Vector2(3, 3);



        _canvas.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        _cam.enabled = true;
        Time.timeScale = 1;
        _isPause = false;
    }

    public void ShowOptions()
    {
        _canvas.SetActive(true);
        _menuMain.SetActive(false);
        _canvasOptionsPause.SetActive(true);
    }

    void FreezeState(int timeScale, bool freeze, Vector2 mouseSensitivity)
    {
        Time.timeScale = timeScale;
        _isFreeze = freeze;
        _cam.sensitivity = mouseSensitivity;
    }

    public void Freeze()
    {
        FreezeState(0, true, new Vector2(0,0));
    }

    public void Defrize()
    {
        FreezeState(1, false, new Vector2(3,3));
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void BackToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}