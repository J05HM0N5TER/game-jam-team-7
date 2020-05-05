using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;


public class Player_controller : MonoBehaviour
{
	[SerializeField] XboxController controller = XboxController.All;
	private bool CheckNumControlers;
	private Rigidbody rb;

	[Header("Movement settings")]
	public float moveSpeed = 10.0f;
	public float maxSpeed = 10.0f;


	[Header("Jumping settings")]
	public float jumpHeight = 5.0f;
	public float groundCheckDistance = 1.000001f;

	[Header("Punch settings")]
	public GameObject fist;
	public float punchActiveSeconds = 2.0f;
	public float punchCoolDown = 2.0f;
	private bool punchCoolDownActive = false;

	// Capsule cast stuff
	private float castHeight = .5f;
	private float castDistance = .6f;

	[Header("footstep Settings")]
	AudioSource Audio;
	public AudioClip footStepSound;
	public float footStepVolume = 1.0f;
	public float footStepPlayDelay = 0.1f;
	private bool canPlayFootStep = true;

	[Header("Punch Settings")]
	public bool isDemon;
	public AudioClip humanPunchSound;
	public float humanPunchVolume = 1.0f;
	public AudioClip demonPunchSound;
	public float demonPunchVolume = 1.0f;



	// Start is called before the first frame update
	void Start()
	{
		// sounds 
		Audio = gameObject.GetComponent<AudioSource>();

		if (!CheckNumControlers)
		{
			CheckNumControlers = true;

			int queriedNumberOfCtrlrs = XCI.GetNumPluggedCtrlrs();
			if (queriedNumberOfCtrlrs == 1)
			{
				Debug.Log("Only " + queriedNumberOfCtrlrs + " Xbox controller plugged in.");
			}
			else if (queriedNumberOfCtrlrs == 0)
			{
				Debug.Log("No Xbox controllers plugged in!");
			}
			else
			{
				Debug.Log(queriedNumberOfCtrlrs + " Xbox controllers plugged in.");
			}

			XCI.DEBUG_LogControllerNames();
		}
		rb = gameObject.GetComponent<Rigidbody>();
		fist.GetComponent<Collider>();
		fist.SetActive(false);

		

	}

	// Update is called once per frame
	void Update()
	{
		LayerMask ground = 1 << 7;

		Vector3 moveInput = new Vector3(XCI.GetAxisRaw(XboxAxis.LeftStickX, controller), 0.0f, XCI.GetAxisRaw(XboxAxis.LeftStickY, controller));

		// If they have move input
		if (moveInput != Vector3.zero)
		{
			// Face in correct direction
			transform.rotation = Quaternion.LookRotation(new Vector3(moveInput.x, 0, 0));

			if (!(rb.velocity.magnitude > maxSpeed) && // Player is not moving too fast
				!Physics.SphereCast(transform.position, castHeight, transform.forward, out _, castDistance, ground))
			{
				// Move the player
				rb.AddForce(moveInput.normalized * moveSpeed);
				//Audio.PlayOneShot(footStepSound, footStepVolume);
				if(canPlayFootStep && Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), groundCheckDistance))
				{
					canPlayFootStep = false;
					StartCoroutine(playsound());
				}
			}
		}
		
		// Jump
		if (XCI.GetButtonDown(XboxButton.A, controller) && // Pressed jump button
			Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), groundCheckDistance)) // On the ground
		{
			rb.AddForce(new Vector3(0, jumpHeight, 0), ForceMode.Impulse);
		}
		

		// Punch
		if (XCI.GetButtonDown(XboxButton.B, controller) && punchCoolDownActive == false)
		{
			fist.SetActive(true);
			if(!isDemon)
			{
				Audio.PlayOneShot(humanPunchSound, humanPunchVolume);
			}
			if(isDemon)
			{
				Audio.PlayOneShot(demonPunchSound, demonPunchVolume);
			}
			StartCoroutine(PunchWait());
			StartCoroutine(punchingCoolDown());
		}
	}

	IEnumerator PunchWait()
	{
		yield return new WaitForSeconds(punchActiveSeconds);
		fist.SetActive(false);
	}
	IEnumerator punchingCoolDown()
	{
		punchCoolDownActive = true;
		yield return new WaitForSeconds(punchCoolDown);
		punchCoolDownActive = false;

	}
	private void OnDrawGizmos()
	{
		Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z), 0.5f);
	}
	//void playsound()
	//{
	//	//Audio.PlayOneShot(footStepSound, footStepVolume);
		
	//}
	IEnumerator playsound()
	{
		Audio.PlayOneShot(footStepSound);
		yield return new WaitForSeconds(footStepPlayDelay);
		canPlayFootStep = true;
	}


}
