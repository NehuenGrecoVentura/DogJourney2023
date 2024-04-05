using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject _mainMenu, _options;
    [SerializeField] Image _backgroundButton;
    [SerializeField]
    private bool _isOptions = false;

    [SerializeField] TMP_Text _txtPressButton;
    [SerializeField] TMP_Text[] _txtsMainMenu;
    [SerializeField] TMP_FontAsset _styleButtonEnter;
    [SerializeField] TMP_FontAsset _styleButtonExit;

    private void Start()
    {
        _txtPressButton.rectTransform.localScale = Vector3.zero;
        _txtPressButton.gameObject.SetActive(true);
        _txtPressButton.rectTransform.DOScale(1.3f, 2f).OnComplete(LoopScale);
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
            Destroy(_txtPressButton);
            _mainMenu.gameObject.SetActive(true);
            //_backgroundButton.gameObject.SetActive(true);
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

    private void LoopScale()
    {
        // Escalar en un loop entre 1.3f y 1.5f
        Sequence sequence = DOTween.Sequence();
        sequence.Append(_txtPressButton.rectTransform.DOScale(1.3f, 2f))
            .Append(_txtPressButton.rectTransform.DOScale(1.5f, 2f))
            .SetLoops(-1, LoopType.Yoyo); // -1 para un loop infinito
    }

    public void ButtonSelect()
    {

    }
}