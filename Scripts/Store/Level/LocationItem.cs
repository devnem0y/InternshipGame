using UnityEngine;
using UnityEngine.UI;

public class LocationItem : MonoBehaviour
{

	private int id;
    public int ID
    {
        get { return id; }
        set { id = value; }
    }
    public string State
    {
        get { return Data.locations[ID]; }
        set { Data.locations[ID] = value; }
    }

    private Button btn;
    private GameObject complete, car, tab, image;
    public Sprite Complete
    {
        get { return complete.GetComponent<Image>().sprite; }
        set { complete.GetComponent<Image>().sprite = value; }
    }
    public Sprite Car
    {
        get { return car.GetComponent<Image>().sprite; }
        set { car.GetComponent<Image>().sprite = value; }
    }
    public Sprite Tab
    {
        get { return tab.GetComponent<Image>().sprite; }
        set { tab.GetComponent<Image>().sprite = value; }
    }

    private void Awake()
    {
        btn = transform.GetChild(4).GetComponent<Button>();
        image = transform.GetChild(0).gameObject;
        complete = transform.GetChild(3).gameObject;
        car = transform.GetChild(2).gameObject;
        tab = transform.GetChild(1).gameObject;
    }

    private void Update()
    {
        switch (Data.locations[ID])
        {
            case "ACTUAL":
                btn.interactable = false;
                image.GetComponent<Image>().color = Color.grey;
                complete.SetActive(true);
                car.SetActive(false);
                tab.SetActive(false);
                break;
            case "LOCKED":
                btn.interactable = false;
                image.GetComponent<Image>().color = Color.grey;
                complete.SetActive(false);
                car.SetActive(true);
                tab.SetActive(true);
                break;
            case "UNLOCKED":
                btn.interactable = true;
                image.GetComponent<Image>().color = Color.white;
                complete.SetActive(false);
                car.SetActive(false);
                tab.SetActive(false);
                break;
        }
    }
}
