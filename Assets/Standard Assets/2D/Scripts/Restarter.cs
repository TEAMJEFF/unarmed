using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restarter : MonoBehaviour
{
	void Start()
	{
		gameObject.
	}

	void OnCollisionEnter(Collision collision)
	{
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);

	}
}