using UnityEngine;

public class LocationStore : MonoBehaviour
{
    public Sprite complete, tab;
    public Sprite[] cars;

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GetItem(i).ID = i;
            InitGUI(i);
            InitState(i);
        }
    }

    private void InitGUI(int indexItem)
    {
        GetItem(indexItem).Complete = complete;
        GetItem(indexItem).Tab = tab;
        GetItem(indexItem).Car = cars[indexItem];
    }

    private void InitState(int indexItem)
    {
        GetItem(indexItem).State = Data.locations[indexItem];
    }

    private LocationItem GetItem(int indexItem)
    {
        return transform.GetChild(indexItem).GetComponent<LocationItem>();
    }

	public void OnClicked(int id) {
        Debug.Log("input " + id);

        for (int i = 0; i < transform.childCount; i++)
        {
            if (GetItem(i).State == "ACTUAL") Data.locations[i] = "UNLOCKED";
        }
        Data.locations[id] = "ACTUAL";

        for (int i = 0; i < transform.childCount; i++) InitState(i);
    }
}
