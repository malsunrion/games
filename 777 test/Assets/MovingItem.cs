using UnityEngine;
using System.Collections;

public class MovingItem : MonoBehaviour {
	public Sprite[] sprites;
	public float speed;
	// Use this for initialization
	void Start () {
		GetComponent<SpriteRenderer> ().sprite = sprites[Random.Range(0,sprites.Length)];
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Rigidbody2D>().velocity = -transform.up  * speed;
	}
}
