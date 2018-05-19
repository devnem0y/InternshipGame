using UnityEngine;

public class Controller : MonoBehaviour {

	private  void OnMouseDown() {
        OnTouch.SetTouch(true);
    }

    private  void OnMouseUp() {
		OnTouch.SetTouch(false);
    }
}
