using UnityEngine;

public class ColorLerp : MonoBehaviour
{
    MeshRenderer meshRender;
    [SerializeField] [Range(0.1f, 1f)] float lerpTime;
    [SerializeField] Color[] colors;
    int _indexColor = 0;
    int _len;
    float t = 0;

    void Start()
    {
        meshRender = GetComponent<MeshRenderer>();
        _len = colors.Length;
    }

    void Update()
    {
        ChangeColor();
    }
    public void ChangeColor()
    {
        meshRender.material.color = Color.Lerp(meshRender.material.color, colors[_indexColor], lerpTime * Time.deltaTime);
        t = Mathf.Lerp(t, 1f, lerpTime * Time.deltaTime);
        if (t > 9f)
        {
            t = 0f;
            _indexColor++;
            _indexColor = (_indexColor >= _len) ? 0 : _indexColor;
        }
    }
}