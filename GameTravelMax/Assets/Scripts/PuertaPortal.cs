using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuertaPortal : MonoBehaviour
{
    [SerializeField] private Timer timer;
    private GameManager gm;

    private void Start()
    {
        gm= GameObject.FindFirstObjectByType<GameManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gm.score_tiempoSobra = (int)timer.timerTime * 10;
            gm.score += (int)timer.timerTime * 10;
            SceneManager.LoadScene("Ganaste");
        }
    }
}
