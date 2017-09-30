using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class GridSnap : MonoBehaviour {
	#if UNITY_EDITOR

	public enum snapType {normal, floor, obj, wall};
	public snapType mySnapType = snapType.normal;

	void Update () {
		if (!Application.isPlaying) {
			float posSnap = 1f;
			float rotSnap = 45f;
			switch(mySnapType){
			case snapType.normal:
				posSnap = 1f;
				rotSnap = 45f;
				break;
			case snapType.floor:
				posSnap = 10f;
				rotSnap = 45f;
				break;
			case snapType.obj:
				posSnap = 2f;
				rotSnap = 45f;
				break;
			case snapType.wall:
				posSnap = 10f;
				rotSnap = 90f;
				break;
			}

			transform.position = new Vector3 (((int)(transform.position.x / posSnap)) * posSnap, 0, ((int)(transform.position.z / posSnap)) * posSnap);
			transform.rotation = Quaternion.Euler (((int)(transform.rotation.eulerAngles.x / rotSnap)) * rotSnap, ((int)(transform.rotation.eulerAngles.y / rotSnap)) * rotSnap, ((int)(transform.rotation.eulerAngles.z / rotSnap)) * rotSnap);

		}
	}
	#endif
}
