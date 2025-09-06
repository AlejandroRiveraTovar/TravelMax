using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para detectar cambio de escenas

/// <summary>
/// Controlador principal del juego.
/// Administra el conteo de objetos eliminados, la puntuación acumulada
/// y actualiza la interfaz de usuario.
/// </summary>
public class GameManager : MonoBehaviour
{
    [Header("Estadísticas de juego")]
    [Tooltip("Puntuación total acumulada por los objetos eliminados.")]
    public int score;

    [Tooltip("Cantidad total de objetos eliminados.")]
    public int objectCounter;

    [Header("Inventario de objetos eliminados")]
    [Tooltip("Lista de ScriptableObjects que representan los objetos eliminados.")]
    public List<PickableObject> eliminatedObjects = new List<PickableObject>();

    public int score_tiempoSobra;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Evitar instancias duplicadas
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Persistir entre escenas

        // Suscribirse al evento de cambio de escena
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        // Evitar errores al destruir la instancia
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    /// <summary>
    /// Evento que se llama cada vez que se carga una nueva escena.
    /// </summary>
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MenuInicio")
        {
            ResetStats();
        }
    }

    /// <summary>
    /// Reinicia las estadísticas y listas del GameManager.
    /// </summary>
    private void ResetStats()
    {
        score = 0;
        objectCounter = 0;
        score_tiempoSobra = 0;
        eliminatedObjects.Clear();
    }

    /// <summary>
    /// Registra un objeto eliminado en la zona de DropZone.
    /// </summary>
    /// <param name="objData">Datos del objeto eliminado (nombre y valor) provenientes de su ScriptableObject.</param>
    public void RegisterDrop(PickableObject objData)
    {
        objectCounter++;
        score += objData.objectValue;
        eliminatedObjects.Add(objData);
    }
}
