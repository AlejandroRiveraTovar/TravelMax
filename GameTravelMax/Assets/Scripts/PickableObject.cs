using UnityEngine;

/// <summary>
/// Define los datos de un objeto que puede ser recogido por el jugador.
/// Estos datos se almacenan como ScriptableObject para poder reutilizarlos
/// entre distintas instancias del objeto en la escena.
/// </summary>
[CreateAssetMenu(
    fileName = "PickableObject",
    menuName = "Scriptable Objects/Pickable Object",
    order = 0)]
public class PickableObject : ScriptableObject
{
    [Header("Datos del Objeto")]

    [Tooltip("Nombre descriptivo del objeto (ej: Botella, Lata, Caja).")]
    public string objectName;

    [Tooltip("Valor numérico que aporta el objeto al eliminarse o recogerse.")]
    public int objectValue;
}
