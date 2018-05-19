using UnityEngine;
using System.Xml.Linq;

public class ConfigManager : MonoBehaviour
{
    XElement root;

    public void Save()
    {
        root = new XElement("root");

        for (int i = 0; i < Data.items.Length; i++)
        {
            root.AddFirst(new XElement("Item_" + i.ToString(), Data.items[i]));
        }

        for (int i = 0; i < Data.locations.Length; i++)
        {
            root.AddFirst(new XElement("Location_" + i.ToString(), Data.locations[i]));
        }

        root.AddFirst(new XElement("score", Data.score));
        root.AddFirst(new XElement("lastscore", Data.lastScore));
        root.AddFirst(new XElement("topscore", Data.topScore));
        root.AddFirst(new XElement("coins", Data.coins));
        root.AddFirst(new XElement("music", Data.music));
        root.AddFirst(new XElement("sound", Data.sound));
        root.AddFirst(new XElement("gamestart", Data.gameStart));
        
        XDocument saveDoc = new XDocument(root);

        PlayerPrefs.SetString("Save", saveDoc.ToString());
    }

    public void Load()
    {
        XElement root = null;

        if (!PlayerPrefs.HasKey("Save"))
        {
            Debug.LogWarning("Save data not found!");
            Debug.Log("DefaultLoad...");
        }
        else
        {
            root = XDocument.Parse(PlayerPrefs.GetString("Save")).Element("root");
        }

        if (root == null)
        {
            Debug.LogWarning("load failed!");
            return;
        }

        InitSave(root);

        Debug.Log(root);
    }

    private void InitSave(XElement root)
    {
        ParseEl(root, Data.items, "Item");
        ParseEl(root, Data.locations, "Location");

        Data.score = int.Parse(root.Element("score").Value);
        Data.lastScore = int.Parse(root.Element("lastscore").Value);
        Data.topScore = int.Parse(root.Element("topscore").Value);
        Data.coins = int.Parse(root.Element("coins").Value);
        Data.gameStart = int.Parse(root.Element("gamestart").Value);
        Data.music = root.Element("music").Value;
        Data.sound = root.Element("sound").Value;
    }

    private void ParseEl(XElement root, string[] Els, string pref)
    {
        for (int i = 0; i < Els.Length; i++)
        {
            string id = i.ToString();

            foreach (XElement el in root.Elements())
            {
                string elName = el.Name.ToString();
                if (elName == pref + "_" + id && elName.EndsWith(id))
                {
                    Els[i] = root.Element(elName).Value;
                }
            }
        }
    }
}