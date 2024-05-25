using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System.Linq;

public class BulleyeManager : MonoBehaviour
{
    [SerializeField] TMP_Text _textTime;
    [SerializeField] TMP_Text _textScore;
    [SerializeField] Transform _containerBullEyes;
    private Transform[] _allBullEyes;

    [SerializeField] float _time = 60f;
    [SerializeField] float _timeActive = 5f;
    public int score = 0;
    private bool _isActive = false;

    private void Awake()
    {
        _allBullEyes = _containerBullEyes.GetComponentsInChildren<Transform>().Skip(1).ToArray();
    }

    void Start()
    {
        StartCoroutine(BeginPlay());
    }

    void Update()
    {
        _textTime.text = "Time: " + _time.ToString();
        _textScore.text = "Score: " + score.ToString();

        _time -= Time.deltaTime;

        if (_time <= 0)
        {
            _time = 0;
        }
    }

    private IEnumerator BeginPlay()
    {
        score = 0;
        Vector3 initialRot = new Vector3(-90, 0, 60);
        Vector3 normalRot = new Vector3(0, 60, 0);

        foreach (Transform bullEye in _allBullEyes)
        {
            bullEye.DORotate(initialRot, 0f).SetEase(Ease.Linear);
        }


        yield return new WaitForSeconds(2f);

        foreach (Transform bullEye in _allBullEyes)
        {
            bullEye.DORotate(normalRot, 0.5f).SetEase(Ease.Linear);
        }

        _isActive = true;
    }
}