using UnityEngine;

public class KeyItem : MonoBehaviour
{
    public int locationID;
    public GameObject[] dependentItems;

	public void OnLockItem()
    {
		foreach (GameObject item in dependentItems)
        {
            item.GetComponent<Item>().Locked = false;
		}
	}
}
