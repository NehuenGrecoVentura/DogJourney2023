using UnityEngine;
using UnityEngine.UI;

public class InfoIcon : MonoBehaviour
{
    [SerializeField] float _speedRot = -4f;
    [SerializeField] Text _buildBridge;
    [SerializeField] KeyCode _cheatButtonMoney = KeyCode.Backspace;
    [SerializeField] KeyCode _buttonConfirm = KeyCode.Return;

    [Header("BRIDGES")]
    [SerializeField] GameObject _bridgeNull;
    [SerializeField] GameObject _bridgeBuild;
    [SerializeField] int _amountNailsForConstruction;
    [SerializeField] int _amountWoodsForConstruction;
    Inventory _inventory;
    [SerializeField] ParticleSystem _smokeConstruction;
    [Header("SLIDER CONFIG")]

    [SerializeField] Slider _slider;
    [SerializeField] float _progress = 0f;
    [SerializeField] float timeToConstruct;
    [SerializeField] GameObject _iconsMaterialsBridge;

    [Header("MAP CONFIG")]
    LocationQuest _map;
    [SerializeField] Transform _nextLocation;

    void Start()
    {
        _slider.gameObject.SetActive(false);
        _smokeConstruction.Stop();
        _buildBridge.gameObject.SetActive(false);
        _inventory = FindObjectOfType<Inventory>();
        _bridgeNull.SetActive(true);
        _bridgeBuild.SetActive(false);
        _iconsMaterialsBridge.SetActive(false);
        _map = FindObjectOfType<LocationQuest>();
    }

    void Update()
    {
        _slider.value = _progress;
        RotateIcon(0, _speedRot, 0);
        if (Input.GetKeyDown(_cheatButtonMoney)) _inventory.money += 100;
        if (_progress > 0f && _progress < 1f) _smokeConstruction.Play();
    }

    void RotateIcon(float x, float y, float z)
    {
        transform.Rotate(x, y, z);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<Character2022>())
        {
            _iconsMaterialsBridge.SetActive(true);
            if ((_inventory.amountnails >= _amountNailsForConstruction && _inventory.amountWood >= _amountWoodsForConstruction) && !Input.GetKey(_buttonConfirm))
            {
                _iconsMaterialsBridge.SetActive(true);
                _buildBridge.gameObject.SetActive(true);
            }

            else if (Input.GetKey(_buttonConfirm) && (_inventory.amountnails >= _amountNailsForConstruction && _inventory.amountWood >= _amountWoodsForConstruction))
            {
                _slider.gameObject.SetActive(true);
                _progress += Time.deltaTime * timeToConstruct;
                if (_progress >= 1)
                {
                    _map.target = _nextLocation;
                    _inventory.amountnails -= _amountNailsForConstruction;
                    _inventory.amountWood = _amountWoodsForConstruction;
                    _progress = 0;
                    _iconsMaterialsBridge.SetActive(false);
                    _buildBridge.gameObject.SetActive(false);
                    Destroy(_bridgeNull);
                    _bridgeBuild.SetActive(true);
                    _slider.gameObject.SetActive(false);
                    Destroy(gameObject);
                }
            }

            else if (Input.GetKeyUp(_buttonConfirm))
            {
                _progress = 0;
                _slider.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Character2022>())
        {
            _iconsMaterialsBridge.SetActive(false);
            _buildBridge.gameObject.SetActive(false);
            _progress = 0;
            _slider.gameObject.SetActive(false);
        }
    }
}