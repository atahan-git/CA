using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public void SelectLevel(int Level)
    {
        SceneManager.LoadScene(Level);
    }
}
