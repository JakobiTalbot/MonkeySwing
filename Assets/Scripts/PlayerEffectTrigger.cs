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
    private int currentEffectIndex = -1;
    private MeshRenderer meshRenderer;
    private MeshFilter meshFilter;

    private void Start()
    {
        // get references
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        meshRenderer = GetComponent<MeshRenderer>();
        meshFilter = GetComponent<MeshFilter>();

        // initialise object
        Reset();

        // enable box collider after moving
        GetComponent<BoxCollider>().enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
            playerEffects[currentEffectIndex].ActivateEffect(player);
            Reset();
            // pickup feedback here
        }
        else if (other.CompareTag("PickupBounds"))
            Reset();
    }

    private void Reset()
    {
        // get new position
        Vector3 newPos = Vector3.zero;
        newPos.x = player.transform.position.x + Random.Range(randMoveDistance.x, randMoveDistance.y);
        newPos.y = Random.Range(randMoveHeight.x, randMoveHeight.y);

        transform.position = newPos;

        int i = Random.Range(0, playerEffects.Length);
        // update visuals if the new player effect is different from current
        if (i != currentEffectIndex)
            UpdateVisuals(i);
    }

    private void UpdateVisuals(int i)
    {
        meshRenderer.material = playerEffects[i].GetMaterial();
        meshFilter.mesh = playerEffects[i].GetMesh();
        // TODO: update scale + rotation + collider size? probably better way

        currentEffectIndex = i;
    }
}