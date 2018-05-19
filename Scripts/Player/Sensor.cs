using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sensor : MonoBehaviour
{
    private AudioManager am;

    private void Awake()
    {
        am = FindObjectOfType<AudioManager>();
    }

    private void OnTriggerEnter2D(Collider2D sensor)
    {
		if (sensor.CompareTag ("SensorD")) { // Если мы вошли в триггер begin
			if (transform.parent.GetComponent<Car> ().IsBlock())
            {
				transform.parent.GetComponent<Car> ().SetBackFlip(true);
				// здесь же можно прибавить (или умножить) очки за то что конкретно был сделан оборот (без заземления)
				transform.parent.GetComponent<Car> ().SetCurrValFlip(transform.parent.GetComponent<Car> ().GetCurrValFlip() + 1);
				transform.parent.GetComponent<Car> ().SetBlock(false);
                am.PlayBackflip();
			}
		}
	}

	private void OnTriggerExit2D(Collider2D sensor)
    {
		if (sensor.CompareTag ("SensorB"))
        { // Если мы вышли из триггера end
			transform.parent.GetComponent<Car> ().SetBlock(true);
		}
	}
}
