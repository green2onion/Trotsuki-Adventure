using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class InputCheckUI : MonoBehaviour
{
	public string[] choiceStrings;
	public string[] displayChoiceStrings;
	public GameObject[] choiceObjects;
	//private string[] choiceStrings;
	private int[] charPos; // in normalized string
	public int[] renderIndexes; // in original string
	private string[] currentString;
	public string color;
	private bool isChoiceSelected;
	private int myChoice;
	private GameObject inputDisplay;
	private int wrongInputs;
	private int choiceLength;

	private string NormalizeString(string input)
	{
		string output = input;
		//output = output.Replace("NEWLINE", "");// remove NEWLINE
		output = Regex.Replace(output, @"\r", "");// remove lines
		output = output.ToLower();
		return output;
	}

	private void CheckInput()
	{
		foreach (char inputChar in Input.inputString) // get input charz
		{
			bool frameChecked = false;
			char inputCharLower = char.ToLower(inputChar);
			for (int i = 0; i < choiceStrings.Length; i++) // try every choice
			{
				string normalizedString = NormalizeString(choiceStrings[i]);
				if (inputCharLower == normalizedString[charPos[i]]) // check if the input char == the char at charPos
				{
					if (!isChoiceSelected)
					{
						myChoice = i;
						choiceLength = displayChoiceStrings[myChoice].Length;
						isChoiceSelected = true;

					}
				}
				if (i == myChoice)
				{
					if (inputCharLower == normalizedString[charPos[i]])
					{
						frameChecked = true; // avoid checking more than one time in the for loop
						currentString[i] = currentString[i] + normalizedString[charPos[i]];
						inputDisplay.GetComponent<DisplayInput>().ReceiveText(displayChoiceStrings[i][renderIndexes[i]]);
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
					else if (!frameChecked && inputCharLower!='\b')
					{
						WrongInput(inputCharLower, renderIndexes[i]);
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
	
		inputDisplay.GetComponent<DisplayInput>().InputSuccess();
		gameObject.GetComponent<AdvanceStory>().SetAccuracy(GetAccuracy());
		gameObject.GetComponent<AdvanceStory>().GoToDialogue(choice);
		wrongInputs = 0;
		choiceLength = 0;
		for (int i = 0; i < 4; i++)
		{
			choiceObjects[i].GetComponent<Text>().text = ColorizeChoice(i);
		}
	}
	private void WrongInput(char input, int place)
	{
		inputDisplay.GetComponent<DisplayInput>().ReceiveText('*');
		wrongInputs++;
	}

	public string ColorizeChoice(int choice)
	{
		string output = displayChoiceStrings[choice];
		output = output.Replace("NEWLINE", "\n");
		if (renderIndexes[choice] == output.Substring(renderIndexes[choice]).IndexOf("\n") + renderIndexes[choice])
		{
			renderIndexes[choice]++;
		}
		output = output.Insert(renderIndexes[choice], "</color>");
		output = color + output;

		return output;
	}
	private string ShowInput(int choice)
	{
		string output = displayChoiceStrings[choice];
		output = output.Replace("NEWLINE", "\n");
		output = output.Substring(0, renderIndexes[choice]);
		return output;
	}

	public double GetAccuracy()
	{
		double accuracy;
		accuracy = wrongInputs / choiceLength;
		return accuracy;
	}

	// Use this for initialization
	private void Start()
	{
		choiceStrings = new string[4];

		choiceObjects = new GameObject[4];
		renderIndexes = new int[4];
		charPos = new int[4];
		currentString = new string[4];
		displayChoiceStrings = new string[4];
		wrongInputs = 0;
		choiceLength = 0;
		for (int i = 0; i < 4; i++)
		{
			choiceObjects[i] = GameObject.FindGameObjectsWithTag("Choice")[i];
			renderIndexes[i] = 0;
			charPos[i] = 0;
			currentString[i] = "";
			choiceStrings[i] = null;
			displayChoiceStrings[i] = choiceStrings[i];
		}
		inputDisplay = GameObject.FindGameObjectWithTag("MyInput");
		//accuracy = gameObject.GetComponent<Accuracy>();
	}

	// Update is called once per frame
	private void Update()
	{
		for (int i = 0; i < 4; i++)
		{
			displayChoiceStrings[i] = choiceStrings[i];
		}
		CheckInput();
	}
}
