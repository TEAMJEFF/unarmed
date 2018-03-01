using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class collissionRestart : MonoBehaviour {

    private Vector3 velocity;
    private bool madeContact = false;
    private float resetTime = 4f;
    private float timeAtContact;
    private float timeAtReset;

    void OnCollisionEnter(Collision collision)
    {
        madeContact = true;
        timeAtContact = Time.unscaledTime;
        timeAtReset = timeAtContact + resetTime;
        //Debug.Log(collision.gameObject);
        //Debug.Log(collision.relativeVelocity);
        //Debug.Log("HIT The object");
    }

    void Update()
    {
        if(madeContact)
        {
            if(timeAtReset < Time.unscaledTime)
            {
                madeContact = false;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

        }
        else
        {
            //timeAtContact = 0f;
            //timeAtReset = 0f;
        }
    }


}
