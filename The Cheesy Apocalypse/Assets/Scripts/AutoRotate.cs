using UnityEngine;

public class AutoRotate : MonoBehaviour
{
    public Vector3 RotateAxis;

	void Update()
    {
        transform.Rotate(RotateAxis * Time.deltaTime);
    }
}
