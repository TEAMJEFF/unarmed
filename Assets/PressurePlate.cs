using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class switchMechanic : MonoBehaviour {

	public GameObject activated;
	private ParticleSystem psys;

	// Use this for initialization
	void Start () 
	{
		activated = this.gameObject.transform.GetChild (0).gameObject;
		//psys = GetComponent<ParticleSystem> ();

	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void OnCollisionEnter(Collision collision)
	{
		//activated = this.gameObject.transform.GetChild (0);
		//psys.Play();
		Destroy(activated);
	}


}
