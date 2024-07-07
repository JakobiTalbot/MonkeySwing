using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMover : MonoBehaviour
{
    [SerializeField]
    Transform playerTransform;
    [SerializeField]
    private float moveAmount = 45f;

    private float playerStartX;

    private void Start()
    {
        playerStartX = playerTransform.position.x;
    }

    private void Update()
    {
        // when player moves over designated amount
        if (playerTransform.position.x - playerStartX > moveAmount)
        {
            // move the background to follow
            transform.position += Vector3.right * moveAmount;
            playerStartX += moveAmount;
        }
    }
}