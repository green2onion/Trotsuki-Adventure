using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputCheck : MonoBehaviour
{
	public List<string> choiceStrings;
	private GameObject[] choiceObjects;
	private string[] choiceObjectsStrings;
	private int charPos;
	private void checkInput()
	{
		foreach (char inputChar in Input.inputString) // get input char
		{
			for (int i = 0; i < choiceObjectsStrings.Length; i++) // try every choice
			{
				if (inputChar == choiceObjectsStrings[i][charPos]) // check if the input char == the char at charPos
				{
					charPos++;
				}
			}

		}
	}

	// Use this for initialization
	private void Start()
	{
		choiceObjects = new GameObject[4];
		charPos = 0;
		for (int i = 0; i < 4; i++)
		{
			choiceObjects[i] = gameObject.transform.GetChild(i).gameObject;
		}

	}

	// Update is called once per frame
	private void Update()
	{
		checkInput();
	}
}
