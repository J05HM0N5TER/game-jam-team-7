using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XboxCtrlrInput;


public class Player_controller : MonoBehaviour
{
    [SerializeField] XboxController Controller = XboxController.All;
    private bool CheckNumControlers;
    private Rigidbody rb;
    public float moveSpeed = 10.0f;
    public float maxSpeed = 10.0f;
    

    //jumping
    private bool isOnGround;
    public float jumpHeight = 5.0f;

    //punching 
    public GameObject fist;
    public float punchActiveSeconds = 2.0f;
    public float punchCoolDown = 2.0f;
    private bool punchCoolDownActive = false;

    float Xrotation = 0.0f;
    [SerializeField] XboxController controller = XboxController.All;

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
        //isOnGround = Physics.CheckSphere(groundChecking.position, groundDistance, );

        Vector3 moveInput = new Vector3(XCI.GetAxisRaw(XboxAxis.LeftStickX, Controller), 0.0f, XCI.GetAxisRaw(XboxAxis.LeftStickY, Controller));
        rb.AddForce(moveInput.normalized * moveSpeed);
        //gameObject.GetComponent<Transform>().Rotate(Vector3.up * moveInput.x  * 5);
        //gameObject.transform.rotation = Mathf.Clamp(Xrotation, 180, 0);
        //Xrotation = Mathf.Clamp(Xrotation, 180.0f, 0.0f);
        //transform.localRotation = Quaternion.Euler(0, Xrotation, 0);
        //gameObject.GetComponent<Transform>().Rotate(Vector3.up * moveInput.x * 5);

        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.AddForce(moveInput.normalized * -moveSpeed);
        }
        if(XCI.GetButtonDown(XboxButton.A) && isOnGround)
        {
            rb.AddForce( new Vector3(0,  jumpHeight, 0), ForceMode.Impulse);
        }
        if(XCI.GetButtonDown(XboxButton.B) && punchCoolDownActive == false)
        {
            fist.SetActive(true);
            StartCoroutine(PunchWait());
            StartCoroutine(punchingCoolDown());
        }
        
        //transform.rotation = Quaternion.LookRotation(moveInput);
        //if(XCI.GetAxisRaw(XboxAxis.RightStickX, controller) > 0.0f)
        //{
        //    transform.eulerAngles = new Vector3(0, 0, 0);
        //}
        //if (XCI.GetAxisRaw(XboxAxis.RightStickX, controller) < 0.0f)
        //{
        //    transform.eulerAngles = new Vector3(0, 180, 0);
        //}
        gameObject.transform.eulerAngles = new Vector3(0, (moveInput.x > 0 ? 180 : 0), 0);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("ground"))
        {
            isOnGround = true;
        }
        else
        {
            isOnGround = false;
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("ground"))
        {
            isOnGround = false;
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


}
