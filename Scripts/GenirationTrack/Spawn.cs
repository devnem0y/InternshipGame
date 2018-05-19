using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public Camera myCamera;

	public List<GameObject> track = new List<GameObject> ();
	public List<GameObject> items = new List<GameObject> ();

    public void GenerationLevel()
    {
		track.Add(items[0]);
		track[0].transform.GetComponent<PartTrack> ().Setup (new Vector3 (-10f, -7.9f, -8f));
	}

	private void Update ()
    {
        transform.position = myCamera.transform.position;

        if (track.Count != 0)
        {
            if (track.Count < 10) AddPartTrack();
            RemovePartTrack();
        }
	}

	private void AddPartTrack()
    {
        Vector3 newPos = track[track.Count - 1].transform.GetChild(0).position;
        track.Add(items[Random.Range(0, items.Count)]);
        track[track.Count - 1].transform.GetComponent<PartTrack>().Setup(newPos);
	}

	private void AddPartTrack(int index)
    {
        Vector3 newPos = track[track.Count - 1].transform.GetChild(0).position;
        track.Add(items[index]);
        track[track.Count - 1].transform.GetComponent<PartTrack>().Setup(newPos);
    }

	private void RemovePartTrack()
    {
		if(transform.GetChild(0).GetComponent<TriggerRemoveEl>().IsRemoveEl()) track.RemoveAt (0);
		transform.GetChild (0).GetComponent<TriggerRemoveEl> ().SetRemoveEl (false);
	}
}
