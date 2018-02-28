using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class ThirdPersonUserControl : MonoBehaviour
    {
        private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
        private Transform m_Cam;                  // A reference to the main camera in the scenes transform
        private Vector3 m_CamForward;             // The current forward direction of the camera
        private Vector3 m_Move;
        private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.

		private bool isSlowed = false;
		private bool slowCooldown = false;
        
		private float slowTime;
		private float cooldownTime;

        private void Start()
        {
            // get the transform of the main camera
            if (Camera.main != null)
            {
                m_Cam = Camera.main.transform;
            }
            else
            {
                Debug.LogWarning(
                    "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
                // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
            }

            // get the third person character ( this should never be null due to require component )
            m_Character = GetComponent<ThirdPersonCharacter>();
        }


        private void Update()
        {
            if (!m_Jump)
            {
                m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
            }
        }
			

        // Fixed update is called in sync with physics
        private void FixedUpdate()
        {
            // read inputs
            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");

			// Forced forward movement
			v = 1f;


            bool crouch = Input.GetKey(KeyCode.C);


<<<<<<< HEAD
<<<<<<< HEAD
=======
			// If key hit and already not slowed down and not in cool down
			if (Input.GetKeyDown (KeyCode.LeftShift) && !isSlowed && !slowCooldown) 
			{
				Debug.Log ("KEY PRESSED");
				// Do something
				isSlowed = true;
				slowCooldown = true;
				slowTime = 5f;
				cooldownTime = 5f;
>>>>>>> master
=======
			// I think this shit works
			// Pretty sure it works correctly
			// Commented out to try and get a timer working
			/*
			if(Input.GetKey(KeyCode.E))
			{
				Time.timeScale = 0.5f;
			} else {
				Time.timeScale = 1f;
			}
			*/
>>>>>>> parent of 946e6ef... Respawn/PressurePlate

			// If key hit and already not slowed down and not in cool down
			if (Input.GetKeyDown (KeyCode.E) && !isSlowed && !slowCooldown) 
			{
				Debug.Log ("KEY PRESSED");
				// Do something
				isSlowed = true;
				slowCooldown = true;
				slowTime = 5f;
				cooldownTime = 5f;

<<<<<<< HEAD
<<<<<<< HEAD
				}
=======
			// If key released and cool down is true
			if (Input.GetKeyUp (KeyCode.LeftShift) && slowCooldown) 
			{
				Debug.Log ("KEY RELEASED");
				isSlowed = false;
			}
>>>>>>> master
=======
			}
>>>>>>> parent of 946e6ef... Respawn/PressurePlate

			// If key released and cool down is true
			if (Input.GetKeyUp (KeyCode.E) && slowCooldown) 
			{
				Debug.Log ("KEY RELEASED");
				isSlowed = false;
			}

			// Slow time for a period of time  
			if (isSlowed) 
			{
				Debug.Log ("SLOWED");
				// Timer to slow for a time and end by making isSlowed = true
				Time.timeScale = 0.5f;
				slowTime -= Time.deltaTime;
				//timer(slowTime);
				//Debug.Log (slowTime);
				if (slowTime < 0) 
				{
					isSlowed = false;
					//Time.timeScale = 1f;
					// Reset timer
					slowTime = 5f;
				}
			}

			// no longer slowed, start cooldown 
			if (!isSlowed && slowCooldown) 
			{
				Debug.Log ("COOLDOWN");
				if (Time.timeScale < 1f) 
				{
					Time.timeScale = 1f;
				}
				cooldownTime -= Time.deltaTime;
				Debug.Log (cooldownTime);
				//timer (cooldownTime);
				if (cooldownTime < 0) 
				{
					slowCooldown = false;
					cooldownTime = 5f;
				}
			}
<<<<<<< HEAD
<<<<<<< HEAD
		}
=======
=======
>>>>>>> parent of 946e6ef... Respawn/PressurePlate
					

            // calculate move direction to pass to character
            if (m_Cam != null)
            {
                // calculate camera relative direction to move:
                m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
                m_Move = v*m_CamForward + h*m_Cam.right;
            }
            else
            {
                // we use world-relative directions in the case of no main camera
                m_Move = v*Vector3.forward + h*Vector3.right;
            }
#if !MOBILE_INPUT
			// walk speed multiplier
<<<<<<< HEAD
	        // if (Input.GetKey(KeyCode.LeftShift)) m_Move *= 0.5f;
=======
	        if (Input.GetKey(KeyCode.LeftShift)) m_Move *= 0.5f;
>>>>>>> parent of 946e6ef... Respawn/PressurePlate
#endif

            // pass all parameters to the character control script
            m_Character.Move(m_Move, crouch, m_Jump);
            m_Jump = false;
        }
<<<<<<< HEAD
>>>>>>> master
=======
>>>>>>> parent of 946e6ef... Respawn/PressurePlate
    }
}
