using UnityEngine;
using UnityEngine.UI;

public class ColorChanger : MonoBehaviour
{
    public Text MyText;

    public float Delay = 0.5f;

    void Start()
    {
        InvokeRepeating("SetColor", 0, Delay);
    }

    void SetColor()
    {
        MyText.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
	}
}
