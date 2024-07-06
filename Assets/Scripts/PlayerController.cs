using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Vector3 raycastDirection = new Vector3(1f, 1f, 0f);
    [SerializeField]
    private Vector3 perfectSwingDirection = new Vector3(1f, 1f, 0f);
    [SerializeField]
    private float perfectSwingSpeedGain = 5f;
    [SerializeField]
    private Vector3 startVelocity = new Vector3(10f, 0f, 0f);
    [SerializeField]
    float gravity = -9.8f;
    [SerializeField]
    float swingAcceleration = 1f;

    private LineRenderer lineRenderer;

    private bool isSwinging = false;
    private Vector3 tetherPoint;
    private float ropeLength;
    private Vector3 velocity;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        velocity = startVelocity;
    }

    private void Update()
    {
        // start/stop swinging
        if (Input.GetKeyDown(KeyCode.Space))
            StartSwing();
        else if (isSwinging && Input.GetKeyUp(KeyCode.Space))
            StopSwing();

        // swing calculations
        if (isSwinging)
            Swing();
        else // only do gravity if not swinging
            velocity.y += gravity * Time.deltaTime;

        // move
        transform.position += velocity * Time.deltaTime;

        Debug.Log(velocity.x);
    }

    private void StartSwing()
    {
        // find grapple point
        RaycastHit hit;
        Physics.Raycast(transform.position, raycastDirection, out hit, 100f, 1 << 6); // ceiling has layer 6
        tetherPoint = hit.point;
        ropeLength = hit.distance; // set max rope length to length between player and grapple point
        lineRenderer.enabled = true;
        
        // TODO: convert velocity direction

        isSwinging = true;
    }

    private void StopSwing()
    {
        isSwinging = false;
        lineRenderer.enabled = false;

        // find perfect swing
        Vector3 dir = (tetherPoint - transform.position).normalized;
        float perfectSwingFactor = Vector3.Dot(dir, perfectSwingDirection);
        velocity.x += perfectSwingSpeedGain * perfectSwingFactor;
    }

    private void Swing()
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, tetherPoint);

        // TODO: swing slower past certain point

        Vector3 dir = (tetherPoint - transform.position);
        velocity = Vector3.Cross(-transform.forward, dir) * (velocity.magnitude / ropeLength);
    }
}