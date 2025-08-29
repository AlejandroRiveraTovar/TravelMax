using UnityEngine;

/// <summary>
/// Asocia un ScriptableObject <see cref="PickableObject"/> a un prefab f�sico en la escena.
/// </summary>
public class Pickable : MonoBehaviour
{
    [Tooltip("Referencia al ScriptableObject con los datos del objeto.")]
    public PickableObject data;
}
