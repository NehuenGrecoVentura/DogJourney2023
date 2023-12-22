using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Construct : MonoBehaviour, IRotate
{
    [Header("ICONS CONFIG")]
    [SerializeField] GameObject _icons;
    [SerializeField] float _speedRot;

    [Header("AMOUNT CONFIG")]
    public int amountWood;
    public int amountNails;
    public int amountRopes;
    Inventory _inventory;

    [Header("OBJECT TO CONSTRUCT CONFIG")]
    [SerializeField] GameObject _object;
    public KeyCode buttonInteractive = KeyCode.Space;

    [Header("OBJECTS TO DESTROY")]
    [SerializeField] GameObject[] _objDestroy;

    [Header("SLIDER CONFIG")]
    [SerializeField] Slider _slider;
    public float progress = 0f;
    [SerializeField] float _timeToConstruct;

    [Header("AUDIO CONFIG")]
    [SerializeField] AudioClip _constructSound;
    AudioSource _myAudio;

    [Header("ICONS MATERIALS")]
    [SerializeField] GameObject _iconWood;
    [SerializeField] GameObject _iconNail; // TAMBIEN SIRVE PARA LA SOGA O CUALQUIER OTRO ELEMENTO QUE ESTE COMBINADO CON LA MADERA


    void Start()
    {
        _myAudio = GetComponent<AudioSource>();
        _inventory = FindObjectOfType<Inventory>();
        _icons.SetActive(false);
        _object.SetActive(false);
        _slider.gameObject.SetActive(false);
        _slider.value = 0;
    }

    private void Update()
    {
        _slider.value = progress;
    }

    private void FixedUpdate()
    {
        RotateObject(0, _speedRot, 0);
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character2022>();
        if (player != null)
        {
            if (!Input.GetKey(buttonInteractive)) _icons.SetActive(true);
            else if (Input.GetKey(buttonInteractive))
            {
                #region BRIDGE 1 & STAIRS

                if (_inventory.amountWood >= amountWood && _inventory.amountnails >= amountNails && gameObject.tag == "Bridge")
                {
                    StartCoroutine(ConstructAudio());
                    player.EjecuteAnim("Construct New");
                    _icons.SetActive(false);
                    _slider.gameObject.SetActive(true);
                    progress += Time.deltaTime * _timeToConstruct;
                    if (progress >= 100f)
                    {
                        player.EjecuteAnim("Idle");
                        _inventory.amountWood -= amountWood;
                        _inventory.amountnails -= amountNails;
                        _object.SetActive(true);
                        _slider.gameObject.SetActive(false);
                        foreach (var obj in _objDestroy) Destroy(obj.gameObject);
                    }
                }

                #endregion

                if (_inventory.amountWood >= amountWood && _inventory.amountRopes >= amountRopes && gameObject.tag != "Bridge")
                {
                    StartCoroutine(ConstructAudio());
                    player.EjecuteAnim("Construct New");
                    _icons.SetActive(false);
                    _slider.gameObject.SetActive(true);
                    progress += Time.deltaTime * _timeToConstruct;
                    if (progress >= 100f)
                    {
                        player.EjecuteAnim("Idle");
                        _inventory.amountWood -= amountWood;
                        _inventory.amountRopes -= amountRopes;
                        _object.SetActive(true);
                        _slider.gameObject.SetActive(false);
                        foreach (var obj in _objDestroy) Destroy(obj.gameObject);
                    }
                }

                if (_inventory.amountWood < amountWood) _iconWood.GetComponent<Renderer>().material.color = Color.red;
                else _iconWood.GetComponent<Renderer>().material.color = Color.white;

                if (_inventory.amountnails < amountNails || _inventory.amountRopes < amountRopes) _iconNail.GetComponent<Renderer>().material.color = Color.red;
                else _iconNail.GetComponent<Renderer>().material.color = Color.white;
            }

            else if (Input.GetKeyUp(buttonInteractive))
            {
                player.EjecuteAnim("Idle");
                _iconWood.GetComponent<Renderer>().material.color = Color.white;
                _iconNail.GetComponent<Renderer>().material.color = Color.white;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character2022>();
        if (player != null)
        {
            StopAllCoroutines();
            progress = 0;
            _icons.SetActive(false);
            _slider.gameObject.SetActive(false);
            _iconWood.GetComponent<Renderer>().material.color = Color.white;
            _iconNail.GetComponent<Renderer>().material.color = Color.white;
        }
    }

    public void RotateObject(float x, float y, float z)
    {
        transform.Rotate(x, y, z);
    }

    IEnumerator ConstructAudio()
    {
        yield return new WaitForEndOfFrame();
        if (!_myAudio.isPlaying)
            _myAudio.PlayOneShot(_constructSound);
    }
}