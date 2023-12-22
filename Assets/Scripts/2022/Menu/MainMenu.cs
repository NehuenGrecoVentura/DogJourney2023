using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject _pressAnyButton, _mainMenu, _options;
    [SerializeField] Image _backgroundButton;
    bool _isOptions = false;
    
    private void Start()
    {
        _pressAnyButton.gameObject.SetActive(true);
        _mainMenu.gameObject.SetActive(false);
        _backgroundButton.gameObject.SetActive(false);
        _options.SetActive(false);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    private void Update()
    {
        if (Input.anyKey && !_isOptions)
        {
            Destroy(_pressAnyButton);
            _mainMenu.gameObject.SetActive(true);
            _backgroundButton.gameObject.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Escape) && _isOptions) BackToMenu();
    }

    public void NewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void Options()
    {
        _isOptions = true;
        _mainMenu.SetActive(false);
        _options.SetActive(true);
    }

    public void BackToMenu()
    {
        _isOptions = false;
        _options.SetActive(false);
        _mainMenu.SetActive(true);
    }

    public void TestsScene()
    {
        SceneManager.LoadScene(2);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}