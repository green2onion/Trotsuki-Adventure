using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class InputCheckUI : MonoBehaviour
{
	public List<string> choiceStrings;
	public string[] displayChoiceStrings;
	private GameObject[] choiceObjects;
	//private string[] choiceStrings;
	private int[] charPos; // in normalized string
	public int[] renderIndexes; // in original string
	private string[] currentString;
	public string color;
	private bool isChoiceSelected;
	private int myChoice;
	private GameObject inputDisplay;

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
				if (inputCharLower == normalizedString[charPos[i]]) // check if the input char == the char at charPos
				{
					if (!isChoiceSelected)
					{
						myChoice = i;
						isChoiceSelected = true;
					}
				}
				if (i == myChoice)
				{
					if (inputCharLower == normalizedString[charPos[i]])
					{
						frameChecked = true; // avoid checking more than one time in the for loop
						currentString[i] = currentString[i] + normalizedString[charPos[i]];
						inputDisplay.GetComponent<DisplayInput>().DisplayText(choiceStrings[i].Substring(0, renderIndexes[i] + 1));
						if (currentString[i] == normalizedString)
						{
							InputSuccess(i);
						}
						else
						{
							charPos[i]++;
							renderIndexes[i]++;
						}
					}
					else if (!frameChecked)
					{
						WrongInput(inputCharLower);
					}
				}
				choiceObjects[i].GetComponent<Text>().text = ColorizeChoice(i);
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
			choiceObjects[i].GetComponent<Text>().text = ColorizeChoice(i);
		}
		isChoiceSelected = false;
		print("zuccess!");
	}
	private void WrongInput(char input)
	{
		print("wrong input!" + input);
	}

	private string ColorizeChoice(int choice)
	{
		string output = displayChoiceStrings[choice];
		output = output.Replace("NEWLINE", "\n");
		if (renderIndexes[choice] == output.Substring(renderIndexes[choice]).IndexOf("\n") + renderIndexes[choice])
		{
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
		for (int i = 0; i < 4; i++)
		{
			choiceObjects[i] = GameObject.FindGameObjectsWithTag("Choice")[i];
			//choiceStrings[i] = choiceStrings[i];
			renderIndexes[i] = 0;
			charPos[i] = 0;
			currentString[i] = "";
			displayChoiceStrings[i] = choiceStrings[i];
		}
		inputDisplay = GameObject.FindGameObjectWithTag("MyInput");
	}

	// Update is called once per frame
	private void Update()
	{
		CheckInput();
		//print(Input.inputString);
	}
}
