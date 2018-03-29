using System;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections;

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

		private const float NORMALTIME = 1.0f;
		private const float SLOWTIME = 0.4f;
		public const float TIMELIMIT = 5.0f;		// TIMELIMIT is the maximum time that time can be slowed, in seconds
		private const float REGENRATE = 0.05f;		// timeRegen is how much timePool is regenerated per tick
		private const float COOLDOWNRATE = 5.0f;

		private bool isSlowed = false;

		public Slider timeSlider;
		public Image fill;
		public Image cooldownImage;
		public float flashSpeed = 10;
		public Color flashColor = new Color(1f, 0.92f, 0.016f, 0.1f);	// yellow khaki
		static public bool inCooldown = false;
		static public float timePool = TIMELIMIT;			// timePool is how much time the player can currently slow down for
		static public float cooldownTimer;
		private bool isFlashing;

		private bool shiftDown;
		private bool didFlash;

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

			timeSlider.maxValue = TIMELIMIT;
			isFlashing = false;
			shiftDown = false;
			didFlash = false;
		}


		private void Update()
		{
			if (!m_Jump)
			{
				m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
			}

			if (inCooldown) {
				cooldownImage.color = Color.Lerp (cooldownImage.color, Color.clear, flashSpeed * Time.deltaTime);
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

			// When LSHIFT is pressed:
			if (Input.GetKey(KeyCode.LeftShift)) {

				// Check if timePool has time remaining in it and isn't in cooldown lock
				if (timePool > 0 && !inCooldown) {

					// if so, slow time and reduce the pool
					Time.timeScale = SLOWTIME;
					Time.fixedDeltaTime = 0.02f * Time.timeScale;
					timePool -= (Time.deltaTime * (1/SLOWTIME));
					timeSlider.value = timePool;
					isSlowed = true;

					// Bring the camera in
					ThirdPersonCamera.ZoomIn();

				} else if (timePool >= 0 && inCooldown) {

					// if timePool has time but player is in cooldown lock
					if (timePool < TIMELIMIT) {
						timePool += REGENRATE;
						if (timePool > TIMELIMIT) {
							timePool = TIMELIMIT;
						}
						timeSlider.value = timePool;
					}

					ThirdPersonCamera.ZoomOut ();

					// Flash UI
					StartCoroutine(flashTimeBar());

				} else if (timePool <= 0) {

					activateCooldown ();
				}
			} else {

				// If LSHIFT not down, regen timePool
				isSlowed = false;
				Time.timeScale = NORMALTIME;
				Time.fixedDeltaTime = 0.02f;
				if (timePool < TIMELIMIT) {
					timePool += REGENRATE;
					if (timePool > TIMELIMIT) {
						timePool = TIMELIMIT;
					}
					timeSlider.value = timePool;
				}

				ThirdPersonCamera.ZoomOut ();

			}

			// In cooldown lock but not pressing LSHIFT
			if (inCooldown) 
			{
				cooldownTimer -= Time.deltaTime;
				if (cooldownTimer <= 0) {
					inCooldown = false;
					fill.color = new Color32 (0x70, 0x71, 0xFF, 0xFF);
				}
			}


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

			// pass all parameters to the character control script

			bool crouch = false;
			m_Character.Move(m_Move, crouch, m_Jump);
			m_Jump = false;
		}

		public IEnumerator flashTimeBar() {

			if (!isFlashing) {
				isFlashing = true;
				for (int i = 0; i < 3; ++i) {
					Color prevColor = fill.color;
					fill.color = Color.red;
					yield return new WaitForSecondsRealtime (0.07f);
					fill.color = prevColor;
					yield return new WaitForSecondsRealtime (0.07f);
				}
				isFlashing = false;
				if (inCooldown) {
					fill.color = Color.grey;
				}
			}
		}

		public void activateCooldown() {

			ThirdPersonCamera.ZoomOut ();

			StartCoroutine(flashTimeBar ());

			// Disable slow-time
			Time.timeScale = NORMALTIME;
			Time.fixedDeltaTime = 0.02f;
			isSlowed = false;

			// Normalize to zero, start cooldown timer
			timePool = 0;
			timeSlider.value = timePool;
			inCooldown = true;
			cooldownTimer = COOLDOWNRATE;
			Color originalColor = fill.color;

			// Flash the timeSlider
			for (int i = 0; i < 4; ++i) {
				fill.color = originalColor;
				fill.color = Color.grey;
			}
		}
	}
}