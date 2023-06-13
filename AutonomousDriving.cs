using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AutonomousDriving : MonoBehaviour
{
    public float desiredDistance = 50f;
    public float maxSpeed = 10f;
    public float maxAcceleration = 5f;
    public float maxBrakeForce = 10f;
    public LayerMask vehicleLayer;

    private Rigidbody rb;
    private GameObject targetVehicle;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        // 获取前方的车辆
        Collider[] vehicles = Physics.OverlapSphere(transform.position, desiredDistance, vehicleLayer);

        // 如果有车辆，则选择目标车辆
        if (vehicles.Length > 0)
        {
            float closestDistance = float.MaxValue;

            foreach (Collider vehicle in vehicles)
            {
                // 排除自身车辆
                if (vehicle.gameObject != gameObject)
                {
                    float distance = Vector3.Distance(transform.position, vehicle.transform.position);

                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        targetVehicle = vehicle.gameObject;
                    }
                }
            }
        }
        else
        {
            targetVehicle = null;
        }

        // 如果有目标车辆，则进行距离控制
        if (targetVehicle != null)
        {
            float distance = Vector3.Distance(transform.position, targetVehicle.transform.position);

            if (distance > desiredDistance)
            {
                // 加速以减小距离
                float acceleration = Mathf.Min(maxAcceleration, (distance - desiredDistance));
                rb.AddForce(transform.forward * acceleration);
            }
            else if (distance < desiredDistance)
            {
                // 制动以增加距离
                float brakeForce = Mathf.Min(maxBrakeForce, (desiredDistance - distance));
                rb.AddForce(transform.forward * -brakeForce);
            }
        }
        else
        {
            // 维持当前速度
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        }
    }
}