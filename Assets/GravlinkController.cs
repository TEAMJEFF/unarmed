using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravlinkController : MonoBehaviour {

	public const float MAXHOOKDISTANCE = 20.0f;

	public GameObject anchor;
	public LayerMask cullingmask;
	public Transform hand;

	private bool isHooked;
	private bool isHooking;

	private Rigidbody rb;
	private HingeJoint joint;
	private HingeJoint anchorJoint;
	private LineRenderer lr;

	private float hookLength;

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

			FindHitLoc ();
		} else if (Input.GetMouseButtonUp(0)) {
			if (isHooked) {
				isHooked = false;
				lr.enabled = false;
				Destroy (joint);
				Destroy (anchorJoint);

				hookLength = 0;
				rb.useGravity = true;
				
			} else if (isHooking) {
				isHooking = false;
				lr.enabled = false;
				Destroy (joint);
				Destroy (anchorJoint);
			}
		}

		if (isHooked) {
			Swinging ();
		} else if (isHooking) {
			Pulling ();
		}
	}

	public void FindHitLoc() {

		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		if (Physics.Raycast(ray, out hit, MAXHOOKDISTANCE, cullingmask)) {

			if (hit.point.z > transform.position.z) {
				Debug.Log ("Hit");

				hookLength = Vector3.Distance (rb.position, hit.point);

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
				joint.connectedBody = anchor.GetComponent<Rigidbody> ();

				// Show line
				lr.SetPosition(0, hand.position);
				lr.SetPosition (1, anchor.transform.position);
				lr.enabled = true;

				// Check isHooked, isHooking
				if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Unanchored")) {
					isHooking = true;
				} else {
					isHooked = true;
				}
			}
		}
	}

	public void Swinging() {

	}

	public void Pulling() {

	}
}