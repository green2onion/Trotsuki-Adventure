using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class InputCheck : MonoBehaviour
{
	public List<string> choiceStrings;
	private GameObject[] choiceObjects;
	private string[] choiceObjectsStrings;
	private int charPos;
	private string currentString;
	private string NormalizeString(string input)
	{
		string output = input.ToLower();
		output = Regex.Replace(output, @"\s", ""); // remove spaces
		return output;
	}

	private void CheckInput()
	{

		foreach (char inputChar in Input.inputString) // get input char
		{
			bool frameChecked = false;
			char inputCharLower = char.ToLower(inputChar);
			for (int i = 0; i < choiceObjectsStrings.Length; i++) // try every choice
			{
				string normalizedString = NormalizeString(choiceObjectsStrings[i]);
				if (inputCharLower == normalizedString[charPos]) // check if the input char == the char at charPos
				{
					frameChecked = true;
					print(normalizedString[charPos]);
					currentString = currentString + normalizedString[charPos];
					if (currentString == normalizedString)
					{
						InputSuccess(i);
					}
					else
					{
						charPos++;
					}
				}
				else if (!frameChecked)
				{
					WrongInput(inputCharLower);
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
		//print(Input.inputString);
	}
}
