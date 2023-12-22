using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class IntroMove : MonoBehaviour
{
    [SerializeField] Dog2022 _dogScript;
    [SerializeField] float _timeToDestroy;
    [SerializeField] Image _keyW, _keyA, _keyS, _keyD;
    [SerializeField] BoxCollider _colMail;
    [SerializeField] GameObject _iconQuestMail;
    [SerializeField] GameObject _panelMail;
    [SerializeField] TMP_Text _text;
    [SerializeField] GameObject _mapGO;
    [SerializeField] Button _buttonSellInMarket;
    private bool _w, _a, _s, _d = false;

    void Start()
    {
        _colMail.enabled = false;
        _iconQuestMail.SetActive(false);
        _panelMail.SetActive(false);

        // Desactivo al inicio al perro para que no venga junto al player al comienzo.
        _dogScript.enabled = false;

        // Por defecto al iniciar el juego, no aparece en las tiendas el boton de vender madera, sino el juego se saltea el level design de la primera quest.
        _buttonSellInMarket.gameObject.SetActive(false);
    }

    void Update()
    {
        CheckMove();
    }

    void CheckMove()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            _w = true;
            _keyW.color = Color.yellow;
        }

        else if (Input.GetKeyUp(KeyCode.W))
            _keyW.color = Color.white;

        if (Input.GetKeyDown(KeyCode.A))
        {
            _a = true;
            _keyA.color = Color.yellow;
        }
            
        else if (Input.GetKeyUp(KeyCode.A))
            _keyA.color = Color.white;

        if (Input.GetKeyDown(KeyCode.S))
        {
            _s = true;
            _keyS.color = Color.yellow;
        }
            
        else if (Input.GetKeyUp(KeyCode.S))
            _keyS.color = Color.white;

        if (Input.GetKeyDown(KeyCode.D))
        {
            _d = true;
            _keyD.color = Color.yellow;
        }
            
        else if (Input.GetKeyUp(KeyCode.D))
            _keyD.color = Color.white;

        if (_w && _a && _s && _d)
        {
            _keyW.color = Color.green;
            _keyA.color = Color.green;
            _keyS.color = Color.green;
            _keyD.color = Color.green;
            _text.color = Color.green;
            _text.text = "GOOD".ToString();
            StartCoroutine(EndTutorial());
        }        
    }

    IEnumerator EndTutorial()
    {
        while (true)
        {
            yield return new WaitForSeconds(_timeToDestroy);
            _colMail.enabled = true;
            _iconQuestMail.SetActive(true);
            _panelMail.SetActive(true);
            _mapGO.SetActive(true);
            Destroy(gameObject);
        }
    }
}