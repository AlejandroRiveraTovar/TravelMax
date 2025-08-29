using UnityEngine;

/// <summary>
/// Controla la cámara para que siga suavemente a un objetivo con un desplazamiento definido.
/// Utiliza <see cref="Vector3.SmoothDamp"/> para un movimiento interpolado y natural.
/// </summary>
public class CameraFollow : MonoBehaviour
{
    [Header("Configuración de seguimiento")]
    [Tooltip("Transform del objetivo que la cámara debe seguir (ej: el jugador).")]
    public Transform target;

    [Tooltip("Tiempo de suavizado. Valores más altos producen un movimiento más lento y suave.")]
    [Range(0.01f, 1f)]
    public float smoothTime = 0.3f;

    [Tooltip("Desplazamiento de la cámara respecto al objetivo.")]
    public Vector3 offset = new Vector3(0f, 5f, -10f);

    /// <summary>
    /// Velocidad actual usada internamente por SmoothDamp.
    /// </summary>
    private Vector3 velocity = Vector3.zero;

    /// <summary>
    /// Actualiza la posición de la cámara en cada frame.
    /// </summary>
    private void LateUpdate()
    {
        if (target != null)
        {
            // Calculamos la posición deseada sumando el offset
            Vector3 targetPos = target.position + offset;

            // Movimiento suave hacia la posición deseada
            transform.position = Vector3.SmoothDamp(
                transform.position,
                targetPos,
                ref velocity,
                smoothTime
            );
        }
    }
}
