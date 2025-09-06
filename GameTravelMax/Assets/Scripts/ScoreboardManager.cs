using UnityEngine;
using TMPro;
using System.IO;
using System.Collections.Generic;

/// <summary>
/// Muestra en pantalla un scoreboard con los usuarios ordenados por score.
/// </summary>
public class ScoreboardManager : MonoBehaviour
{
    [Header("UI")]
    [Tooltip("Texto donde se mostrará la lista de jugadores y sus puntajes.")]
    public TMP_Text scoreboardText;

    private string ruta;

    [System.Serializable]
    public class DatosUsuario
    {
        public string nombre;
        public string email;
        public int edad;
        public string ciudad;
        public int score;
    }

    [System.Serializable]
    public class ListaUsuarios
    {
        public List<DatosUsuario> usuarios = new List<DatosUsuario>();
    }

    private ListaUsuarios lista = new ListaUsuarios();

    private void Awake()
    {
        ruta = Path.Combine(Application.persistentDataPath, "usuarios.json");
        CargarJSON();
        MostrarScoreboard();
    }

    /// <summary>
    /// Carga los datos desde el archivo JSON.
    /// </summary>
    public void CargarJSON()
    {
        if (File.Exists(ruta))
        {
            string json = File.ReadAllText(ruta);
            lista = JsonUtility.FromJson<ListaUsuarios>(json);
            if (lista == null) lista = new ListaUsuarios();
        }
        else
        {
            lista = new ListaUsuarios();
            Debug.LogWarning("No se encontró archivo de usuarios, lista vacía.");
        }
    }

    /// <summary>
    /// Ordena los usuarios por score y los muestra en el TMP.
    /// </summary>
    public void MostrarScoreboard()
    {
        if (scoreboardText == null) return;

        // Ordenar de mayor a menor score
        lista.usuarios.Sort((a, b) => b.score.CompareTo(a.score));

        // Construir el texto
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        
      
        for (int i = 0; i < lista.usuarios.Count; i++)
        {
            var user = lista.usuarios[i];
            sb.AppendLine($"{i + 1}. {user.nombre} - {user.score} pts");
        }

        scoreboardText.text = sb.ToString();
    }
}
