using UnityEngine;

public class WolfRotate : MonoBehaviour
{
    public float speed;
    private float time = 5f;
    private float time2 = 5f;
    public GameObject alertIcon;
    private bool _detected = false;
    public Transform player;
    public GameManager gManager;

    private void Start()
    {
        alertIcon.gameObject.SetActive(false);
    }

    void Update()
    {
        Rotate();
    }

    void Rotate()
    {
        if (!_detected)
        {
            time -= Time.deltaTime * speed;
            if (time <= 0)
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                time2 -= speed * Time.deltaTime;
                if (time2 <= 0)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                    time = 5f;
                    time2 = 5f;
                }
            }
        }
        else transform.LookAt(player);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") _detected = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            print("El jugador escapó");
            alertIcon.gameObject.SetActive(false);
            _detected = false;

        }
    }
}
