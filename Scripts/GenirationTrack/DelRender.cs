using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelRender : MonoBehaviour {

	private void Start () {
		transform.gameObject.GetComponent<SpriteRenderer> ().enabled = false;
	}
}
