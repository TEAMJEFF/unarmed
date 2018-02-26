using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchMechanic : MonoBehaviour {

	public GameObject activated;

	// Use this for initialization
	void Start () 
	{
		activated = this.gameObject.transform.GetChild (0).gameObject;
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void OnCollisionEnter(Collision collision)
	{
		//activated = this.gameObject.transform.GetChild (0);
		Destroy(activated);
	}
}
