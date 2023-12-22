using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DogOrder2022 : MonoBehaviour
{
    public List<Dog2022> dog;
    public bool orderBlock = false;
    int _dogCount, dogNumber;
    [SerializeField] ParticleSystem _iconDogCall, _iconDogStop, _iconDogReunion, _iconDogBlock;

    [Header("SOUNDS")]
    [SerializeField] AudioSource _audioTrolley;
    [SerializeField] AudioClip _soundCallDog;
    [SerializeField] AudioClip _soundStopDog;
    [SerializeField] AudioSource _soundDogFeedbackOrder;
    AudioSource _myAudio;
    [SerializeField] Character2022 _player;

    [Header("COUNTDOWN CONFIG")]
    [SerializeField] float _countDown = 1.5f;
    float _initialCountDown;
    bool _countDownActive = false;
    private Upgrade[] _upgrade;

    private void Awake()
    {
        _dogCount = dog.Count;
        dogNumber = 0;
        _iconDogCall.Stop();
        _iconDogStop.Stop();
        _iconDogReunion.Stop();
        _myAudio = GetComponent<AudioSource>();
        _audioTrolley.Stop();
        _upgrade = FindObjectsOfType<Upgrade>();
    }

    private void Start()
    {
        _initialCountDown = _countDown;
    }

    void Update()
    {
        CallDog();
        CountDown();

        if (Input.GetKeyDown(KeyCode.Alpha1)) dogNumber = 0;

        foreach (var upgrade in _upgrade)
        {
            if (upgrade.dogsAdded)
            {
                if (dog.Count >= 2 && Input.GetKeyDown(KeyCode.Alpha2))
                    dogNumber = 1;
            }
        }
    }

    void CallDog()
    {
        if (Input.GetKeyDown(KeyCode.Q) && _countDown >= 1.5f && !orderBlock)
        {
            _player.EjecuteAnim("Silbido Idle");
            dog[dogNumber].OrderGO();
            _iconDogCall.Play();
            _myAudio.pitch = Random.Range(0.8f, 1.1f);
            _myAudio.PlayOneShot(_soundCallDog);
            _audioTrolley.Play();
            _soundDogFeedbackOrder.Play();
            _countDownActive = true;
        }

        else if (Input.GetKeyDown(KeyCode.Q) && _countDown >= 1.5f && orderBlock)
        {
            _player.EjecuteAnim("Silbido Idle");
            _iconDogBlock.Play();
            _myAudio.pitch = Random.Range(0.8f, 1.1f);
            _myAudio.PlayOneShot(_soundCallDog);
            _countDownActive = true;
        }


        if (Input.GetKeyDown(KeyCode.E) && _countDown >= 1.5f && !orderBlock)
        {
            dog[dogNumber].OrderStay();
            _iconDogStop.Play();
            _myAudio.PlayOneShot(_soundStopDog);
            _countDownActive = true;
        }


        if (Input.GetKeyDown(KeyCode.R) && _countDown >= 1.5f && !orderBlock)
        {
            for (int i = 0; i < _dogCount; i++)
            {

                foreach (var upgrade in _upgrade)
                {
                    if (upgrade.dogsAdded)
                    {
                        dog[i].OrderGO();
                        _player.EjecuteAnim("Silbido Idle");
                        _iconDogCall.Play();
                        _myAudio.pitch = Random.Range(0.8f, 1.1f);
                        _myAudio.PlayOneShot(_soundCallDog);
                        _audioTrolley.Play();
                        _soundDogFeedbackOrder.Play();
                        _countDownActive = true;
                    }
                } 
            }
        }

        #region ANIMS SILBIDOS 

        if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) && Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E) || Input.GetKey(KeyCode.E))
            _player.EjecuteAnim("Silbido Run");

        else if ((!Input.GetKey(KeyCode.W) || !Input.GetKey(KeyCode.A) || !Input.GetKey(KeyCode.S) || !Input.GetKey(KeyCode.D)) && Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E) || Input.GetKey(KeyCode.E))
            _player.EjecuteAnim("Silbido Idle");

        #endregion
    }

    void CountDown()
    {
        if (_countDownActive)
        {
            _countDown -= Time.deltaTime;
            if (_countDown <= 0)
            {
                _countDown = _initialCountDown;
                _countDownActive = false;
            }
        }
    }

    IEnumerator CoolDown()
    {
        while (_countDownActive)
        {
            yield return new WaitForSeconds(1.5f);
            _countDownActive = false;
        }
    }
}