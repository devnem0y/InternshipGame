using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SnapScrolling : MonoBehaviour {

	public GameObject[] instItems;
	private Vector2[] itemPos;
    private Vector2[] itemScale;
	private Vector2 contentVector;

	private RectTransform contentRect;
    public ScrollRect scrollRect;

	[Range (0, 500)]
	public int itemOffSet;
    [Range (0f, 100f)]
    public float scaleOffSet;
    [Range (0, 50)]
    public float scaleSpeed;
    [Range (0f, 100f)]
    public float snapSpeed;
    [Range (0f, 1000f)]
    public float velocityScrolling;

	private int selectedItemID;
    private bool isScrolling;

	private void Start () {
		contentRect = GetComponent<RectTransform> ();

		itemPos = new Vector2[instItems.Length];
		itemScale = new Vector2[instItems.Length];

		for (int i = 0; i < instItems.Length; i++) {
			if (i == 0) continue;
			instItems [i].transform.localPosition = new Vector2 (instItems[i - 1].transform.localPosition.x + 
				instItems[i].GetComponent<RectTransform>().sizeDelta.x + itemOffSet, 
				instItems[i].transform.localPosition.y);
			itemPos[i] = -instItems[i].transform.localPosition;
			//instItems [i].GetComponent<ItemState> ().SetId (i);
		}
	}

	private void FixedUpdate() {
		if (contentRect.anchoredPosition.x >= itemPos[0].x && !isScrolling || contentRect.anchoredPosition.x <= itemPos[itemPos.Length - 1].x && !isScrolling) {
            scrollRect.inertia = false;
        }
        float nearestPos = float.MaxValue;
		for (int i = 0; i < instItems.Length; i++) {
			float distance = Mathf.Abs (contentRect.anchoredPosition.x - itemPos[i].x);
			if (distance < nearestPos) {
				nearestPos = distance;
				selectedItemID = i;
			}
            // Изменяем масштаб Item по индексу (по аналогии можно менять альфа канал)
            float scale = Mathf.Clamp(1 / (distance / itemOffSet) * scaleOffSet, 0.5f, 1.6f);
            itemScale[i].x = Mathf.SmoothStep(instItems[i].transform.localScale.x, scale + 0.2f, scaleSpeed * Time.fixedDeltaTime);
            itemScale[i].y = Mathf.SmoothStep(instItems[i].transform.localScale.y, scale + 0.2f, scaleSpeed * Time.fixedDeltaTime);
            instItems[i].transform.localScale = itemScale[i];
			if (instItems[i].transform.localScale.x < 1.8f && instItems[i].transform.localScale.y < 1.8f) { // когда эллемент не в фокусе выполняем этот блок
                instItems[selectedItemID].transform.GetChild(1).GetComponent<Button>().interactable = false;
            }
		}
        float scrollVelocity = Mathf.Abs(scrollRect.velocity.x);
		if (scrollVelocity < velocityScrolling && !isScrolling) scrollRect.inertia = false;

		if (isScrolling || scrollVelocity > velocityScrolling) return;
        // Делаем привязку к объекту по индексу selectedItemID
        contentVector.x = Mathf.SmoothStep(contentRect.anchoredPosition.x, itemPos[selectedItemID].x, snapSpeed * Time.fixedDeltaTime);
        contentRect.anchoredPosition = contentVector;
        // когда элемент в фокусе выполняем далее строчки
        instItems[selectedItemID].transform.GetChild(1).GetComponent<Button>().interactable = true;
	}

    public void Scrolling(bool scroll) {
		isScrolling = scroll;
		if (scroll) scrollRect.inertia = true;
    }

	public void TaskOnClick() {
		Debug.Log("OnClicked " + selectedItemID);
    }
}
