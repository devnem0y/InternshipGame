using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    private GameManager gm;
    private float carSpeedX = 0f;
    private float carSpeedY = 0f;

    private float minCamSize = 8f;
	private float maxCamSize = 11f;

	private float smoothZoomOut = 0f;
	private float smoothZoomIn = 0f;

    private bool isShake = true;

    private void Awake()
    {
        gm = FindObjectOfType<GameManager>();
    }

	private void FixedUpdate()
	{
        if (gm.car.activeSelf && !gm.car.GetComponent<Car>().IsCrash())
        {
            Vector3 newPosition = gm.car.transform.position;
            newPosition.z = -10;
            newPosition.y += 3.5f;
            newPosition.x += 3;

            transform.position = newPosition;
        }
	}

	private void Update()
	{
        smoothZoomOut = Mathf.Lerp(GetComponent<Camera>().orthographicSize, maxCamSize, Time.deltaTime * SmoothTime(10f));
		smoothZoomIn = Mathf.Lerp(GetComponent<Camera>().orthographicSize, minCamSize, Time.deltaTime * SmoothTime(10f));

        if (gm.car.activeSelf)
        {
            carSpeedX = gm.car.GetComponent<Car>().GetSpeed();
            carSpeedY = gm.car.GetComponent<Car>().GetVerticalSpeed();
        }

        if ((Mathf.Abs(carSpeedX) >= 8f) || (Mathf.Abs(carSpeedY) >= 2.5f)) GetComponent<Camera>().orthographicSize = smoothZoomOut;
        else if ((Mathf.Abs(carSpeedX) <= 8f) || (Mathf.Abs(carSpeedY) <= 2.5f)) GetComponent<Camera>().orthographicSize = smoothZoomIn;
    }

    private float SmoothTime(float parameter)
    {
        return Mathf.Abs(carSpeedX) / parameter;
    }

    public IEnumerator Shake(float duration, float magnitude)
    {
        if (isShake)
        {
            Vector3 originalPos = transform.localPosition;
            Quaternion originalRot = transform.localRotation;

            float elapsed = 0f;

            while (elapsed < duration)
            {
                float x = Random.Range(-1f, 1f) * magnitude;
                float y = Random.Range(-1f, 1f) * magnitude;
                float z = Random.Range(-0.02f, 0.02f) * magnitude;

                transform.localPosition = new Vector3(transform.position.x + x, transform.position.y + y, originalPos.z);
                transform.localRotation = new Quaternion(originalRot.x, originalRot.y, transform.localRotation.z + z, originalRot.w);
                elapsed += Time.deltaTime;

                yield return null;
            }

            transform.localRotation = originalRot;
            isShake = false;
        }
    }
}
