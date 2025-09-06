using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class TextManager : MonoBehaviour
{
    [Header("Referencias de UI")]
    [Tooltip("Texto de la UI que muestra la cantidad de objetos eliminados.")]
    public TMPro.TextMeshProUGUI textCounter;

    [Tooltip("Texto de la UI que muestra la puntuación acumulada.")]
    public TMPro.TextMeshProUGUI textScore;

    private int objectCounter;
    private int score;
    private GameManager gameManager;
    void Start()
    {
        gameManager = GameObject.FindFirstObjectByType<GameManager>();
    }

    /// <summary>
    /// Actualiza la UI cada frame con los valores actuales de contador y puntuación.
    /// </summary>
    private void Update()
    {
        objectCounter = gameManager.objectCounter;
        score = gameManager.score;
        textCounter.text = objectCounter.ToString();
        textScore.text = score.ToString();
    }
}
