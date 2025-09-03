using NUnit.Framework;
using UnityEngine;

/// <summary>
/// Asocia un ScriptableObject <see cref="PickableObject"/> a un prefab físico en la escena.
/// </summary>
public class Pickable : MonoBehaviour
{
    [Tooltip("Referencia al ScriptableObject con los datos del objeto.")]
    public PickableObject data;

    [Header("Player reference")]
    public Transform player;

    [Header("Outline Material")]
    private Material[] materials;
    private Material outlineMaterial;
    [Header("Outline Settings")]
    public float activationDistance = 5f;
    public float outlineWidth = 0.05f;

    private void Start()
    {
      
       materials = GetComponent<MeshRenderer>().materials;
        foreach (var mat in materials)
        {
            if (mat.name.Contains("Outline"))
            {
                outlineMaterial = mat;
                break;
            }
        }
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        float dist = Vector3.Distance(player.position, transform.position);

        if (dist <= activationDistance)
        {
            outlineMaterial.SetFloat("_OutlineSize", outlineWidth); // activa
        }
        else
        {
            outlineMaterial.SetFloat("_OutlineSize", 0f); // desactiva
        }

        
    }
}
