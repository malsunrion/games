using UnityEngine;
using System.Collections;

public class Line : MonoBehaviour {
	public string upper;
	public string middle;
	public string lower;
	public int number;
	public bool stop;
	// Use this for initialization
	void Start () {
		//StartCoroutine (ChangeNumber());
	}
	
	// Update is called once per frame
	void Update () {
		if (number < 0) 
		{
			number = 4;
		}
	}
	public IEnumerator ChangeNumber()
	{
		/*
		for (int i = 0; i < Random.Range (15, 30); i++) 
		{
			yield return new WaitForSeconds (0.1f);
			number--;
		}*/
		while (true) 
		{
			yield return new WaitForSeconds (0.1f);
			number--;
			if (stop) 
			{
				yield break;
			}
		}
	}
}
