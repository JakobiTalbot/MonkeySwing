using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerEffect : ScriptableObject
{
    [SerializeField]
    private Material material;
    [SerializeField]
    private Mesh mesh;

    public abstract void ActivateEffect(PlayerController player);

    public Material GetMaterial() => material;
    public Mesh GetMesh() => mesh;
}