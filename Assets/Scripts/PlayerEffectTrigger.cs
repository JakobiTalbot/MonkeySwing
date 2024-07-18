using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffectTrigger : MonoBehaviour
{
    [SerializeField]
    private PlayerEffect playerEffect;

    private void OnTriggerEnter(Collider other)
    {
        PlayerController player = other.GetComponent<PlayerController>();
        if (player)
        {
            playerEffect.ActivateEffect(player);
            Destroy(gameObject);
            // pickup feedback here
        }
    }
}