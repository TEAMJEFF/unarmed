﻿using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{

    public Camera cam;
    public RaycastHit hit;
    private Rigidbody rb;

    public LayerMask cullingmask;
<<<<<<< HEAD
    public int maxDistance;

    public bool IsFlying;
    public bool IsSmaller;
    public Vector3 loc;
    public GameObject unitHit;

    public float speed = 10;
=======
    public const float MAXHOOKDISTANCE = 15.0f;

    public bool IsHooked;
    public bool IsHooking;
    public Vector3 target;
    public GameObject unitHit;
	public float hookLength = 0f;
	public const float hookDelta = 0.2f;

	public const float downMouseThreshold = -0.25f;
    public float speed = 25;
>>>>>>> master
    public Transform hand;

    public ThirdPersonCharacter FPC;
    public LineRenderer LR;

    // https://www.youtube.com/watch?time_continue=42&v=rhNmjKedcjw
    // Use this for initialization
    void Start()
    {
        //Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            Findspot();

        if (IsHooked)
            Hooking();

        if (IsHooking)
            Pulling();

        if (Input.GetButtonUp("Fire1"))
        {
			if (IsHooked) {
				IsHooked = false;
				hookLength = 0;
				//FPC.CanMove = true;
				LR.enabled = false;
				rb.useGravity = true;
				FPC.m_GravityMultiplier = 2f;

			} else if (IsHooking) {
				
				IsHooking = false;
				LR.enabled = false;
			}
        }
    }

    public void Findspot()
    {
        //Vector3 mousePos = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        //Debug.Log(cam.transform.forward);
        // Debug.Log(Input.mousePosition);
        //Vector3 guess = new Vector3(0.5f, 0.5f, 0);
        //Vector3 rayOrgin = cam.ViewportToWorldPoint(Input.mousePosition);
        //Debug.Log(rayOrgin);
        //Debug.DrawRay(rayOrgin, guess);
	
        Plane playerPlane = new Plane(Vector3.up, transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        //float hitdist = 0.0f;
        //if (playerPlane.Raycast(ray, out hitdist))

        if (Physics.Raycast(ray, out hit, MAXHOOKDISTANCE, cullingmask))
        {
            //Vector3 targetPoint = ray.GetPoint(hitdist);
            //Debug.Log("Actually hits?");
            target = hit.point;
            if (target.z > transform.position.z)
            {
				hookLength = Vector3.Distance (transform.position, target) + hookDelta;
				Debug.Log ("hookLength: " + hookLength.ToString ());

                LR.enabled = true;
                LR.SetPosition(1, target);
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Unanchored"))
                {
                    unitHit = hit.transform.gameObject;
                    IsHooking = true;
                }
                else
                {
                    IsHooked = true;            
                }
            }
        }
    }

    public void Hooking()
    {
        // rb.useGravity = false;
        FPC.m_GravityMultiplier = 0.1f;
        
		float targetDistance = Vector3.Distance (transform.position, target);

		Debug.Log ("Distance: " + targetDistance.ToString() + ", hookLength: " + hookLength.ToString() + "MAXHOOKDISTANCE: " + MAXHOOKDISTANCE.ToString());

		if (targetDistance < hookLength) {

			LR.SetPosition (0, hand.position);

			//if (Input.GetAxis ("Mouse X") < 0 || Input.GetAxis("Mouse X") > 0 || Input.GetAxis("Mouse Y") < 0 || Input.GetAxis("Mouse Y") > 0) {

			// Mouse Movement - downward only
			if (Input.GetAxis("Mouse Y") < downMouseThreshold) {
				Debug.Log ("Mouse Moved on Hook");
				// TODO: Should affect character momentum/direction, not linear interpolation
				//transform.position = Vector3.Lerp(transform.position, target, speed * Time.deltaTime / Vector3.Distance(transform.position, target));
				transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
			}

		} else {
			Debug.Log ("Hook length depleted.");
			IsHooked = false;
			LR.enabled = false;
			rb.useGravity = true;
			FPC.m_GravityMultiplier = 2f;
		}
    }
    public void Pulling()
    {
        //unitHit.transform.position = Vector3.Lerp(target, transform.position, speed * Time.deltaTime / Vector3.Distance(target, transform.position));
        Vector3 direction = unitHit.transform.position - transform.position;
        //unitHit.GetComponent().AddForce(10f * direction);
        
		float targetDistance = Vector3.Distance (transform.position, target);

		if (targetDistance < hookLength) {

			if (Input.GetAxis ("Mouse Y") < downMouseThreshold) {
				unitHit.transform.position = Vector3.MoveTowards (unitHit.transform.position, hand.position, (speed * Time.deltaTime));
			}

			LR.SetPosition (0, hand.position);
			LR.SetPosition (1, unitHit.transform.position);
        
		} else {
			
            IsHooking = false;
            LR.enabled = false;
        }
    }
}