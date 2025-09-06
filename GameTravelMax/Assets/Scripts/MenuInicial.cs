using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class MenuInicial : MonoBehaviour
{
    [Header("Video Settings")]
    public VideoPlayer videoPlayer;   // Referencia al componente VideoPlayer
    public string sceneToLoad = "SampleScene"; // Escena que se cargará mientras se reproduce el video
    public AudioSource audioSource1;
    public AudioSource audioSource2;
    private AsyncOperation asyncLoad; // Para cargar la escena en segundo plano

    /// <summary>
    /// Inicia el proceso de jugar: reproduce el video y carga la escena.
    /// </summary>
    public void Jugar()
    {
        audioSource1.Stop();
        audioSource2.Stop();
        if (videoPlayer != null)
        {
            // Ocupa el callback para saber cuando termina el video
            videoPlayer.loopPointReached += OnVideoFinished;

            // Inicia la reproducción del video
            videoPlayer.Play();

            // Empieza a cargar la escena en segundo plano
            StartCoroutine(CargarEscenaAsync());
        }
        else
        {
            Debug.LogWarning("No hay VideoPlayer asignado. Cargando escena directamente.");
            SceneManager.LoadScene(sceneToLoad);
        }
    }

    /// <summary>
    /// Cierra la aplicación (solo funciona en compilados).
    /// </summary>
    public void Salir()
    {
        Debug.Log("Salir del juego");
        Application.Quit();
    }

    /// <summary>
    /// Corrutina que carga la escena en segundo plano mientras se reproduce el video.
    /// </summary>
    private IEnumerator CargarEscenaAsync()
    {
        asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad);
        asyncLoad.allowSceneActivation = false; // Espera hasta que termine el video

        while (!asyncLoad.isDone)
        {
            // Se queda esperando hasta que termine el video
            yield return null;
        }
    }

    /// <summary>
    /// Se llama automáticamente cuando el video termina.
    /// </summary>
    private void OnVideoFinished(VideoPlayer vp)
    {
        // Una vez que termina el video, activa la escena
        if (asyncLoad != null)
        {
            asyncLoad.allowSceneActivation = true;
        }
        else
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
