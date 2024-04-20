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
        float minX = _iconLocation.GetPixelAdjustedRect().width / 2;
        float maxX = Screen.width - minX;
        float minY = _iconLocation.GetPixelAdjustedRect().height / 2;
        float maxY = Screen.height - minY;
        Vector2 pos = Camera.main.WorldToScreenPoint(target.position + _offset);

        if (Vector3.Dot((target.position - transform.position), transform.forward) < 0)
        {
            if (pos.x < Screen.width / 2) pos.x = maxX;
            else pos.x = minX;
        }

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);
        _iconLocation.transform.position = pos;
        _distNumber.text = ((int)Vector3.Distance(target.position, transform.position)).ToString() + "m";
        //HideMap();
    }

    public void StatusRadar(bool active)
    {
        _canvasRadar.SetActive(active);
    }

    private void HideMap()
    {
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