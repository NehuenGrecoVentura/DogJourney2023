using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FOV : MonoBehaviour
{
	public float viewRadius;
	[Range(0, 360)]
	public float viewAngle;
	public LayerMask targetMask;
	public LayerMask obstacleMask;
	[HideInInspector]
	public List<Transform> visibleTargets = new List<Transform>();
	private SensorWolf _sensorWolf;
	private bool _playerDetected = false;
	public Transform player;
	public GameObject coneVision;
	private AudioSource _aS;
	public AudioClip soundDetected;


	void Start()
	{
		StartCoroutine("FindTargetsWithDelay", .2f);
		_sensorWolf = GetComponent<SensorWolf>();
		_aS = GetComponent<AudioSource>();
	}

    private void Update()
    {
        if(_playerDetected)
        {
			coneVision.GetComponent<Renderer>().material.color = Color.red;
			transform.LookAt(player);
		}

		else
        {
			coneVision.GetComponent<Renderer>().material.color = Color.yellow;
			transform.LookAt(transform.position);
		}				
	}


    IEnumerator FindTargetsWithDelay(float delay)
	{
		while (true)
		{
			yield return new WaitForSeconds(delay);
			FindVisibleTargets();
		}
	}

	void FindVisibleTargets()
	{
		visibleTargets.Clear();
		Collider[] targetsInViewRadius = Physics.OverlapSphere(transform.position, viewRadius, targetMask);
		_playerDetected = false;
		for (int i = 0; i < targetsInViewRadius.Length; i++)
		{
			Transform target = targetsInViewRadius[i].transform;
			Vector3 dirToTarget = (target.position - transform.position).normalized;
			if (Vector3.Angle(transform.forward, dirToTarget) < viewAngle / 2)
			{

				float dstToTarget = Vector3.Distance(transform.position, target.position);
				if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, obstacleMask))
				{
					visibleTargets.Add(target);
					_playerDetected = true;
					_sensorWolf.WolfIconDetected();
					_aS.PlayOneShot(soundDetected);
				}


				else
                {
					_playerDetected = false;
					_sensorWolf.ResetIconWolf();

				}

			}	
		}
	}


	public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
	{
		if (!angleIsGlobal) angleInDegrees += transform.eulerAngles.y;
		return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
	}
}
