using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// All mighty god reseting class 
// If this works I might pee a bit

public class ResetClass {


    private GameObject gameObject;
    private Vector3 resetPosition;
    private Quaternion resetRotation;


    public ResetClass(GameObject _gameObject, Vector3 _resetPosition, Quaternion _resetRotation)
    {
        gameObject = _gameObject;
        resetPosition = _resetPosition;
        resetRotation = _resetRotation;
    }

    // Reset position
    public void ResetThePositions()
    {
        gameObject.transform.position = resetPosition;
        gameObject.transform.rotation = resetRotation;
    }
}
