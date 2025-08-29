using UnityEngine;

/// <summary>
/// Controla la c�mara para que siga suavemente a un objetivo con un desplazamiento definido.
/// Utiliza <see cref="Vector3.SmoothDamp"/> para un movimiento interpolado y natural.
/// </summary>
public class CameraFollow : MonoBehaviour
{
    [Header("Configuraci�n de seguimiento")]
    [Tooltip("Transform del objetivo que la c�mara debe seguir (ej: el jugador).")]
    public Transform target;

    [Tooltip("Tiempo de suavizado. Valores m�s altos producen un movimiento m�s lento y suave.")]
    [Range(0.01f, 1f)]
    public float smoothTime = 0.3f;

    [Tooltip("Desplazamiento de la c�mara respecto al objetivo.")]
    public Vector3 offset = new Vector3(0f, 5f, -10f);

    /// <summary>
    /// Velocidad actual usada internamente por SmoothDamp.
    /// </summary>
    private Vector3 velocity = Vector3.zero;

    /// <summary>
    /// Actualiza la posici�n de la c�mara en cada frame.
    /// </summary>
    private void LateUpdate()
    {
        if (target != null)
        {
            // Calculamos la posici�n deseada sumando el offset
            Vector3 targetPos = target.position + offset;

            // Movimiento suave hacia la posici�n deseada
            transform.position = Vector3.SmoothDamp(
                transform.position,
                targetPos,
                ref velocity,
                smoothTime
            );
        }
    }
}
