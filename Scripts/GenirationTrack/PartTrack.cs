using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartTrack : MonoBehaviour {

	public void Setup(Vector3 pos) {
		transform.position = pos;
		Instantiate (gameObject);
	}

	public Vector3 GetPosOut() {
		return transform.GetChild (0).GetComponent<Transform> ().position;
	}

	private void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.name == "TriggerRemovePart") {
			Destroy (gameObject);
		}
	}

	public void NewPosition(Vector3 pos) {
		transform.position = pos;
	}
}