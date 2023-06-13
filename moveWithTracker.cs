using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Utility;
public class moveWithTracker : MonoBehaviour
{
    public float velocity;
    private Transform Target;
    void Update()
    {
        Target = GetComponent<WaypointProgressTracker>().target;
        Vector3 heading = Target.position - transform.position;
        float distance = heading.magnitude;
        Vector3 direction = heading / distance;

        transform.position += direction * velocity * Time.deltaTime;

    }
}
