using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    [SerializeField]
    private Transform targetTransform;
    [SerializeField]
    private Vector3 axisMultiplier = Vector3.one;

    private Vector3 offset;

    private void Start()
    {
        offset = transform.position;
    }

    private void LateUpdate()
    {
        Vector3 newPos;
        newPos.x = targetTransform.position.x * axisMultiplier.x;
        newPos.y = targetTransform.position.y * axisMultiplier.y;
        newPos.z = targetTransform.position.z * axisMultiplier.z;
        transform.position = newPos + offset;
    }
}