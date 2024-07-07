using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    Transform targetTransform;
    [SerializeField]
    private Vector3 offset;

    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    private void LateUpdate()
    {
        transform.position = new Vector3(targetTransform.position.x, startPos.y, startPos.z) + offset;
    }
}