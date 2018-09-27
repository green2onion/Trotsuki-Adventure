using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputCheck : MonoBehaviour
{
	public List<string> choiceStrings;
	private GameObject[] choiceObjects;
	private string[] choiceObjectsStrings;
	private int charPos;
	private string currentString;
	private void CheckInput()
	{
		foreach (char inputChar in Input.inputString) // get input char
		{
			char inputCharLower = char.ToLower(inputChar);
			for (int i = 0; i < choiceObjectsStrings.Length; i++) // try every choice
			{
				if (inputCharLower == choiceObjectsStrings[i].ToLower()[charPos]) // check if the input char == the char at charPos
				{
					print(choiceObjectsStrings[i][charPos]);
					currentString = currentString + choiceObjectsStrings[i][charPos];
					if (currentString == choiceObjectsStrings[i])
					{
						InputSuccess(i);
					}
					else
					{
						charPos++;
					}
				}
				else
				{
					if (inputCharLower!='\0')
					{
						WrongInput(inputCharLower);
					}
					
				}
			}
		}
	}
	private void InputSuccess(int choice)
	{
		currentString = "";
		print("zuccess!");
	}
	private void WrongInput(char input)
	{
		print("wrong input!" + input);
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
		choiceObjectsStrings = new string[4];
		for (int i = 0; i < 4; i++)
		{
			choiceObjectsStrings[i] = choiceObjects[i].GetComponent<TextMesh>().text;
		}

	}

	// Update is called once per frame
	private void Update()
	{
		CheckInput();
		print(Input.inputString);
	}
}
