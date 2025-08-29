using UnityEngine;

/// <summary>
/// Zona de eliminación de objetos "Pickable".
/// Cuando un objeto con el tag "Pickable" entra en el trigger,
/// este script notifica al <see cref="GameManager"/> y destruye el objeto.
/// </summary>
[RequireComponent(typeof(Collider))]
public class DropZone : MonoBehaviour
{
    [Header("Referencias")]
    [Tooltip("Referencia al GameManager encargado de llevar el conteo y la puntuación.")]
    public GameManager gameManager;

    /// <summary>
    /// Detecta cuando un objeto entra en el área de la zona de eliminación.
    /// </summary>
    /// <param name="other">El collider del objeto que entra en el trigger.</param>
    /// <remarks>
    /// - Solo afecta a objetos con el tag "Pickable".  
    /// - Llama a <see cref="GameManager.RegisterDrop"/> pasando la información del objeto.  
    /// - Luego destruye el objeto de la escena.  
    /// </remarks>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickable"))
        {
            Pickable pickable = other.GetComponent<Pickable>();
            if (pickable != null)
            {
                // Enviamos los datos al GameManager para registrar el objeto eliminado
                gameManager.RegisterDrop(pickable.data);

                // Eliminamos la instancia de la escena (no el prefab de la lista)
                Destroy(other.gameObject);
            }
        }
    }
}
