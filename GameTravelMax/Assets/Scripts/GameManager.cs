using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controlador principal del juego.
/// Administra el conteo de objetos eliminados, la puntuaci�n acumulada
/// y actualiza la interfaz de usuario.
/// </summary>
public class GameManager : MonoBehaviour
{
    [Header("Estad�sticas de juego")]
    [Tooltip("Puntuaci�n total acumulada por los objetos eliminados.")]
    public int score;

    [Tooltip("Cantidad total de objetos eliminados.")]
    public int objectCounter;

    [Header("Referencias de UI")]
    [Tooltip("Texto de la UI que muestra la cantidad de objetos eliminados.")]
    public TMPro.TextMeshProUGUI textCounter;

    [Tooltip("Texto de la UI que muestra la puntuaci�n acumulada.")]
    public TMPro.TextMeshProUGUI textScore;

    [Tooltip("Referencia al script encargado de mostrar mensajes desvanecientes en pantalla.")]
    public FadeText fadeText;

    [Header("Inventario de objetos eliminados")]
    [Tooltip("Lista de ScriptableObjects que representan los objetos eliminados.")]
    public List<PickableObject> eliminatedObjects = new List<PickableObject>();

    /// <summary>
    /// Actualiza la UI cada frame con los valores actuales de contador y puntuaci�n.
    /// </summary>
    private void Update()
    {
        textCounter.text = objectCounter.ToString();
        textScore.text = score.ToString();
    }

    /// <summary>
    /// Registra un objeto eliminado en la zona de DropZone.
    /// </summary>
    /// <param name="objData">Datos del objeto eliminado (nombre y valor) provenientes de su ScriptableObject.</param>
    /// <remarks>
    /// - Incrementa el contador de objetos.  
    /// - Suma su valor a la puntuaci�n total.  
    /// - Guarda el objeto en la lista de eliminados.  
    /// - Muestra un mensaje temporal en pantalla si se asign� <see cref="fadeText"/>.  
    /// </remarks>
    public void RegisterDrop(PickableObject objData)
    {
        objectCounter++;
        score += objData.objectValue;
        eliminatedObjects.Add(objData);

        // Mostrar mensaje en pantalla con fade-out
        if (fadeText != null)
            fadeText.ShowMessage($"Se elimin� {objData.objectName} (+{objData.objectValue})");
    }
}
