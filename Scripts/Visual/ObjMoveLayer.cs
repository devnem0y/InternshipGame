using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjMoveLayer : MonoBehaviour {

	public GameObject begin;
	public GameObject end;

	public float speed = 0f;

	private void Update () {
		transform.Translate (Vector3.right * Time.deltaTime * speed);
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag ("End")) {
			transform.position = begin.transform.position;
		}
	}
}
