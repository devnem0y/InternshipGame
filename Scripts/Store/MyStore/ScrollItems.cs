using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollItems : MonoBehaviour {

	public GameObject content;
	private Vector3 screenPoint, offset;
	private float lockedYPos;

	private void Start() {
		screenPoint = new Vector3 (0, 0, 0f);
	}

	private void Update() {
		if (content.transform.position.x > 0f)
			content.transform.position = Vector3.MoveTowards (content.transform.position, new Vector3(0f, content.transform.position.y, content.transform.position.z), Time.deltaTime * 10f);
		else if(content.transform.position.x < -13f)
			content.transform.position = Vector3.MoveTowards (content.transform.position, new Vector3(-13f, content.transform.position.y, content.transform.position.z), Time.deltaTime * 10f);
	}

	private void OnMouseDown() {
		lockedYPos = screenPoint.x;
		offset = content.transform.position - Camera.main.ScreenToWorldPoint (new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z + 10f));
		Cursor.visible = false;
	}

	private void OnMouseDrag() {
		Vector3 curScreenPoint = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPoint.z + 10f);
		Vector3 curPosition = Camera.main.ScreenToWorldPoint (curScreenPoint) + offset;
		curPosition.y = lockedYPos;
		content.transform.position = curPosition;
	}

	private void OnMouseUp() {
		Cursor.visible = true;
	}
}
