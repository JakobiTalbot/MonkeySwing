using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffectTrigger : MonoBehaviour
{
    [SerializeField]
    private PlayerEffect[] playerEffects;
    [SerializeField]
    private Vector2 randMoveHeight = new Vector2(-8f, 8f);
    [SerializeField]
    private Vector2 randMoveDistance = new Vector2(100f, 500f);

    private PlayerController player;
    private int currentEffectIndex;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        Vector3 newPos = transform.position;
        newPos.x += Random.Range(randMoveDistance.x, randMoveDistance.y);
        newPos.y += Random.Range(randMoveHeight.x, randMoveHeight.y);

        transform.position = newPos;

        GetComponent<BoxCollider>().enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            playerEffects[currentEffectIndex].ActivateEffect(player);
            Move(player);
            SetRandomPlayerEffect();
            // pickup feedback here
        }
        else if (other.CompareTag("PickupBounds"))
        {
            Move(player);
            SetRandomPlayerEffect();
        }
    }

    private void Move(PlayerController player)
    {
        // get new position
        Vector3 newPos = player.transform.position;
        newPos.x += Random.Range(randMoveDistance.x, randMoveDistance.y);
        newPos.y += Random.Range(randMoveHeight.x, randMoveHeight.y);

        transform.position = newPos;
    }

    private void SetRandomPlayerEffect()
    {
        int i = Random.Range(0, playerEffects.Length);
        // update visuals if the new player effect is different from current
        if (i != currentEffectIndex)
            playerEffects[currentEffectIndex].SetVisuals(gameObject);
    }
}