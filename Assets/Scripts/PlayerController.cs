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
    private Vector3 startVelocity = new Vector3(10f, 0f, 0f);
    [SerializeField]
    float gravity = -9.8f;
    [SerializeField]
    private int ceilingBoundsLayer = 6;

    private LineRenderer lineRenderer;

    private bool isSwinging = false;
    private Vector3 tetherPoint;
    private Vector3 velocity;
    private bool doUpdate = true;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        velocity = startVelocity;
    }

    private void Update()
    {
        if (!doUpdate)
            return;

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

        transform.position += velocity * Time.deltaTime;
        // rotate forwards to direction player is moving
        transform.LookAt(transform.position + velocity.normalized);
    }

    private void LateUpdate()
    {
        // set rope points if swinging
        if (isSwinging)
        {
            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, tetherPoint);
        }

    }

    private void StartSwing()
    {
        // raycast for grapple point
        RaycastHit hit;
        // get raycast direction upwards of player velocity direction
        Vector3 dir = Vector3.Cross(velocity.normalized, Vector3.back);
        // don't let player grapple behind them
        dir.x = Mathf.Max(dir.x, 0f);
        Physics.Raycast(transform.position, dir, out hit, 100f, 1 << ceilingBoundsLayer);
        tetherPoint = hit.point;

        // show rope
        lineRenderer.enabled = true;
        isSwinging = true;
    }

    private void StopSwing()
    {
        isSwinging = false;
        // hide rope
        lineRenderer.enabled = false;

        // TODO: implement perfect swing
        //float perfectSwingFactor = Vector3.Dot(transform.forward, perfectSwingDirection);
        //speed *= Mathf.Max(perfectSwingFactor, 1f) * perfectSwingSpeedGain;
    }

    private void Swing()
    {
        // set velocity in swing direction
        Vector3 tetherPointDir = (tetherPoint - transform.position);
        velocity = Vector3.Cross(Vector3.back, tetherPointDir).normalized * velocity.magnitude;
    }
    
    public void AddVelocity(Vector3 v) => velocity += v;
    public void MultiplyVelocityX(float factor) => velocity.x *= factor;

    private void OnTriggerEnter(Collider other)
    {
        // WIP fail state, quick fix
        if (other.GetComponentInParent<BackgroundMover>())
        {
            GameManager.instance.GameOver((int)transform.position.x);
            doUpdate = false;
        }
    }
}