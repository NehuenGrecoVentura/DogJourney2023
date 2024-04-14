using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class Inputs : MonoBehaviour
{
    private PickAxe _axe;    
    [SerializeField] GameObject[] _nextStep;
    [SerializeField] Collider _mailQuest1;
    [SerializeField] GameObject _tutorialAddDog;
    [SerializeField] GameObject _womanInMarket;
    [SerializeField] DogBall _dogBall;

    [Header("RADAR")]
    [SerializeField] Transform _nextPos;
    private LocationQuest _radar;

    [Header("ICONS CONTROLS")]
    [SerializeField] Image[] _iconsControls;
    [SerializeField] Transform[] _parentsIcons;
    [SerializeField] RectTransform _boxWASD;
    [SerializeField] RectTransform _boxZoom;

    private TutorialAddDog _dog2;

    private void Awake()
    {
        _axe = FindObjectOfType<PickAxe>();
        _radar = FindObjectOfType<LocationQuest>();
        _dog2 = FindObjectOfType<TutorialAddDog>();
    }

    void Start()
    {
        StartCoroutine(Move());
    }

    private void NextStep(bool active)
    {
        foreach (var item in _nextStep)
            item.SetActive(active);
    }

    private IEnumerator Move()
    {
        _dog2.enabled = false;
        _dog2.gameObject.SetActive(false);
        _axe.enabled = false;
        _mailQuest1.enabled = false;
        _tutorialAddDog.SetActive(false);
        _womanInMarket.SetActive(false);
        _radar.StatusRadar(false);
        _dogBall.enabled = false;
        NextStep(false);

        _boxZoom.gameObject.SetActive(false);
        _boxWASD.DOAnchorPosY(-202f, 1f);
        yield return new WaitForSeconds(3f);
        _boxWASD.DOAnchorPosY(-1000f, 1f);
        yield return new WaitForSeconds(0.5f);
        Destroy(_boxWASD.gameObject);
        _boxZoom.gameObject.SetActive(true);
        _boxZoom.DOAnchorPosY(-202f, 1f);
        yield return new WaitForSeconds(3f);
        _boxZoom.DOAnchorPosY(-1000f, 1f);
        yield return new WaitForSeconds(0.5f);
        Destroy(_boxZoom.gameObject);
        _mailQuest1.enabled = true;
        _radar.StatusRadar(true);
        _radar.target = _nextPos;
        Destroy(gameObject);
    }
}