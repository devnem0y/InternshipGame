using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectItems : MonoBehaviour {

	//public Button apply, buy;
	public string thisItem;

	private void Start() {
		if (PlayerPrefs.GetString ("Item_0") != "Open")
			PlayerPrefs.SetString ("Item_0", "Open");
	}

	private void OnTriggerEnter2D(Collider2D other) {
		thisItem = other.gameObject.name;
		other.transform.localScale += new Vector3 (0.4f, 0.4f, 0f);
		if (PlayerPrefs.GetString (other.gameObject.name) == "Open") {
			//apply.gameObject.SetActive (true);
			//buy.gameObject.SetActive (false);
		} else {
			//apply.gameObject.SetActive (false);
			//buy.gameObject.SetActive (true);
		}
		// Можно сделать проверку на количество монет, и если их не хватает,
		// то спрайт делаем серенький и кнопку "Купить" interactable = false
	}

	private void OnTriggerExit2D(Collider2D other) {
		other.transform.localScale -= new Vector3 (0.4f, 0.4f, 0f);
	}
}
