using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private Vector3 perfectSwingDirection = new Vector3(0.5f, 0.5f, 0f);
    [SerializeField]
    private float perfectSwingSpeedGain = 5f;
    [SerializeField]
    private Vector3 startDirection = new Vector3(1f, 0f, 0f);
    [SerializeField]
    private float startSpeed = 1f;
    [SerializeField]
    float gravity = -9.8f;
    [SerializeField]
    float swingAcceleration = 1f;

    private LineRenderer lineRenderer;

    private bool isSwinging = false;
    private Vector3 tetherPoint;
    private float ropeLength;
    private Vector3 direction;
    float speed;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        direction = startDirection;
        speed = startSpeed;
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
        else // only do gravity if not swinging TODO: do gravity if not at max rope length ?
            direction.y += gravity * Time.deltaTime;

        // move
        transform.position += direction * speed * Time.deltaTime;
        // face player towards direction they're moving
        transform.LookAt(transform.position + direction);

    }

    private void StartSwing()
    {
        // find grapple point
        RaycastHit hit;
        // get raycast direction upwards of player velocity direction
        Vector3 dir = Vector3.Cross(direction, Vector3.back);
        dir.x = Mathf.Clamp(dir.x, 0f, 1f);
        Physics.Raycast(transform.position, dir, out hit, 100f, 1 << 6); // ceiling bounds have layer 6
        tetherPoint = hit.point;
        ropeLength = hit.distance; // set max rope length to length between player and grapple point

        lineRenderer.enabled = true; // show rope
        isSwinging = true;
    }

    private void StopSwing()
    {
        isSwinging = false;
        lineRenderer.enabled = false;

        // TODO: implement perfect swing
        float perfectSwingFactor = Vector3.Dot(transform.forward, perfectSwingDirection);
        //speed *= Mathf.Max(perfectSwingFactor, 1f) * perfectSwingSpeedGain;

        Debug.Log(perfectSwingFactor);
    }

    private void Swing()
    {
        // set rope points
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, tetherPoint);

        // TODO: accelerate slower / deccelerate past certain point ?
        // accelerate speed while swinging
        //speed += swingAcceleration * Time.deltaTime;
        //Debug.Log(speed);

        // set swing direction
        Vector3 tetherPointDir = (tetherPoint - transform.position);
        direction = Vector3.Cross(Vector3.back, tetherPointDir).normalized;
    }
}