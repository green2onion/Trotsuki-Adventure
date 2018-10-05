using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class InputCheck : MonoBehaviour
{
	public List<string> choiceStrings;
	public string[] displayChoiceStrings;
	private GameObject[] choiceObjects;
	//private string[] choiceStrings;
	private int[] charPos; // in normalized string
	private int[] renderIndexes; // in original string
	private string[] currentString;
	public string color;


	private void ParseChoices()
	{

		for (int i = 0; i < choiceStrings.Count; i++)
		{
			for (int j = 0; j < choiceStrings[i].Length; j++)
			{
				if ((j >= 20) && (j % 20 == 0))
				{
					print("triggered");
					displayChoiceStrings[i].Insert(j, "INSERT");
				}
			}
		}
	}
	private string NormalizeString(string input)
	{
		string output = input;
		output = output.Replace("NEWLINE", "");// remove NEWLINE
		output = input.ToLower();
		output = Regex.Replace(output, @"\s", "");// remove spaces
		return output;
	}

	private void CheckInput()
	{
		foreach (char inputChar in Input.inputString) // get input charz
		{
			bool frameChecked = false;
			char inputCharLower = char.ToLower(inputChar);
			for (int i = 0; i < choiceStrings.Count; i++) // try every choice
			{
				string normalizedString = NormalizeString(choiceStrings[i]);
				//print("the inputCharLower is " + inputCharLower + " in the iteration of " + i);
				if (inputCharLower == normalizedString[charPos[i]]) // check if the input char == the char at charPos
				{
					frameChecked = true; // avoid checking more than one time in the for loop

					currentString[i] = currentString[i] + normalizedString[charPos[i]];

					if (currentString[i] == normalizedString)
					{
						InputSuccess(i);
						//break;
					}
					else
					{
						charPos[i]++;
						renderIndexes[i]++;
						print("choice" + i + renderIndexes[i]);
						if (choiceStrings[i][renderIndexes[i]] == ' ')
						{
							renderIndexes[i]++; // add the spaces in the renderIndex
						}
					}
				}
				else if (!frameChecked)
				{
					WrongInput(inputCharLower);
				}
				//print("the renderIndex is " + renderIndexes[i] + " in the iteration of " + i);
				choiceObjects[i].GetComponent<TextMesh>().text = ColorizeChoice(i);
			}


		}
	}
	private void InputSuccess(int choice)
	{

		for (int i = 0; i < 4; i++)
		{
			renderIndexes[i] = 0;
			charPos[i] = 0;
			currentString[i] = "";
			choiceObjects[choice].GetComponent<TextMesh>().text = ColorizeChoice(choice);
		}
		print("zuccess!");
	}
	private void WrongInput(char input)
	{
		print("wrong input!" + input);
	}

	private string ColorizeChoice(int choice)
	{
		string output = color + displayChoiceStrings[choice].Replace("NEWLINE", "\n").Insert(renderIndexes[choice], "</color>");
		return output;
	}
	// Use this for initialization
	private void Start()
	{
		choiceObjects = new GameObject[4];
		renderIndexes = new int[4];
		charPos = new int[4];
		currentString = new string[4];
		displayChoiceStrings = new string[4];
		//choiceStrings = new string[4];
		for (int i = 0; i < 4; i++)
		{
			choiceObjects[i] = gameObject.transform.GetChild(i).gameObject;
			//choiceStrings[i] = choiceStrings[i];
			renderIndexes[i] = 0;
			charPos[i] = 0;
			currentString[i] = "";
			displayChoiceStrings[i] = choiceStrings[i];
		}
		//ParseChoices();
	}

	// Update is called once per frame
	private void Update()
	{
		CheckInput();
		//print(Input.inputString);
	}
}
