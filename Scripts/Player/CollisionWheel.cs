using UnityEngine;

public class CollisionWheel : MonoBehaviour {

	private bool isGrounded;
	private bool isDeathTrigger = false;

    private void OnCollisionStay2D(Collision2D other) {
		if (other.gameObject.CompareTag ("Ground")) {
            isGrounded = true;
		}
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.CompareTag ("Ground")) {
            isGrounded = false;
        }
    }

	private void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag ("DeathTrigger")) {
			isGrounded = true;
		}
	}

    public bool IsGrounded() {
        return isGrounded;
    }

	public bool IsDeathTrigger() {
		return isDeathTrigger;
	}
}
