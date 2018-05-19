using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerRemoveEl : MonoBehaviour {

	private bool removeEl;

	private void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.CompareTag ("Part")) {
			removeEl = true;
		}
	}

	public bool IsRemoveEl() {
		return removeEl;
	}

	public void SetRemoveEl(bool removeEl) {
		this.removeEl = removeEl;
	}
}
