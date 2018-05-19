using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyStore : MonoBehaviour {

	public GameObject content;
	public int coins;
	//public Text infoCoins;

	//	private void Awake() {
	//		PlayerPrefs.DeleteAll ();
	//	}

	private void Start() {
		GameParams.AddCoins (coins);
	}

	private void Update() {
		//infoCoins.text = PlayerPrefs.GetInt ("Coins").ToString();
	}
}
