using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerEffect : ScriptableObject
{
    public abstract void ActivateEffect(PlayerController player);
}