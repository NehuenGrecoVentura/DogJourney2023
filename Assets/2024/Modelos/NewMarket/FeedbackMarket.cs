using System.Collections;
using UnityEngine;
using TMPro;

public class FeedbackMarket : MonoBehaviour
{
    [SerializeField] private GameObject[] _icons;
    [SerializeField] private TMP_Text[] _texts;
    [SerializeField] private Animator _anim;
    [SerializeField] private float _timeInScreen = 4f;

    void Start()
    {
        foreach (var icons in _icons)
            icons.SetActive(false);

        foreach (var text in _texts)
            text.gameObject.SetActive(false);
    }

    public void ActiveTutorialDogs()
    {
        _icons[8].gameObject.SetActive(true);
    }

    public void Message(string info, string percent, bool iconDog, bool iconTrolley, bool iconTree,
    bool iconAxe, bool iconSpeedPlayer, bool iconSpeedDog, bool canvasDogSelection, bool tutorialDogs, bool newDog)
    {
        foreach (var text in _texts)
            text.gameObject.SetActive(true);

        _icons[7].gameObject.SetActive(true);
        InAnim();
        StartCoroutine(StarAnim());
        _texts[0].text = info;
        _texts[1].text = percent;
        _icons[0].gameObject.SetActive(iconDog);
        _icons[1].gameObject.SetActive(iconTrolley);
        _icons[2].gameObject.SetActive(iconTree);
        _icons[3].gameObject.SetActive(iconAxe);
        _icons[4].gameObject.SetActive(iconSpeedPlayer);
        _icons[5].gameObject.SetActive(iconSpeedDog);
        _icons[6].gameObject.SetActive(canvasDogSelection);
        _icons[8].gameObject.SetActive(tutorialDogs);
        _icons[10].gameObject.SetActive(newDog);
        //_icons[9].gameObject.SetActive(true); // PARTICULA LUMINOSA AL PLAYER QUE SE HACE UNA VEZ QUE COMPRAMOS UN UPGRADE

    }

    IEnumerator StarAnim()
    {
        while (true)
        {
            yield return new WaitForSeconds(_timeInScreen);
            OutAnim();
            StopAllCoroutines();
        }
    }

    #region ANIM MESSAGE

    // Animación que sale del costado una vez que compramos un upgrade
    void InAnim()
    {
        _anim.SetBool("In", true);
        _anim.SetBool("Out", false);
    }

    // Animación que se esconde del costado una vez que compramos un upgrade
    void OutAnim()
    {
        _anim.SetBool("In", false);
        _anim.SetBool("Out", true);
    }

    #endregion
}