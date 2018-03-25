using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class RepelBlast : MonoBehaviour {

	public Camera cam;
	public RaycastHit hit;
	private Rigidbody rb;

	public LayerMask cullingmask;
	public const float MAXFIREDISTANCE = 20.0f;

	public bool IsRepelled;
	public bool IsRepelling;
	public Vector3 target;
	public Rigidbody unitHit;

	public Transform hand;
	public ThirdPersonCharacter FPC;
	public LineRenderer LR;

	public Transform particleToAlign;
	public ParticleSystem particleOne;
	public ParticleSystem particleTwo;

	public float force = 75f;
	public float radius = 100f;
	public float upModifier = 0.25f;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update () {
		if (cam.enabled) {
			if (Input.GetButtonDown ("Fire1"))
				Fire ();
		}
	}

	public void Fire()
	{

		Ray ray = cam.ScreenPointToRay(Input.mousePosition);

		// Use up time when repelling, if not in cooldown
		if (!ThirdPersonUserControl.inCooldown) {

			float blastDelta = ThirdPersonUserControl.timePool - ThirdPersonUserControl.TIMELIMIT / 2.5f;

			// Got enough time to blast
			if (blastDelta > 0) {
				Debug.Log ("Got time for repel");
				ThirdPersonUserControl.timePool = blastDelta;

			// Got just enough time to blast, but will enter cooldown
			} else if (blastDelta >= -1f) {
				Debug.Log ("Repel, then cooldown");
				GameObject thirdPersonUC = GameObject.Find ("ThirdPersonController");
				thirdPersonUC.GetComponent<ThirdPersonUserControl> ().activateCooldown (); // takes care of timePool

			// Don't got enough time to blast
			} else {
				Debug.Log ("Not enough time for blast");
				GameObject thirdPersonUC = GameObject.Find ("ThirdPersonController");
				StartCoroutine(thirdPersonUC.GetComponent<ThirdPersonUserControl> ().flashTimeBar ());
				return;
			}

		// If in cooldown, flash the timebar
		} else {
			Debug.Log ("Flash it biyatch");
			GameObject thirdPersonUC = GameObject.Find ("ThirdPersonController");
			StartCoroutine(thirdPersonUC.GetComponent<ThirdPersonUserControl> ().flashTimeBar ());
			return;
		}

		Debug.Log ("Fire!");



		if (Physics.Raycast(ray, out hit, MAXFIREDISTANCE, cullingmask))
		{
			Debug.Log ("Hit");

			particleToAlign.LookAt(hit.point);
			particleOne.Emit(1);
			particleTwo.Emit(1);



			target = hit.point;
			if (target.z > transform.position.z - 1)
			{
				// What did we hit?
				if (hit.transform.gameObject.layer == LayerMask.NameToLayer ("Unanchored")) {

					Debug.Log ("Unanchored");

					// An unanchored object: blow it back!
					GameObject target = hit.transform.gameObject;
					unitHit = target.GetComponent<Rigidbody> ();
					unitHit.AddExplosionForce (800f, transform.position, 80f, 0.25f);
				} else if (hit.transform.gameObject.layer == LayerMask.NameToLayer ("Anchored")) {
					{
						Debug.Log ("Anchored");

						Vector3 direction = -transform.forward * force;
						GameObject target = hit.transform.gameObject;
						Debug.Log ("Name of unanchored " + target.name);
						if (target.name.Contains ("Lever")) {
							target.SendMessage ("PulledDown");
						} else {
							rb.AddExplosionForce (1000f, hit.point, 80f, 0.3f);
							// rb.AddForce (direction, ForceMode.Impulse);
						}


					}
				}
			}
		}
	}
}
