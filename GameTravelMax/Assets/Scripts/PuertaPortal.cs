using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuertaPortal : MonoBehaviour
{
    [SerializeField] private Timer timer;
    [SerializeField] private GameManager gm;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gm.score += (int)timer.timerTime * 10;
            SceneManager.LoadScene("Ganaste");
        }
    }
}
