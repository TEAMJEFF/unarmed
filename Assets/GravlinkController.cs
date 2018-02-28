using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravlinkController : MonoBehaviour {

	public const float MAXHOOKDISTANCE = 15.0f;

	public GameObject anchor;
	public LayerMask cullingmask;
	public Transform hand;

	private bool isHooked;
	private bool isHooking;

	private Rigidbody rb;
	private HingeJoint joint;
	private HingeJoint anchorJoint;
	private LineRenderer lr;

	RaycastHit hit;

	// Basis for hooking: https://forum.unity.com/threads/swinging-ninja-rope.227628/

	// Use this for initialization
	void Start () {
		lr = this.GetComponent<LineRenderer> ();
		rb = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update () {

		// If Left Click
		if (Input.GetMouseButtonDown (0)) {

			Debug.Log ("MouseDown");

			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			if (Physics.Raycast(ray, out hit, MAXHOOKDISTANCE, cullingmask)) {

				Debug.Log ("Hit");

				isHooked = true;

				// Move anchor to hitloc and attach a HingeJoint to it
				anchor.transform.position = hit.point;
				anchor.transform.rotation = Quaternion.identity;
				anchorJoint = anchor.AddComponent<HingeJoint> ();
				anchorJoint.axis = Vector3.back;
				anchorJoint.anchor = Vector3.zero;

				// Add HingeJoint to player model
				joint = gameObject.AddComponent<HingeJoint> ();
				joint.axis = Vector3.back;
				joint.anchor = Vector3.zero;
				joint.connectedBody = anchor.GetComponent<Rigidbody>();

				// Show line
				lr.enabled = true;
			}
		} else if (isHooked && Input.GetMouseButtonUp (0)) {
			Destroy (joint);
			Destroy (anchorJoint);
			lr.enabled = false;
		}

		lr.SetPosition (0, hand.transform.position);
		lr.SetPosition (1, anchor.transform.position);

	}
}