using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
    private AudioManager am;

	public GameObject locationContent;
    [Space(10)]
    public Sprite backgroundFrame;
    public Sprite complete;
    public Sprite padlock;
    public Sprite btnApply;
    public Sprite btnBuy;
    public int[] price;
    [Space(10)]
    public Sprite[] skins;
    public Sprite[] wheels;

    private void Awake()
    {
        am = FindObjectOfType<AudioManager>();
    }

    private void Start ()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GetItem(i).ID = i;
            GetItem(i).Price = price[i];
            GetItem(i).SetupPrice();
            InitGUI(i);
            InitState(i);
        }
	}

    private void InitGUI(int indexItem)
    {
        GetItem(indexItem).BackgroundFrame = backgroundFrame;
        GetItem(indexItem).Complete = complete;
        GetItem(indexItem).Padlock = padlock;
        GetItem(indexItem).BtnApply = btnApply;
        GetItem(indexItem).BtnBuy = btnBuy;

        GetItem(indexItem).Car = skins[indexItem];
        if (indexItem < 5)
        {
            GetItem(indexItem).WheelF = wheels[0];
            GetItem(indexItem).WheelB = wheels[0];
        }
        else if (indexItem > 4 && indexItem < 10)
        {
            GetItem(indexItem).WheelF = wheels[1];
            GetItem(indexItem).WheelB = wheels[1];
        }
        else if (indexItem > 9 && indexItem < 15)
        {
            GetItem(indexItem).WheelF = wheels[2];
            GetItem(indexItem).WheelB = wheels[2];
        }
        else if (indexItem > 14 && indexItem < 20)
        {
            GetItem(indexItem).WheelF = wheels[3];
            GetItem(indexItem).WheelB = wheels[3];
        }
        else if (indexItem > 19 && indexItem <= 24)
        {
            GetItem(indexItem).WheelF = wheels[3];
            GetItem(indexItem).WheelB = wheels[3];
        }
    }

	private void InitState(int indexItem)
    {
        if ((GetItem(indexItem).State != "OPEN" && GameParams.GetCoins() < GetItem(indexItem).Price) && GetItem(indexItem).State != "ACTUAL")
        {
            GetItem(indexItem).State = "CLOSED";
            if (GetItem(indexItem).KeyItem)
            {
                GetItem(indexItem).State = "CAN_BUY";
                GetItem(indexItem).transform.GetChild(3).GetComponent<Button>().interactable = false;
            }
        }
        else if ((GetItem(indexItem).State != "OPEN" && !GetItem(indexItem).Locked && GameParams.GetCoins() >= GetItem(indexItem).Price) && GetItem(indexItem).State != "ACTUAL")
        {
        GetItem(indexItem).State = "CAN_BUY";
        GetItem(indexItem).transform.GetChild(3).GetComponent<Button>().interactable = true;
        }
    }

    private Item GetItem(int indexItem)
    {
        return transform.GetChild(indexItem).GetComponent<Item>();
    }

	public void ClickedBuy(int itemID)
    {
		GameParams.SetCoins (GameParams.GetCoins() - GetItem(itemID).Price);
        for (int i = 0; i < transform.childCount; i++)
        {
            if (GetItem(i).State == "ACTUAL") GetItem(i).State = "OPEN";
        }
        GetItem(itemID).State = "ACTUAL";

		// init location
		if (GetItem(itemID).KeyItem)
        {
			for (int i = 0; i < locationContent.transform.childCount; i++)
            {
				if (i == transform.GetChild(itemID).GetComponent<KeyItem>().locationID)
                {
                    Data.locations[i] = "UNLOCKED";
				}
			}
		}

        if (GetItem(itemID).KeyItem) transform.GetChild(itemID).GetComponent<KeyItem>().OnLockItem();
        for (int i = 0; i < transform.childCount; i++) InitState (i);

        am.PlayClickPicupcoin();
	}

	public void ClickedApply(int itemID)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (GetItem(i).State == "ACTUAL") GetItem(i).State = "OPEN";
        }
        GetItem(itemID).State = "ACTUAL";
        for (int i = 0; i < transform.childCount; i++) InitState(i);

        am.PlayClickChange();
    }
}
