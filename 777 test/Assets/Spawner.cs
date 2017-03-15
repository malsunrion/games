using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
	public GameObject items;
	public GameObject turnOn;
	public int addClone;
	// Use this for initialization
	void Start () {
		StartCoroutine (Spawn ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator Spawn()
	{
		for (int i = 0; i < 20+addClone; i++) 
		{
			yield return new WaitForSeconds (0.05f);
			GameObject cloneItems = (GameObject)Instantiate (items, transform.position, Quaternion.identity);
			cloneItems.transform.localScale = new Vector3 (1.2f, 1.2f, 1.2f);
		}
		yield return new WaitForSeconds (0.1f);
		turnOn.SetActive (true);
	}
}
