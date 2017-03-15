using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SlotMachine : MonoBehaviour {
	public float money=10000;
	public float bet;
	int betTemp;

	public int autoSpin;
	public GameObject startButton;
	bool canAutoSpin=true;
	bool stopAutoSpin;
	bool isAutoSpin;

	public Line line1;
	public Line line2;
	public Line line3;

	public Text moneyText;
	public Text SpinStop;
	public Text totalBet;
	public Text betPerLineText;
	public Text autoSpinText;
	public Text PrizeText;

	public bool canSpin=true;
	bool canStop=true;

	[Header("Icon Percentage Left")]
	public int leftIcon1;
	public int leftIcon2;
	public int leftIcon3;
	public int leftIcon4;
	public int leftIcon5;
	public int leftIcon6;
	public int leftIcon7;
	public int leftIcon8;
	public int leftIcon9;
	public int leftIcon10;

	[Header("Icon Percentage Middle")]
	public int middleIcon1;
	public int middleIcon2;
	public int middleIcon3;
	public int middleIcon4;
	public int middleIcon5;
	public int middleIcon6;
	public int middleIcon7;
	public int middleIcon8;
	public int middleIcon9;
	public int middleIcon10;

	[Header("Icon Percentage Right")]
	public int rightIcon1;
	public int rightIcon2;
	public int rightIcon3;
	public int rightIcon4;
	public int rightIcon5;
	public int rightIcon6;
	public int rightIcon7;
	public int rightIcon8;
	public int rightIcon9;
	public int rightIcon10;

	[Header("Prize Table")]
	public int prize1;
	public int prize2;
	public int prize3;
	public int prize4;
	public int prize5;
	public int prize6;
	public int prize7;
	public int prize8;
	public int prize9;
	public int prize10;
	float totalPrize;

	[Header("Lines")]
	public GameObject upperLine;
	public GameObject middleLine;
	public GameObject lowerLine;
	public GameObject line357;
	public GameObject line159;

	//For long use
	int totalPlay;
	public static string log;
	// Use this for initialization
	void Start () {

		//StartCoroutine (Spin ());
		//StartCoroutine (line1.ChangeNumber ());

	}
	
	// Update is called once per frame
	void Update () {
		//Start Button Auto Spin
		if (isAutoSpin) 
		{
			startButton.SetActive (false);
		} 
		else 
		{
			startButton.SetActive (true);
		}

		//UI TEXT
		moneyText.GetComponent<Text> ().text = "Money : " + money;
		betPerLineText.GetComponent<Text> ().text = "" + bet;
		autoSpinText.GetComponent<Text> ().text = "" + autoSpin;
		totalBet.GetComponent<Text>().text = ""+bet*5+" Total Bet";


		//Toggle SPIN and STOP
		if (!canSpin) 
		{
			if (canStop) 
			{
				SpinStop.text = "STOP";
			} 
			else 
			{
				SpinStop.text = "SPIN";
			}
		} 
		else 
		{
			SpinStop.text = "SPIN";
		}



		//Bet Per Line
		if (betTemp == 0) 
		{
			bet = 0.1f;
		}
		else if (betTemp == 1) 
		{
			bet = 0.2f;
		}
		else if (betTemp == 2) 
		{
			bet = 0.5f;
		}
		else if (betTemp == 3) 
		{
			bet = 1f;
		}
		else if (betTemp == 4) 
		{
			bet = 2f;
		}
	}
		

	public void Spin()
	{
		//reset total prize
		PrizeText.GetComponent<Text> ().text = "";
		totalPrize = 0;

		totalPlay++;
		Debug.Log (log);

		if (canSpin) 
		{
			//Multiply by 5 because 5 lines
			if (money > bet*5) 
			{
				money -= bet*5;
				StartCoroutine (StartSpin ());
			}
		} 
		else 
		{
			if (canStop) 
			{
				canStop = false;
				StartCoroutine(StopSpin());
			}
		}
	}

	public IEnumerator StartSpin()
	{
		canSpin = false;
		StartCoroutine (line1.ChangeNumber ());
		StartCoroutine (line2.ChangeNumber ());
		StartCoroutine (line3.ChangeNumber ());

		if (!line1.stop) 
		{
			yield return new WaitForSeconds (Random.Range(1.8f,2.5f));
			line1.stop = true;
		}
		if (!line2.stop) 
		{
			yield return new WaitForSeconds (Random.Range (1.8f, 2.5f));
			line2.stop = true;
		}
		if (!line3.stop) 
		{
			yield return new WaitForSeconds (Random.Range (1.8f, 2.5f));
			line3.stop = true;
		}

		yield return new WaitForSeconds (0.2f);
		line1.stop = false;
		line2.stop = false;
		line3.stop = false;
		CheckSlot ();

	}

	public IEnumerator StopSpin()
	{
		line1.stop = true;
		yield return new WaitForSeconds (0.5f);
		line2.stop = true;
		yield return new WaitForSeconds (0.5f);
		line3.stop = true;
		stopAutoSpin = true;
	}

	void CheckSlot()
	{
		canSpin = true;
		canStop = true;
		canAutoSpin = true;
		if ((line1.middle == line2.middle) && (line1.middle == line3.middle)) 
		{
			AddMoney (line1.middle,bet);
			StartCoroutine (MatchingLine (middleLine));
		}
		if ((line1.upper == line2.upper) && (line1.upper == line3.upper)) 
		{
			AddMoney (line1.upper,bet);
			StartCoroutine (MatchingLine (upperLine));
		}
		if ((line1.lower == line2.lower) && (line1.lower == line3.lower)) 
		{
			AddMoney (line1.lower,bet);
			StartCoroutine (MatchingLine (lowerLine));
		}
		if ((line1.lower == line2.middle) && (line1.lower == line3.upper)) 
		{
			AddMoney (line1.lower,bet);
			StartCoroutine (MatchingLine (line357));
		}
		if ((line1.upper == line2.middle) && (line1.upper == line3.lower)) 
		{
			AddMoney (line1.upper,bet);
			StartCoroutine (MatchingLine (line159));
		}

		//Add Log History
		if(totalPrize==0)
			log += "\n" + totalPlay+"-";
		else
			log += "\n" + totalPlay+"- "+totalPrize;
	}

	void AddMoney(string name,float bet)
	{
		
		if (name == "icons_0") 
		{
			totalPrize += prize1*bet;
		}
		else if (name == "icons_1") 
		{
			totalPrize += prize2*bet;
		}
		else if (name == "icons_2") 
		{
			totalPrize += prize3*bet;
		}
		else if (name == "icons_3") 
		{
			totalPrize += prize4*bet;
		}
		else if (name == "icons_4") 
		{
			totalPrize += prize5*bet;
		}
		else if (name == "icons_5") 
		{
			totalPrize += prize6*bet;
		}
		else if (name == "icons_6") 
		{
			totalPrize += prize7*bet;
		}
		else if (name == "icons_7") 
		{
			totalPrize += prize8*bet;
		}
		else if (name == "icons_8") 
		{
			totalPrize += prize9*bet;
		}
		else if (name == "red") 
		{
			totalPrize += prize10*bet;
		}

		money += totalPrize;
		PrizeText.GetComponent<Text> ().text = "YOU GOT " + totalPrize;
	}

	//Show the matching line
	IEnumerator MatchingLine(GameObject line)
	{
		canSpin = false;
		canStop = false;
		canAutoSpin = false;
		line.SetActive (true);
		yield return new WaitForSeconds (1f);
		line.SetActive (false);
		canSpin = true;
		canStop = true;
		canAutoSpin = true;
	}


	//Add and reduce bet
	public void AddBet()
	{
		if(betTemp<4)
			betTemp++;
	}

	public void ReduceBet()
	{
		if(betTemp>0)
			betTemp--;
	}


	//Add and reduce auto spin
	public void AddAutoSpin()
	{
		autoSpin += 10;
	}

	public void ReduceAutoSpin()
	{
		if (autoSpin > 9) 
		{
			autoSpin -= 10;
		} 
		else 
		{
			autoSpin = 0;
		}
	}

	public void StartAutoSpin()
	{
		stopAutoSpin = false;
		isAutoSpin = true;
		StartCoroutine(AutoSpin());
	}

	IEnumerator AutoSpin()
	{
		while (true) 
		{
			yield return new WaitUntil (() => canAutoSpin);
			yield return new WaitForSeconds (1f);

			if (stopAutoSpin) 
			{
				isAutoSpin = false;
				break;
			}
				
			
			if (autoSpin > 0) {
				canAutoSpin = false;
				Spin ();
				autoSpin--;
			} 
			else 
			{
				break;
				yield return new WaitForSeconds (1f);
			}

		}
	}
}
