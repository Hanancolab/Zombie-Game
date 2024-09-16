using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckController : MonoBehaviour
{
    [Header("Wheels colliders")]
    public WheelCollider frontRightWheelCollider;
    public WheelCollider frontLeftWheelCollider;
    public WheelCollider backRightWheelCollider;
    public WheelCollider backLeftwheelCollider;

    [Header("Wheels Transforms")]
    public Transform frontRightWheelTransform;
    public Transform frontLeftwheelTransform;
    public Transform backRightWheelTransform;
    public Transform backLeftwheelTransform;
    public Transform vehicleDoor;

    [Header("Vehicle Engine")]
    public float accelerationForce = 100f;
    public float presentAcceleration = 0f;
    public float brakingForce = 200f;
    public float presentBrakeForce = 0f;

    [Header("Vehicle Steering")]
    public float wheelTorque = 20f;
    public float presentTurnAngle = 0f;

    [Header("Vehicle Brakes")]
    float breakingForce = 200f;
    float presentBreakForce = 0f;

    [Header("Player Enter")]
    public Player player;
    private float radius = 5f;
    private bool isOpened = false;

    [Header("Disable Things")]
    public GameObject aimCam;
    public GameObject aimCanvas;
    public GameObject thirdAimCamera;
    public GameObject thirdAimCanvas;
    public GameObject playerCharacter;

    [Header("truck hit Var")]
    public float hitRange = 2f;
    private float giveDamageOf = 100f;
    public GameObject goreEffect;
    public Camera cam;

    public GameObject obj_4;
    private void Update()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < radius)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                isOpened = true;
                radius = 5000f;
                Objective.occurence.GetobjectivesDone(true, true, true, false);
                obj_4.SetActive(true);
            }
            else if (Input.GetKeyDown(KeyCode.F))
            {
                player.transform.position = vehicleDoor.transform.position;
                isOpened = false;
                radius = 5f;
            }
        }
        if(isOpened == true)
        {
            thirdAimCamera.SetActive(false);
            thirdAimCanvas.SetActive(false);
            aimCam.SetActive(false);
            aimCanvas.SetActive(false);
            playerCharacter.SetActive(false);
            MoveVehicle();
            VehicleSteering();
            ApplyBreaks();
            HitZombies();
        }
        else if (isOpened == false)
        {
            thirdAimCamera.SetActive(true);
            thirdAimCanvas.SetActive(true);
            aimCam.SetActive(true);
            aimCanvas.SetActive(true);
            playerCharacter.SetActive(true);
        }
        
    }

    void MoveVehicle()
    {
        frontRightWheelCollider.motorTorque = presentAcceleration;
        frontLeftWheelCollider.motorTorque = presentAcceleration;
        backRightWheelCollider.motorTorque = presentAcceleration;
        backLeftwheelCollider.motorTorque = presentAcceleration;
        presentAcceleration = accelerationForce * -Input.GetAxis("Vertical");

    }

    void VehicleSteering()
    {
        presentTurnAngle = wheelTorque * Input.GetAxis("Horizontal");

        frontRightWheelCollider.steerAngle = presentTurnAngle;
        frontLeftWheelCollider.steerAngle = presentTurnAngle;
        steeringWheels(frontRightWheelCollider, frontRightWheelTransform);
        steeringWheels(frontLeftWheelCollider, frontLeftwheelTransform);
        steeringWheels(backRightWheelCollider, backRightWheelTransform);
        steeringWheels(backLeftwheelCollider, backLeftwheelTransform);
    }

    void steeringWheels(WheelCollider WC, Transform WT)
    {
        Vector3 Wposition;
        Quaternion Wprotation;

        WC.GetWorldPose(out Wposition, out Wprotation);

        WT.position = Wposition;
        WT.rotation = Wprotation;
    }
    void ApplyBreaks()
    {
        if (Input.GetKey(KeyCode.Space))
            presentBreakForce = breakingForce;
        else
            presentBreakForce = 0f;

        frontRightWheelCollider.brakeTorque = presentBreakForce;
        frontLeftWheelCollider.brakeTorque = presentBreakForce;
        backRightWheelCollider.brakeTorque = presentBreakForce;
        backLeftwheelCollider.brakeTorque = presentBreakForce;
    }

    void HitZombies()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hitInfo, hitRange))
        {
            Debug.Log(hitInfo.transform.name);

            Zombie1 zombie1 = hitInfo.transform.GetComponent<Zombie1>();
            Zombie2 zombie2 = hitInfo.transform.GetComponent<Zombie2>();

            if (zombie1 != null)
            {
                zombie1.zombieHitDamage(giveDamageOf);
                zombie1.GetComponent<CapsuleCollider>().enabled = false;
                GameObject goreEffectGo = Instantiate(goreEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(goreEffectGo, 1f);
            }
            else if (zombie2 != null)
            {
                zombie2.zombieHitDamage(giveDamageOf);
                zombie2.GetComponent<CapsuleCollider>().enabled = false;
                GameObject goreEffectGo = Instantiate(goreEffect, hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                Destroy(goreEffectGo, 1f);
            }
        }
    }
}