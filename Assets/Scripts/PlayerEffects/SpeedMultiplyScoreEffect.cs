using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player Effects/Speed Multiply and Score Gain")]
public class SpeedMultiplyScoreEffect : PlayerEffect
{
    [SerializeField]
    private float speedMultiplier = 1.1f;
    [SerializeField]
    private int scoreGain = 100;

    public override void ActivateEffect(PlayerController player)
    {
        player.MultiplyVelocityX(speedMultiplier);
        GameManager.instance.AddScore(scoreGain);
    }
}