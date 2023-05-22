using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneMovementController : MonoBehaviour
{
    [SerializeField]
    private float pitchPower, rollPower, yawPower, enginePower;
    private float activeRoll, activePitch, activeYaw;
    public GameObject planefan;
    private bool throttle => Input.GetKey(KeyCode.Space); 
    private bool isPlaneAlive = true;
    public bool IsPlaneAlive{get{return isPlaneAlive;} set{
        isPlaneAlive = value;
        }}

    private Rigidbody planeRigidbody;
    // Start is called before the first frame update
    void Start()
    {
        planeRigidbody = transform.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(throttle && isPlaneAlive == true)
        {
            planefan.transform.Rotate(Vector3.forward*1000*Time.deltaTime);
            transform.position += transform.forward * enginePower * Time.deltaTime;
            //transform.Translate(transform.forward*enginePower,Space.Self);
            //planeRigidbody.AddRelativeForce(transform.forward * enginePower);
            activePitch = Input.GetAxisRaw("Vertical") * pitchPower * Time.deltaTime;
            activeRoll = Input.GetAxisRaw("Horizontal") * rollPower * Time.deltaTime;
            activeYaw = Input.GetAxisRaw("Yaw") * yawPower * Time.deltaTime;

            transform.Rotate(activePitch,-activeRoll,activeYaw, Space.Self);

        //limit the speed
        if (planeRigidbody.velocity.magnitude > enginePower)
        {
            planeRigidbody.velocity = planeRigidbody.velocity.normalized * enginePower;
        }
        }
        else{
            planefan.transform.Rotate(Vector3.forward*0*Time.deltaTime);
        }
    }
}
