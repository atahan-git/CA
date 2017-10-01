using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public static LevelSelector s;

    void Start()
    {
        s = this;
        DontDestroyOnLoad(this);
    }

    public void SelectLevel(int Level)
    {
        SceneManager.LoadScene(Level);
    }
}
