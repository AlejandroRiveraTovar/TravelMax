using UnityEngine;

/// <summary>
/// Detecta si un objeto "Pickable" entra en el área de agarre del jugador.
/// Este script se encarga de indicar al <see cref="Player"/> si puede agarrar un objeto
/// y cuál es ese objeto disponible.
/// </summary>
[RequireComponent(typeof(Collider))]
public class CanHold : MonoBehaviour
{
    [Header("Estado de agarre")]
    [Tooltip("Indica si el jugador puede agarrar un objeto en este momento.")]
    public bool canHold;

    [Tooltip("Referencia al objeto que puede ser agarrado.")]
    public GameObject holdableObject = null;

    /// <summary>
    /// Detecta si un objeto entra en el trigger del área de agarre.
    /// </summary>
    /// <param name="other">El collider que entra en el área de detección.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickable"))
        {
            canHold = true;
            holdableObject = other.gameObject;
        }
        else
        {
            canHold = false;
            holdableObject = null;
        }
    }

    /// <summary>
    /// Limpia la referencia cuando un objeto sale del área de agarre.
    /// </summary>
    /// <param name="other">El collider que sale del área de detección.</param>
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == holdableObject)
        {
            canHold = false;
            holdableObject = null;
        }
    }
}
