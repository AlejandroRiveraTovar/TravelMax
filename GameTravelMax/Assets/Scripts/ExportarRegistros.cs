using UnityEngine;
using TMPro;
using System.IO;
using System.Collections.Generic;

public class ExportarRegistros : MonoBehaviour
{
    private string rutaOrigen;
    private string rutaDestino;

    void Start()
    {
        // Ruta donde Unity guarda el JSON
        rutaOrigen = Path.Combine(Application.persistentDataPath, "usuarios.json");

        // Ruta de exportación (ejemplo: Escritorio del usuario)
#if UNITY_STANDALONE || UNITY_EDITOR
        rutaDestino = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop), "usuarios_exportados.json");
#else
        rutaDestino = Path.Combine(Application.persistentDataPath, "usuarios_exportados.json");
#endif
    }

    // Este método puedes llamarlo desde un botón
    public void Exportar()
    {
        if (File.Exists(rutaOrigen))
        {
            File.Copy(rutaOrigen, rutaDestino, true);
            Debug.Log("Archivo exportado a: " + rutaDestino);
        }
        else
        {
            Debug.LogWarning("No se encontró el archivo de usuarios en: " + rutaOrigen);
        }
    }
}
