using UnityEngine;

public class Coin : MonoBehaviour {

    private GameManager gm;
    private AudioManager am;
    private bool move;
    private float timerDestroy = 0f;
	private float speed = 11f;

    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
        am = FindObjectOfType<AudioManager>();
    }

    private void FixedUpdate ()
    {
		if (move) {
            Vector3 newVector = gm.car.transform.position; // vector obj car
			transform.position = Vector3.MoveTowards(transform.position, newVector, speed * Time.fixedDeltaTime);
			if (timerDestroy >= 0.35f) {
				Destroy (gameObject);
				timerDestroy = 0f;
			}
			timerDestroy += Time.deltaTime;
		}
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!gm.car.GetComponent<Car>().IsCrash()) {
            if (other.CompareTag("SensorCoins")) {
                move = true;
                GameParams.AddCoins(1);
                am.PlayCoin();
            } else if (other.CompareTag("Player")) {
                Destroy(gameObject);
            }
        }
	}
}
