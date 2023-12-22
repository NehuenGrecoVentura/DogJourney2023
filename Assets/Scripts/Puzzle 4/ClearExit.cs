using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearExit : MonoBehaviour
{
    public GeiserPuzzle4 buttonUp2;
    public GeiserPuzzle4 buttonUp3;
    public GeiserPuzzle4 buttonUp4;
    public GameObject geiserExit1;
    public GameObject geiserExit2;
    public GameObject wolfExit1;
    public GameObject wolfExit2;
    public Camera mainCamera;
    public Camera cameraWolfs;
    private float timeCam = 3;
    public float forceGeiserWolfs;
    public ClearExit myScript;
    public GameObject[] sensorsWolfs;
    private float _rotateExitWolf = 5f;

    private void Start()
    {
        geiserExit1.gameObject.SetActive(false);
        geiserExit2.gameObject.SetActive(false);
        cameraWolfs.enabled = false;
    }

    private void Update()
    {
        if(buttonUp3.button3On && buttonUp4.button4On && buttonUp2.button2On)
        {
            foreach (var sensor in sensorsWolfs) sensor.gameObject.SetActive(false);
            mainCamera.gameObject.SetActive(false);
            cameraWolfs.enabled = true;
            geiserExit1.gameObject.SetActive(true);
            geiserExit2.gameObject.SetActive(true);
            wolfExit1.GetComponent<WolfLvl4>().enabled = false;
            wolfExit2.GetComponent<WolfLvl4>().enabled = false;
            wolfExit1.transform.position += transform.forward * forceGeiserWolfs * Time.deltaTime;
            wolfExit2.transform.position += transform.forward * forceGeiserWolfs * Time.deltaTime;

            wolfExit1.transform.rotation = Quaternion.Euler(new Vector3(0, 270, 180));
            wolfExit2.transform.rotation = Quaternion.Euler(new Vector3(0, 270, 180));
            timeCam -= Time.deltaTime;
            
            if (timeCam <= 0)
            {
                timeCam = 0;
                cameraWolfs.gameObject.SetActive(false);
                mainCamera.gameObject.SetActive(true);
                wolfExit1.gameObject.SetActive(false);
                wolfExit2.gameObject.SetActive(false);
                geiserExit1.GetComponent<ParticleSystem>().Stop();
                geiserExit2.GetComponent<ParticleSystem>().Stop();
            }
        }
    }
}