using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

/// <summary>
/// Temporizador regresivo que se muestra en pantalla.
/// Al llegar a cero, cambia de escena.
/// </summary>
public class Timer : MonoBehaviour
{
    #region Variables
    [SerializeField] private TMP_Text timerText;  // Texto donde se muestra el tiempo
    [SerializeField, Tooltip("Tiempo en segundos")] private float timerTime;

    private int minutes, seconds, cents;
    private float startTime;

    public bool Pausado { get; set; } = false; // <-- NUEVO: bandera de pausa
    #endregion

    void Start()
    {
        startTime = timerTime;
    }

    void Update()
    {
        if (Pausado) return; // <-- Si está pausado, no hacer nada

        timerTime -= Time.deltaTime;
        if (timerTime < 0) timerTime = 0;

        minutes = (int)(timerTime / 60f);
        seconds = (int)(timerTime - minutes * 60f);
        cents = (int)((timerTime - (int)timerTime) * 100f);

        timerText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, cents);

        if (timerTime == 0)
        {
            SceneManager.LoadScene("Perdiste");
        }
    }

    internal float GetRemainingTime()
    {
        return timerTime;
    }
}
