using UnityEngine;
using TMPro;
using System.IO;
using System.Collections.Generic;

public class FormularioRegistro : MonoBehaviour
{
    [Header("Campos del Formulario")]
    public TMP_InputField inputNombre;
    public TMP_InputField inputEmail;
    public TMP_InputField inputEdad;
    public TMP_InputField inputCiudad;
    public TMP_Text scoreText;

    [Header("GameManager")]
    public GameManager gameManager;

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
    private string ruta;

    private void Awake()
    {
        ruta = Path.Combine(Application.persistentDataPath, "usuarios.json");
        CargarJSON();
        gameManager = FindFirstObjectByType<GameManager>();
        scoreText.text = gameManager.score.ToString();
    }

    public void EnviarFormulario()
    {
        // Crear usuario nuevo con datos del formulario
        DatosUsuario nuevoUsuario = new DatosUsuario();
        nuevoUsuario.nombre = inputNombre.text;
        nuevoUsuario.email = inputEmail.text;

        if (int.TryParse(inputEdad.text, out int edadParseada))
            nuevoUsuario.edad = edadParseada;
        else
            nuevoUsuario.edad = 0;

        nuevoUsuario.ciudad = inputCiudad.text;
        nuevoUsuario.score = gameManager.score;
        // Agregar a la lista
        lista.usuarios.Add(nuevoUsuario);

        // Guardar en JSON
        GuardarJSON();
        
        Debug.Log("Usuario agregado y guardado: " + JsonUtility.ToJson(nuevoUsuario, true));
    }

    private void GuardarJSON()
    {
        string json = JsonUtility.ToJson(lista, true);
        File.WriteAllText(ruta, json);
        Debug.Log("Lista guardada en: " + ruta);
    }

    private void CargarJSON()
    {
        if (File.Exists(ruta))
        {
            string json = File.ReadAllText(ruta);
            lista = JsonUtility.FromJson<ListaUsuarios>(json);
            if (lista == null) lista = new ListaUsuarios();
            Debug.Log("Lista cargada con " + lista.usuarios.Count + " usuarios.");
        }
        else
        {
            lista = new ListaUsuarios();
            Debug.Log("No se encontró archivo, se creó lista nueva.");
        }
    }
}