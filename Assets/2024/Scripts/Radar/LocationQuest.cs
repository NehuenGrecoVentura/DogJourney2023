using UnityEngine;
using UnityEngine.UI;

public class LocationQuest : MonoBehaviour
{
    [Header("VISUAL CONFIG")]
    [SerializeField] Image _iconLocation;
    [SerializeField] Text _distNumber;
    [SerializeField] Vector3 _offset;
    public Transform target;

    [Header("HIDE CONFIG")]
    [SerializeField] Transform _playerPos;
    [SerializeField] float _distanceHide;
    [SerializeField] GameObject _canvasRadar;
    private Character _player;

    private void Awake()
    {
        _player = FindObjectOfType<Character>();
    }

    void Update()
    {
        TargetPos();
        //HideMap(_distanceHide); PROBAR EN VEZ DE IMAGE.GAMEOBJECT, DIRECTAMENTE CON EL CANVAS CONTENEDOR
    }

    void TargetPos()
    {
        //float minX = _iconLocation.GetPixelAdjustedRect().width / 2;
        //float maxX = Screen.width - minX;
        //float minY = _iconLocation.GetPixelAdjustedRect().height / 2;
        //float maxY = Screen.height - minY;
        //Vector2 pos = Camera.main.WorldToScreenPoint(target.position + _offset);

        //if (Vector3.Dot((target.position - transform.position), transform.forward) < 0)
        //{
        //    if (pos.x < Screen.width / 2) pos.x = maxX;
        //    else pos.x = minX;
        //}

        //pos.x = Mathf.Clamp(pos.x, minX, maxX);
        //pos.y = Mathf.Clamp(pos.y, minY, maxY);
        //_iconLocation.transform.position = pos;
        //_distNumber.text = ((int)Vector3.Distance(target.position, transform.position)).ToString() + "m";

        if (target == null) return;

        Vector3 playerPos = _player.transform.position;
        float distance = Vector3.Distance(playerPos, target.position);
        _distNumber.text = ((int)distance).ToString() + "m";

        Vector2 pos = Camera.main.WorldToScreenPoint(target.position + _offset);

        if (Vector3.Dot((target.position - playerPos), _player.transform.forward) < 0)
        {
            pos.x = (pos.x < Screen.width / 2) ? Screen.width - pos.x : pos.x;
        }

        pos.x = Mathf.Clamp(pos.x, 0, Screen.width);
        pos.y = Mathf.Clamp(pos.y, 0, Screen.height);

        _iconLocation.rectTransform.position = pos;
    }

    public void StatusRadar(bool active)
    {
        _canvasRadar.SetActive(active);
    }

    private void HideMap()
    {
        if (target != null) return;
        float dist = Vector3.Distance(transform.position, target.position);

        if (dist <= _distanceHide)
        {
            _iconLocation.enabled = false;
            _distNumber.enabled = false;
        }

        else
        {
            _iconLocation.enabled = true;
            _distNumber.enabled = true;
        }
    }
}