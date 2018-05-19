using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    private int id;
    public int ID
    {
        get { return id; }
        set { id = value; }
    }
    [SerializeField]
    private bool locked;
    public bool Locked
    {
        get { return locked; }
        set { locked = value; }
    }
    private int price;
    public int Price
    {
        get { return price; }
        set { price = value; }
    }
    public string State
    {
        get { return Data.items[ID]; }
        set
        {
            Data.items[ID] = value;
        }
    }
    [SerializeField]
    private bool keyItem;
    public bool KeyItem
    {
        get { return keyItem; }
    }

    private GameObject car, wheelF, wheelB;
    public Gradient trail;
    public Sprite Car
    {
        get { return car.GetComponent<Image>().sprite; }
        set { car.GetComponent<Image>().sprite = value; }
    }
    public Sprite WheelF
    {
        get { return wheelF.GetComponent<Image>().sprite; }
        set { wheelF.GetComponent<Image>().sprite = value; }
    }
    public Sprite WheelB
    {
        get { return wheelB.GetComponent<Image>().sprite; }
        set { wheelB.GetComponent<Image>().sprite = value; }
    }
    public Gradient Trail
    {
        get { return trail; }
    }

    private GameObject complete, padlock, btnApply, btnBuy;
    public Sprite Complete
    {
        get { return complete.GetComponent<Image>().sprite; }
        set { complete.GetComponent<Image>().sprite = value; }
    }
    public Sprite Padlock
    {
        get { return padlock.GetComponent<Image>().sprite; }
        set { padlock.GetComponent<Image>().sprite = value; }
    }
    public Sprite BtnApply
    {
        get { return btnApply.GetComponent<Image>().sprite; }
        set { btnApply.GetComponent<Image>().sprite = value; }
    }
    public Sprite BtnBuy
    {
        get { return btnBuy.GetComponent<Image>().sprite; }
        set { btnBuy.GetComponent<Image>().sprite = value; }
    }

    public Sprite BackgroundFrame
    {
        get { return transform.GetComponent<Image>().sprite; }
        set { transform.GetComponent<Image>().sprite = value; }
    }

    private void Awake()
    {
        car = transform.GetChild(0).gameObject;
        wheelF = transform.GetChild(0).transform.GetChild(1).gameObject;
        wheelB = transform.GetChild(0).transform.GetChild(0).gameObject;

        complete = transform.GetChild(1).gameObject;
        padlock = transform.GetChild(2).gameObject;
        btnBuy = transform.GetChild(3).gameObject;
        btnApply = transform.GetChild(4).gameObject;
    }

    public void SetupPrice()
    {
        btnBuy.transform.GetChild(0).transform.GetComponent<Text>().text = "BUY\n$ " + price.ToString();
    }

    private void Update()
    {
		switch (Data.items[ID])
        {
		    case "OPEN":
			    complete.SetActive (false);
			    padlock.SetActive (false);
			    btnBuy.SetActive (false);
                btnApply.SetActive (true);
			break;
		    case "CLOSED":
                complete.SetActive (false);
                padlock.SetActive (true);
                btnBuy.SetActive (false);
                btnApply.SetActive(false);
			break;
		    case "CAN_BUY":
                complete.SetActive (false);
                padlock.SetActive (false);
                btnBuy.SetActive (true);
                btnApply.SetActive (false);
			break;
		    case "ACTUAL":
                complete.SetActive (true);
                padlock.SetActive (false);
                btnBuy.SetActive (false);
                btnApply.SetActive (false);
			break;
		}
	}
}
