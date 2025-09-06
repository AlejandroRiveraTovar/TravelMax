using TMPro;
using UnityEngine;

public class TiempoSobra : MonoBehaviour
{
    public GameManager gm;
    private int score_ts;
    public TMP_Text text;
    void Start()
    {
        gm = GameObject.FindFirstObjectByType<GameManager>();
        score_ts = gm.score_tiempoSobra;
        text.text = $"+{score_ts.ToString()} puntos por tiempo de sobra!";
    }

    
}
