using UnityEngine;

public class Exit : MonoBehaviour
{
    /// <summary>
    /// Cierra la aplicación (solo funciona en compilados).
    /// </summary>
    public void Salir()
    {
        Debug.Log("Salir del juego");
        Application.Quit();
    }
}
