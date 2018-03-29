using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// All mighty god reseting class 
// If this works I might pee a bit
// Update: I peed a bit

public class ResetClass {


    private GameObject gameObject;
    private Vector3 resetPosition;
    private Quaternion resetRotation;
	private Rigidbody body;


    public ResetClass(GameObject _gameObject, Vector3 _resetPosition, Quaternion _resetRotation)
    {
        gameObject = _gameObject;
        resetPosition = _resetPosition;
        resetRotation = _resetRotation;
		body = gameObject.GetComponent<Rigidbody> ();

    }

    // Reset position
    public void ResetThePositions()
    {
		gameObject.SetActive (true);
        gameObject.transform.position = resetPosition;
        gameObject.transform.rotation = resetRotation;
		if (body != null) 
		{
			body.velocity = new Vector3 (0f, 0f, 0f);
		}
    }
}
