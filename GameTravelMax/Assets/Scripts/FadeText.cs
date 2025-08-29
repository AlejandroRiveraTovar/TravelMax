using TMPro;
using UnityEngine;
using System.Collections;

/// <summary>
/// Controla la visualización de mensajes temporales en pantalla con efecto de desvanecimiento.
/// Compatible con <see cref="TextMeshProUGUI"/>.
/// </summary>
public class FadeText : MonoBehaviour
{
    [Header("Referencias")]
    [Tooltip("Componente de texto donde se mostrará el mensaje.")]
    public TextMeshProUGUI textUI;

    [Header("Duraciones")]
    [Tooltip("Tiempo en segundos que el mensaje permanece completamente visible.")]
    public float displayTime = 2f;

    [Tooltip("Tiempo en segundos que tarda en desvanecerse el mensaje.")]
    public float fadeDuration = 1f;

    /// <summary>
    /// Referencia a la rutina de desvanecimiento en curso.
    /// </summary>
    private Coroutine fadeRoutine;

    private void Awake()
    {
        // Si no se asigna desde el inspector, intenta obtenerlo del mismo GameObject
        if (textUI == null)
            textUI = GetComponent<TextMeshProUGUI>();

        // Estado inicial: texto vacío e invisible
        textUI.text = "";
        textUI.alpha = 0f;
    }

    /// <summary>
    /// Muestra un mensaje en pantalla de forma temporal.
    /// Si ya hay un mensaje activo, reinicia la animación.
    /// </summary>
    /// <param name="message">Texto a mostrar en pantalla.</param>
    public void ShowMessage(string message)
    {
        // Cancelamos cualquier animación anterior
        if (fadeRoutine != null)
            StopCoroutine(fadeRoutine);

        // Iniciamos una nueva
        fadeRoutine = StartCoroutine(FadeRoutine(message));
    }

    /// <summary>
    /// Rutina que gestiona la aparición, permanencia y desvanecimiento del mensaje.
    /// </summary>
    private IEnumerator FadeRoutine(string message)
    {
        textUI.text = message;

        // Mostrar instantáneamente
        textUI.alpha = 1f;

        // Espera antes de empezar a desvanecer
        yield return new WaitForSeconds(displayTime);

        // Proceso de desvanecimiento
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            textUI.alpha = Mathf.Lerp(1f, 0f, t / fadeDuration);
            yield return null;
        }

        // Vaciar texto al final
        textUI.text = "";
        textUI.alpha = 0f;
    }
}
