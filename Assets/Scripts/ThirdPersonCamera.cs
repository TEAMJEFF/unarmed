using System.Collections;
using System.Collections.Generic;
//using EZCameraShake;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour {
	private const float yAngleMin = 0.0f;
	private const float yAngleMax = 30.0f;

	public Transform lookAt;
	public Transform camTransform;
	private Rigidbody rb;

	static Camera cam;
	static Fisheye fshh;

	private RaycastHit hit;
	public bool attached = false;

	private static float distance = 7.0f;
	private static float rate = 0.08f;
	private static float camHeight = 3.0f;

	const float closeHeight = 2.0f;
	const float farHeight = 3.0f;
	const float closeDist = 6.0f;
	const float farDist = 7.0f;

	private float currentX = 0.0f;
	private float currentY = 0.0f;
	private float sensX = 4.0f;
	private float sensY = 1.0f;

	private float momentum;
	public float speed;
	private float step;

	static AudioSource bgm;
	static AudioSource heartbeat;

	private void Start()
	{
		camTransform = transform;
		cam = Camera.main;
		rb = GetComponent<Rigidbody>();

		fshh = cam.GetComponent<Fisheye> ();
		AudioSource[] audios = cam.GetComponents<AudioSource>();
		bgm = audios [0];
		heartbeat = audios [1];
	}

	private void Update()
	{
		//currentX += Input.GetAxis("Mouse X");
		//currentY += Input.GetAxis("Mouse Y");

		//currentY = Mathf.Clamp(currentY, yAngleMin, yAngleMax);
	}

	private void LateUpdate()
	{
		Vector3 dir = new Vector3(0, camHeight, -distance);
		Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
		camTransform.position = lookAt.position + rotation * dir;
		camTransform.LookAt(lookAt.position);
	}

	void Shake()
	{
		Debug.Log("Gets here");
//		CameraShaker.Instance.ShakeOnce(4f, 4f, .1f, 0.5f);
	}

	static public void ZoomIn() {
		distance = Mathf.Lerp(distance, closeDist, rate);
		camHeight = Mathf.Lerp (camHeight, closeHeight, rate);

		bgm.volume = Mathf.Lerp (bgm.volume, 0.10f, 0.04f);
		heartbeat.volume = Mathf.Lerp (heartbeat.volume, 1.0f, 0.1f);

		fshh.strengthX = Mathf.Lerp (fshh.strengthX, 0.3f, 0.08f);

	}

	static public void ZoomOut() {
		distance = Mathf.Lerp(distance, farDist, rate);
		camHeight = Mathf.Lerp (camHeight, farHeight, rate);

		bgm.volume = Mathf.Lerp (bgm.volume, 0.9f, 0.08f);
		heartbeat.volume = Mathf.Lerp (heartbeat.volume, 0.0f, 0.08f);

		fshh.strengthX = Mathf.Lerp (fshh.strengthX, 0.0f, 0.08f);

	}

}