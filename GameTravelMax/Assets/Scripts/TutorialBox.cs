using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class TutorialBox : MonoBehaviour
{
    [Header("Referencias UI")]
    [SerializeField] private RectTransform panelTutorial;
    [SerializeField] private TextMeshProUGUI textoUI;

    [Header("Mensajes")]
    [SerializeField] private string[] mensajes;
    [SerializeField] private KeyCode teclaAvanzar = KeyCode.Space;
    [SerializeField] private float velocidadEscritura = 0.05f;
    public List<GameObject> walls;
    private int indiceActual = 0;
    private bool escribiendo = false;
    private Timer timer; // <-- referencia al temporizador

    

    private void Start()
    {
        // Buscar el Timer en la escena
        timer = FindFirstObjectByType<Timer>();
        // Pausa el movimiento del jugador 
        
        if (timer != null) timer.Pausado = true; // Pausar al inicio del tutorial

        if (mensajes.Length > 0)
        {
            panelTutorial.gameObject.SetActive(true);
            StartCoroutine(EscribirMensaje(mensajes[indiceActual]));
        }
        else
        {
            panelTutorial.gameObject.SetActive(false);
            if (timer != null) timer.Pausado = false; // Si no hay tutorial, arrancar el tiempo
        }
    }

    private void Update()
    {
        if (panelTutorial.gameObject.activeSelf && Input.GetKeyDown(teclaAvanzar))
        {
            if (escribiendo)
            {
                StopAllCoroutines();
                textoUI.text = mensajes[indiceActual];
                escribiendo = false;
            }
            else
            {
                SiguienteMensaje();
            }
        }
    }

    private void SiguienteMensaje()
    {
        indiceActual++;

        if (indiceActual < mensajes.Length)
        {
            StartCoroutine(EscribirMensaje(mensajes[indiceActual]));
        }
        else
        {
            panelTutorial.gameObject.SetActive(false);
            if (timer != null) timer.Pausado = false; for (int i = 0; i < walls.Count; i++) { walls[i].SetActive(false); };  // Reanudar cuando acaben los mensajes
        }
    }

    private IEnumerator EscribirMensaje(string mensaje)
    {
        escribiendo = true;
        textoUI.text = "";

        foreach (char letra in mensaje.ToCharArray())
        {
            textoUI.text += letra;
            yield return new WaitForSeconds(velocidadEscritura);
        }

        escribiendo = false;
    }
}
