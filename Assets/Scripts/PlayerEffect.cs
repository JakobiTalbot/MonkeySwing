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

    public void SetVisuals(GameObject target)
    {
        target.GetComponent<MeshRenderer>().material = material;
        target.GetComponent<MeshFilter>().mesh = mesh;
    }
}