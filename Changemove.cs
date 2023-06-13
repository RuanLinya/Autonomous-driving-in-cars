using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;
public class Changemove : MonoBehaviour
{
    public float acceleration = 0.00001f; // 加速度
    public float deceleration = 0.00001f; // 减速度
    private Transform Target;
    private float currentVelocity = 1.0f; // 当前速度


    void Update()
    {
        Target = GetComponent<WaypointProgressTracker>().target;
        Vector3 heading = Target.position - transform.position;
        float distance = heading.magnitude;
        Vector3 direction = heading / distance;

        float time = Time.time;
        print(time);

        if (time <= 20f)
        {
            // 在0秒到20秒之间保持匀速
            currentVelocity = 1f; // 假设匀速为10m/s
        }
        else if (time > 20f && time <= 40f)
        {
            // 在20秒到40秒之间加速
            currentVelocity = Mathf.Lerp(currentVelocity, 20f, (time - 10f) * acceleration);
        }
        else if (time > 40f && time <= 60f)
        {
            // 在40秒到60秒之间减速
            currentVelocity = Mathf.Lerp(currentVelocity, 5f, (time - 40f) * deceleration);
        }
        else
        {
            // 大于60秒保持匀速
            currentVelocity = 1f; // 假设匀速为10m/s
        }

        // 应用速度到位置
        transform.position += direction * currentVelocity * Time.deltaTime;
    }
}











