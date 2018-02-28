<<<<<<< HEAD
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
=======
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityStandardAssets._2D
{
    public class Restarter : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Player")
            {
                SceneManager.LoadScene(SceneManager.GetSceneAt(0).name);
            }
        }
    }
}
>>>>>>> parent of 946e6ef... Respawn/PressurePlate
