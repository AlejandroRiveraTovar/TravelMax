using UnityEngine;

/// <summary>
/// Ajusta la velocidad (pitch) de un AudioSource de acuerdo al tiempo de un script Timer.
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class AudioSpeedController : MonoBehaviour
{
    [Header("Referencias")]
    public Timer timerScript;        // Script Timer que contiene la variable timerTime
    private AudioSource audioSource; // Componente de audio

    [Header("Configuración")]
    public float minPitch = 1f;      // Velocidad mínima (normal = 1)
    public float maxPitch = 3f;      // Velocidad máxima
    public float maxTime = 45f;      // Tiempo máximo esperado para normalizar

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (timerScript == null)
        {
            Debug.LogError("No se ha asignado el script Timer.");
        }
    }

    private void Update()
    {
        if (timerScript == null) return;
        
        
            // Normaliza el tiempo (0 a 1)
            float normalized = Mathf.Clamp01(timerScript.timerTime / maxTime);

            // Invertimos el valor para que baje con el tiempo
            float inverted = 1f - normalized;

            // Aplicamos curva exponencial (más gradual al inicio)
            float curved = Mathf.Pow(inverted, 2f); // prueba con 2, 3 o 4

            // Lerp con la curva aplicada
            float newPitch = Mathf.Lerp(minPitch, maxPitch, curved);

            audioSource.pitch = newPitch;
        


    }
}
