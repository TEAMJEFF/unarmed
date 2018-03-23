using UnityEngine;
using System.Collections;

public class BodyAiming : MonoBehaviour
{
		public Transform target;

		void Update () {
         float mouseInput = Input.GetAxis("Mouse X");
         Vector3 lookhere = new Vector3(0, mouseInput,  0);
         transform.Rotate(lookhere);
     }
}
