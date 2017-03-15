using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Item : MonoBehaviour {
	public SlotMachine slotMachine;
	public Line line;
	public int currentNumber;
	public int sortingNumber;
	public Sprite[] sprites;
	Dictionary<int,int> randomIcon = new Dictionary<int, int> ();

	bool randomOnce;
	// Use this for initialization
	void Start () {
		RandomIcon ();
	}

	void RandomIcon()
	{
		randomIcon.Clear ();
		if (line.name == "Left") 
		{
			randomIcon.Add (0, slotMachine.leftIcon1);
			randomIcon.Add (1, slotMachine.leftIcon2);
			randomIcon.Add (2, slotMachine.leftIcon3);
			randomIcon.Add (3, slotMachine.leftIcon4);
			randomIcon.Add (4, slotMachine.leftIcon5);
			randomIcon.Add (5, slotMachine.leftIcon6);
			randomIcon.Add (6, slotMachine.leftIcon7);
			randomIcon.Add (7, slotMachine.leftIcon8);
			randomIcon.Add (8, slotMachine.leftIcon9);
			randomIcon.Add (9, slotMachine.leftIcon10);
		}
		else if (line.name == "Middle") 
		{
			randomIcon.Add (0, slotMachine.middleIcon1);
			randomIcon.Add (1, slotMachine.middleIcon2);
			randomIcon.Add (2, slotMachine.middleIcon3);
			randomIcon.Add (3, slotMachine.middleIcon4);
			randomIcon.Add (4, slotMachine.middleIcon5);
			randomIcon.Add (5, slotMachine.middleIcon6);
			randomIcon.Add (6, slotMachine.middleIcon7);
			randomIcon.Add (7, slotMachine.middleIcon8);
			randomIcon.Add (8, slotMachine.middleIcon9);
			randomIcon.Add (9, slotMachine.middleIcon10);
		}
		else if (line.name == "Right") 
		{
			randomIcon.Add (0, slotMachine.rightIcon1);
			randomIcon.Add (1, slotMachine.rightIcon2);
			randomIcon.Add (2, slotMachine.rightIcon3);
			randomIcon.Add (3, slotMachine.rightIcon4);
			randomIcon.Add (4, slotMachine.rightIcon5);
			randomIcon.Add (5, slotMachine.rightIcon6);
			randomIcon.Add (6, slotMachine.rightIcon7);
			randomIcon.Add (7, slotMachine.rightIcon8);
			randomIcon.Add (8, slotMachine.rightIcon9);
			randomIcon.Add (9, slotMachine.rightIcon10);
		}
	}

	void Update()
	{
		if (!slotMachine.canSpin) 
		{
			if (!randomOnce) 
			{
				randomOnce = true;
				RandomIcon ();
			}
		} 
		else
		{
			randomOnce = false;
		}

		currentNumber =  (sortingNumber + line.number)%5;
		if (currentNumber != 4) 
		{
			//If not the highest then move smoothly
			//transform.position = Vector3.Lerp (transform.position, new Vector3 (transform.position.x, (currentNumber * 2) - 4, 0), 0.1f);
			transform.position = Vector3.MoveTowards (transform.position, new Vector3 (transform.position.x, (currentNumber * 2) - 4, 0), 25*Time.deltaTime);
		} 
		else 
		{
			//If the highest then directly move and change the sprite
			transform.position = new Vector3 (transform.position.x,4,0);
			int selected = WeightedRandomizer.From (randomIcon).TakeOne ();
			GetComponent<SpriteRenderer> ().sprite = sprites[selected];
		}

		if (currentNumber == 0) 
		{
			//If not the lowest turn off the sprite
			StartCoroutine(DelayAction(0.08f, () =>
			{
				GetComponent<SpriteRenderer> ().sprite = null;
			}));
		}
		if (line.stop) 
		{
			StartCoroutine(DelayAction(0.1f, () =>
				{
					if (currentNumber == 2) 
					{
						line.middle = GetComponent<SpriteRenderer> ().sprite.name;
					} 
					else if (currentNumber == 1) 
					{
						line.lower = GetComponent<SpriteRenderer> ().sprite.name;
					} 
					else if (currentNumber == 3) 
					{
						line.upper = GetComponent<SpriteRenderer> ().sprite.name;
					} 
				}));
		}

	}
	// delay method coroutine
	IEnumerator DelayAction(float dTime, System.Action callback)
	{
		yield return new WaitForSeconds(dTime);
		callback();
	}
}
