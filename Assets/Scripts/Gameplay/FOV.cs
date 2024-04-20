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


}
