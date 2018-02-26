using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{

    public Camera cam;
    public RaycastHit hit;
    private Rigidbody rb;

    public LayerMask cullingmask;
    public const float MAXHOOKDISTANCE = 15.0f;

    public bool IsHooked;
    public bool IsSmaller;
    public Vector3 target;
    public GameObject unitHit;
	public float hookLength = 0f;
	public const float hookDelta = 0.2f;

    public float speed = 25;
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

        if (IsSmaller)
            Pulling();

        if (Input.GetButtonUp("Fire1") && IsHooked)
        {
            IsHooked = false;
			hookLength = 0;
            //FPC.CanMove = true;
            LR.enabled = false;
            rb.useGravity = true;
            FPC.m_GravityMultiplier = 2f;
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
                if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Smaller"))
                {
                    unitHit = hit.transform.gameObject;
                    IsSmaller = true;
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
        
		// transform.position = Vector3.Lerp(transform.position, target, speed * Time.deltaTime / Vector3.Distance(transform.position, target));
        
		float targetDistance = Vector3.Distance (transform.position, target);

		Debug.Log ("Distance: " + targetDistance.ToString() + ", hookLength: " + hookLength.ToString() + "MAXHOOKDISTANCE: " + MAXHOOKDISTANCE.ToString());

		if (targetDistance < hookLength) {

			LR.SetPosition (0, hand.position);

			if (Vector3.Distance (transform.position, target) < 0.5f) {
				IsHooked = false;
				rb.useGravity = true;
				LR.enabled = false;
				FPC.m_GravityMultiplier = 2f;
			}
		} else {
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
        float step = speed * Time.deltaTime;
        unitHit.transform.position = Vector3.MoveTowards(unitHit.transform.position, hand.position, step);
        LR.SetPosition(0, hand.position);
        LR.SetPosition(1, unitHit.transform.position);
        if (Vector3.Distance(unitHit.transform.position, hand.position) < 1f)
        {
            IsSmaller = false;
            LR.enabled = false;
        }
    }
}