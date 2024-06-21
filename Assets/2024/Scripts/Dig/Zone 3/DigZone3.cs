using UnityEngine;
using UnityEngine.UI;

public class DigZone3 : MonoBehaviour
{
    [Header("INTERACT")]
    [SerializeField] KeyCode _keyInteract = KeyCode.Mouse0;
    [SerializeField] NPCHouses _quest;

    [SerializeField] float _hp;
    [SerializeField] CharacterInventory _inventory;
    [SerializeField] Slider _slider;
    [SerializeField] Image _sliderImage;
    [SerializeField] AudioSource _myAudio;

    void Start()
    {
        _slider.maxValue = _hp;
        _slider.value = _slider.maxValue;
        _slider.gameObject.SetActive(false);
        _sliderImage.color = Color.green;
    }

    private void FocusToDig(Character player)
    {
        Vector3 pos = new Vector3(transform.position.x, player.transform.position.y, transform.position.z);
        player.gameObject.transform.LookAt(pos);
    }

    private void Stop(Character player)
    {
        _myAudio.Stop();
        player.isConstruct = false;
        player.DeFreezePlayer();
        player.MainAnim();
    }

    private void HealthBar(Character player)
    {
        _slider.value = _hp;
        Color color;

        if (_slider.value >= 60f) color = Color.green;
        else if (_slider.value >= 30f) color = Color.yellow;
        else color = Color.red;
        _sliderImage.color = color;

        if (_slider.value <= 0)
        {
            _hp = 0;
            _slider.value = 0;
            Stop(player);
            _quest.ItemFound();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null) _slider.gameObject.SetActive(true);
    }

    private void OnTriggerStay(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null && Input.GetKey(_keyInteract) && _inventory.shovelSelected)
        {
            if (!_myAudio.isPlaying) _myAudio.Play();
            FocusToDig(player);
            player.HitDig();
            _hp--;
            HealthBar(player);
        }

        else if (player != null && !Input.GetKey(_keyInteract)) Stop(player);
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            _slider.gameObject.SetActive(false);
            Stop(player);
        }
    }
}