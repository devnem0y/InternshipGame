using UnityEngine;

public class Background : MonoBehaviour
{
    public Camera myCamera;
    
    public void Move()
    {
        transform.position = myCamera.transform.position;
    }

    public void InitBG()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (Data.locations[i] == "ACTUAL") transform.GetChild(i).gameObject.SetActive(true);
            else transform.GetChild(i).gameObject.SetActive(false);
        }
    }	
}
