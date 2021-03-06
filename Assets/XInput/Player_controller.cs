﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;


public class Player_controller : MonoBehaviour
{
	public XboxController controller = XboxController.All;
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

	[Header("Punch Sound Settings")]
	public bool isDemon;
	public AudioClip humanPunchSound;
	public float humanPunchVolume = 1.0f;
	public AudioClip demonPunchSound;
	public float demonPunchVolume = 1.0f;

	[Header("Hurt Sound Settings")]
	public AudioClip humanHurtSound;
	public float humanHurtVolume = 1.0f;
	public AudioClip demonHurtSound;
	public float demonHurtVolume = 1.0f;


	private Vector3 playerSpawnPoint;

	public ParticleSystem bloodSplatter;
	private Animator animator;

	// Start is called before the first frame update
	void Start()
	{
		// sounds 
		Audio = gameObject.GetComponent<AudioSource>();
		animator = gameObject.GetComponent<Animator>();
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


		playerSpawnPoint = gameObject.GetComponent<Transform>().position;
		bloodSplatter = bloodSplatter.GetComponentInChildren<ParticleSystem>();

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

			if ( // Player is not moving too fast
				!Physics.SphereCast(transform.position, castHeight, transform.forward, out _, castDistance, ground))
			{
				// Move the player
				rb.AddForce(moveInput.normalized * moveSpeed);
				//Audio.PlayOneShot(footStepSound, footStepVolume);
				if(canPlayFootStep && Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), groundCheckDistance))
				{
					canPlayFootStep = false;
					animator.SetBool("isRunning", true);
					StartCoroutine(playsound());
				}
				else
				{
					animator.SetBool("isRunning", false);
				}
			}
			if((rb.velocity.magnitude >= maxSpeed))
			{
				rb.AddForce(moveInput.normalized * -moveSpeed);
			}
		}
		
		// Jump
		if (XCI.GetButtonDown(XboxButton.A, controller) && // Pressed jump button
			Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), groundCheckDistance)) // On the ground
		{
			
			rb.AddForce(new Vector3(0, jumpHeight, 0), ForceMode.Impulse);
			animator.SetBool("Jumping", true);
			
		}
		if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), groundCheckDistance))
		{
			animator.SetBool("Jumping", false);
		}
		
		

		// Punch
		if (XCI.GetButtonDown(XboxButton.B, controller) && punchCoolDownActive == false)
		{
			animator.SetBool("punching", true);
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
		animator.SetBool("punching", false);
		fist.SetActive(false);
	}
	IEnumerator punchingCoolDown()
	{
		punchCoolDownActive = true;
		yield return new WaitForSeconds(punchCoolDown);
		punchCoolDownActive = false;

	}
	
	//void playsound()
	//{
	//	//Audio.PlayOneShot(footStepSound, footStepVolume);
		
	//}
	public void playHurtSounds()
	{
		if(!isDemon)
		{
			Audio.PlayOneShot(humanHurtSound, humanHurtVolume);
			StartCoroutine(playblood());
		}
		if(isDemon)
		{
			Audio.PlayOneShot(demonHurtSound, demonHurtVolume);
			StartCoroutine(playblood());
		}
	}
	IEnumerator playsound()
	{
		Audio.PlayOneShot(footStepSound);
		yield return new WaitForSeconds(footStepPlayDelay);
		canPlayFootStep = true;
	}
	public void playerRespawn()
	{
		gameObject.transform.position = playerSpawnPoint;
		rb.velocity = Vector3.zero;
	}
	IEnumerator playblood()
	{
		bloodSplatter.Play();
		yield return new WaitForSeconds(2);
		bloodSplatter.Stop();
	}
	


}
