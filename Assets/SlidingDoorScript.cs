using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidingDoorScript : MonoBehaviour
{

    public GameObject LeftDoor;
    public GameObject RightDoor;
    public GameObject handle;
    private Vector3 endPosLeft;
    private Vector3 endPosRight;

    public bool leverPulled = false;

    // Use this for initialization
    void Start()
    {
        endPosLeft = LeftDoor.transform.position + new Vector3(-10, 0, 0);
        endPosRight = RightDoor.transform.position + new Vector3(10, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (leverPulled)
        {
            LeftDoor.transform.position = Vector3.MoveTowards(LeftDoor.transform.position, endPosLeft, 10 * Time.deltaTime);
            RightDoor.transform.position = Vector3.MoveTowards(RightDoor.transform.position, endPosRight, 10 * Time.deltaTime);
        }
    }

    void PulledDown()
    {
        leverPulled = true;
        handle.GetComponent<Rigidbody>().AddForce(0, 0, -20f);
        //GetComponent<CameraShake>().enabled = true;
        //cam.GetComponent<ThirdPersonCamera>().enabled = false;
        //cam.GetComponent<Rigidbody>().AddExplosionForce(500f, transform.position, 1f, 100f);
    }
}
