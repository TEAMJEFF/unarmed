using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine;

public class GrapplingHook : MonoBehaviour {

    public Camera cam;
    public RaycastHit hit;
    private Rigidbody rb;

    public LayerMask cullingmask;
    public int maxDistance;

    public bool IsFlying;
    public Vector3 loc;

    public float speed = 10;
    public Transform hand;

    public ThirdPersonCharacter FPC;
    public LineRenderer LR;

    // https://www.youtube.com/watch?time_continue=42&v=rhNmjKedcjw
    // Use this for initialization
    void Start () {
        //Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1"))
            Findspot();

        if (IsFlying)
            Flying();
        
        if (Input.GetButtonUp("Fire1") && IsFlying)
        {
            IsFlying = false;
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
        if (Physics.Raycast(ray, out hit, maxDistance, cullingmask))
        {
            //Vector3 targetPoint = ray.GetPoint(hitdist);
            //Debug.Log("Actually hits?");
            loc = hit.point;
            if (loc.z > transform.position.z)
            {
                LR.enabled = true;
                LR.SetPosition(1, loc);
                IsFlying = true;
            }
            
        }
    }

    public void Flying()
    {
        rb.useGravity = false;
        FPC.m_GravityMultiplier = 0f;
        transform.position = Vector3.Lerp(transform.position, loc, speed * Time.deltaTime / Vector3.Distance(transform.position, loc));
        LR.SetPosition(0, hand.position);

        if(Vector3.Distance(transform.position, loc) < 0.5f)
        {
            IsFlying = false;
            rb.useGravity = true;
            //FPC.CanMove = true;
            LR.enabled = false;
            FPC.m_GravityMultiplier = 2f;
        }
    }
}
