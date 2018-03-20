using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    //https://gist.github.com/ftvs/5822103
    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    public Transform camTransform;

    // How long the object should shake for.
    public float shakeDuration = 2f;

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    Vector3 originalPos;
    private Quaternion originRotation;

    void Awake()
    {
        if (camTransform == null)
        {
            camTransform = transform;
        }
    }

    void OnEnable()
    {
        originalPos = camTransform.localPosition;
        shakeDuration = 2f;
    }

    void Update()
    {
        if (shakeDuration > 0)
        {
            camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
            camTransform.rotation = new Quaternion(
                    originRotation.x + Random.Range(-shakeAmount, shakeAmount) * .2f,
                    originRotation.y + Random.Range(-shakeAmount, shakeAmount) * .2f,
                    originRotation.z + Random.Range(-shakeAmount, shakeAmount) * .2f,
                    originRotation.w + Random.Range(-shakeAmount, shakeAmount) * .2f);
            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
        else
        {
            shakeDuration = 0f;
            camTransform.localPosition = originalPos;
            camTransform.rotation = new Quaternion(0f, 0f, 0f,0f);
        }
    }
}