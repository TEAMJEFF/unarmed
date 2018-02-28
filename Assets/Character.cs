using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Character : MonoBehaviour {

    public Transform cam;
    private RaycastHit hit;
    private Rigidbody rb;
    public bool attached = false;

    private float momentum;
    public float speed;
    private float step;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();

	}
	
	// Update is called once per frame
	void Update () {
		// Pause game on start
		while (Time.timeSinceLevelLoad < 100) 
		{
			Time.timeScale = 0f;
		}

		Time.timeScale = 1f;

		if (Input.GetButtonDown("Fire1"))
        {
            Vector3 mousePos = new Vector3(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0);
            if (Physics.Raycast (rb.position, mousePos, out hit))
            {
                attached = true;
                rb.isKinematic = true;
            } 
        }

        if (Input.GetButtonUp ("Fire1"))
        {
            attached = false;
            rb.isKinematic = false;
            rb.velocity = transform.forward * momentum;
        }

        if (attached)
        {
            momentum += Time.deltaTime * speed;
            step = momentum * Time.deltaTime;
            rb.position = Vector3.MoveTowards(transform.position, hit.point, step);
        }

        if (!attached && momentum >= 0)
        {
            momentum -= Time.deltaTime * 5;
            step = 0;
        }

        //if (cc.Grounded && momentum <= 0)
        //{
        //   momentum = 0;
        //   step = 0;
        //}
	}
}
