using UnityEngine;

public class AreaQuest2 : TestCinematic
{
    [Header("CINEMATIC")]
    [SerializeField] GameObject _cinematicRabbit;
    [SerializeField] float _timeCinematic = 3f;

    [Header("MESSAGE SLIDE")]
    [SerializeField] Sprite _iconRabbit;
    [SerializeField] string _messageText;
    [SerializeField] MessageSlide _messageSlide;

    private void Start()
    {
        _cinematicRabbit.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<Character>();
        if (player != null)
        {
            _messageSlide.ShowMessage(_messageText, _iconRabbit);
            _cinematicRabbit.SetActive(true);
            StartCinematic(_cinematicRabbit, _timeCinematic);
            Destroy(this, 3.1f);
        }
    }
}