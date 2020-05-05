using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;


public class Player_controller : MonoBehaviour
{
	[SerializeField] XboxController Controller = XboxController.All;
	private bool CheckNumControlers;
	private Rigidbody rb;

	[Header("Movement settings")]
	public float moveSpeed = 10.0f;
	public float maxSpeed = 10.0f;


	[Header("Jumping settings")]
	public float jumpHeight = 5.0f;
	private float raydistance = 1.000001f;

	[Header("Punch settings")]
	public GameObject fist;
	public float punchActiveSeconds = 2.0f;
	public float punchCoolDown = 2.0f;
	private bool punchCoolDownActive = false;

	// Capsule cast stuff
	private float castHeight = .5f;
	private float castDistance = .6f;


	// Start is called before the first frame update
	void Start()
	{
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
		Vector3 moveInput = new Vector3(XCI.GetAxisRaw(XboxAxis.LeftStickX, Controller), 0.0f, XCI.GetAxisRaw(XboxAxis.LeftStickY, Controller));

		// If they have move input
		if (moveInput != Vector3.zero)
		{
			// Face in correct direction
			transform.rotation = Quaternion.LookRotation(new Vector3(moveInput.x, 0, 0));

			if (!(rb.velocity.magnitude > maxSpeed) && // Player is not moving too fast
				!Physics.SphereCast(transform.position, castHeight, transform.forward, out _))
			{
				// Move the player
				rb.AddForce(moveInput.normalized * moveSpeed);
			}
		}

		// Jump
		if (XCI.GetButtonDown(XboxButton.A))
		{
			RaycastHit hit;
			// Check if they are on the ground
			if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, raydistance))
			{
				GameObject hitobject;
				hitobject = (hit.collider.gameObject);
				if (hitobject.CompareTag("ground"))
				{
					rb.AddForce(new Vector3(0, jumpHeight, 0), ForceMode.Impulse);
				}
			}

		}

		// Punch
		if (XCI.GetButtonDown(XboxButton.B) && punchCoolDownActive == false)
		{
			fist.SetActive(true);
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


}
