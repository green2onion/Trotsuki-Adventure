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
	public int[] renderIndexes; // in original string
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
		output = Regex.Replace(output, @"\s", "");// remove spaces
		output = output.ToLower();
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
				print(normalizedString);
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
						//print("choice" + i + renderIndexes[i]);


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
		print("kevin is actually bad!");
	}
	private void WrongInput(char input)
	{
		print("wrong input!" + input);
	}

	private string ColorizeChoice(int choice)
	{
		string output = displayChoiceStrings[choice];
		output = output.Replace("NEWLINE", "\n");
		print(output.Substring(renderIndexes[choice]).IndexOf("\n"));
		if (renderIndexes[choice] == output.Substring(renderIndexes[choice]).IndexOf("\n") + renderIndexes[choice])
		{
			print("hello???????");
			renderIndexes[choice]++;
		}
		if (output[renderIndexes[choice]] == ' ') //
		{
			renderIndexes[choice]++; // add the spaces in the renderIndex
		}
		output = output.Insert(renderIndexes[choice], "</color>");
		output = color + output;

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
