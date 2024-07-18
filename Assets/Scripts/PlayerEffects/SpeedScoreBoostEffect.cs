using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player Effects/Speed and Score Gain")]
public class SpeedScoreBoostEffect : PlayerEffect
{
    [SerializeField]
    private float speedBoostFactor = 1.1f;
    [SerializeField]
    private int scoreGain = 100;

    public override void ActivateEffect(PlayerController player)
    {
        player.MultiplyVelocityX(speedBoostFactor);
        GameManager.instance.AddScore(scoreGain);
    }
}