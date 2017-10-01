using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelector : MonoBehaviour
{
    public static LevelSelector s;

    void Start()
    {
		if (s != null && s != this)
        {
            Destroy(gameObject);
            return;
        }
		SceneManager.sceneLoaded += OnSceneLoaded;
        s = this;
        DontDestroyOnLoad(this);
    }

	bool isLoading = false;
    public void SelectLevel(int Level)
    {
		if (!isLoading) {
			SceneManager.LoadScene (Level);
			isLoading = true;
		}
    }

	void OnSceneLoaded (Scene scene, LoadSceneMode mode){
		isLoading = false;
		/*foreach (MonoBehaviour mono in FindObjectsOfType<MonoBehaviour>()) {
			mono.BroadcastMessage ("Start");
		}*/
	}
}
