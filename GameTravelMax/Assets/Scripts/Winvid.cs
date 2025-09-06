using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.Video;

public class Winvid : MonoBehaviour
{
    public GameObject canvas;
    public VideoPlayer videoPlayer;
    public AudioSource audioSource;
    void Start()
    {
        canvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
      canvas.SetActive(true);
      audioSource.Play();
    }
}
