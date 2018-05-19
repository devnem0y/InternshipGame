using System.Xml.Linq;
using UnityEngine;

public class SaveItemStoreState : MonoBehaviour, ISaveObject
{
    private ConfigManager configM;
    //public string state;

    private void Awake()
    {
        configM = FindObjectOfType<ConfigManager>();
    }

    private void Start()
    {
        //configM.itemsStore.Add(this);
        Debug.Log("add");
    }

    private void OnDestroy()
    {
        //configM.itemsStore.Remove(this);
    }

    public XElement GetElement()
    {
        XElement element = new XElement(name, transform.GetComponent<Item>().State);
        return element;
    }
}
