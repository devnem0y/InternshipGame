using UnityEngine;

public class Carpet : MonoBehaviour {

    private bool isCarpet;

    private void OnCollisionEnter2D(Collision2D col)
    { // Если игрок перевернулся
        if (col.gameObject.tag == "Ground")
        {
	        isCarpet = true;
        }
    }

    public bool IsCarpet()
    {
        return isCarpet;
    }
}
