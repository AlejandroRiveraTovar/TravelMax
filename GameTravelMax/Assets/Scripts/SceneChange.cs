using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public string SceneName;

    public void loadScene(string scene) 
    {
        SceneManager.LoadScene(scene);
    }
}
