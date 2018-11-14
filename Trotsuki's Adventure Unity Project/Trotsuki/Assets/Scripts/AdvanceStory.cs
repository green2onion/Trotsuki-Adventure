using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdvanceStory : MonoBehaviour
{
	/*
	public struct Criteria
	{
		public double accuracy; // every right type + 100, every wrong type - 100, player score/best possible score = accuracy
		public bool overtime; // bool overtime or not
		public int relationshipWithKerensuki; // from -100 to 100, starts from 0
		public int relationshipWithRenin; // from -100 to 100, starts from 0
		public int relationshipWithSudarin; // from -100 to 100, starts from 0
	}
	public Criteria[][] criterias;
	*/
	private Criterium myProperties;
	private InputCheckUI inputCheckUI;
	private LoadDialogue loadDialogue;
	private Text opponent;
	private int id;
	private void AdvanceDialogue(int id)
	{
		for (int i = 0; i < 4; i++)
		{
			inputCheckUI.choiceStrings[i] = loadDialogue.dialogueList[id].texts[i + 2]; // put the text from dialogueList into the choiceStrings in InputCheckUI
			inputCheckUI.displayChoiceStrings[i] = loadDialogue.dialogueList[id].texts[i + 2];

			inputCheckUI.choiceObjects[i].GetComponent<Text>().text = inputCheckUI.ColorizeChoice(i);

			opponent.text = loadDialogue.dialogueList[id].texts[1];

		}
		this.id = id;

	}
	private bool CheckConditions(int choice) // if the condition is met
	{

		if (myProperties.accuracy >= loadDialogue.dialogueList[id].criteria[choice].accuracy)
		{
			if (myProperties.isOvertime == loadDialogue.dialogueList[id].criteria[choice].isOvertime)
			{
				if (myProperties.relationshipWithKerensuki >= loadDialogue.dialogueList[id].criteria[choice].relationshipWithKerensuki)
				{
					if (myProperties.relationshipWithRenin >= loadDialogue.dialogueList[id].criteria[choice].relationshipWithRenin)
					{
						if (myProperties.relationshipWithSudarin >= loadDialogue.dialogueList[id].criteria[choice].relationshipWithSudarin)
						{
							return true;
						}
					}
				}
			}
		}

		return false;
	}

	public void GoToDialogue(int choice)
	{
		switch (id)
		{
			case 0: // dialogue id
				switch (choice)
				{
					case 1: // choice
						if (CheckConditions(choice))
						{
							AdvanceDialogue(1);
						}
						else
						{
							AdvanceDialogue(1);
						}
						break;
					case 2: // choice
						if (CheckConditions(choice))
						{
							AdvanceDialogue(1);
						}
						else
						{
							AdvanceDialogue(1);
						}
						break;
					case 3: // choice
						if (CheckConditions(choice))
						{
							AdvanceDialogue(1);
						}
						else
						{
							AdvanceDialogue(1);
						}
						break;
					case 4: // choice
						if (CheckConditions(choice))
						{
							AdvanceDialogue(1);
						}
						else
						{
							AdvanceDialogue(2);
						}
						break;
					default:
						print("GoToDialogue is not working");
						break;
				}
				break;

			default:
				print("GoToDialogue is not working");
				break;
		}
	}
	public void AddAndSetProperties(int id, int accuracy, int choice, bool isOvertime, int relationshipWithKerensuki, int relationshipWithRenin, int relationshipWithSudarin)
	{
		myProperties.id = id;
		myProperties.accuracy = accuracy;
		myProperties.choice = choice;
		myProperties.isOvertime = isOvertime;
		myProperties.relationshipWithKerensuki += relationshipWithKerensuki;
		myProperties.relationshipWithRenin += relationshipWithRenin;
		myProperties.relationshipWithSudarin += relationshipWithSudarin;
	}
	// Use this for initialization
	private void Start()
	{
		inputCheckUI = gameObject.GetComponent<InputCheckUI>();
		loadDialogue = gameObject.GetComponent<LoadDialogue>();
		opponent = GameObject.FindGameObjectWithTag("Opponent").GetComponent<Text>();
		//criterias = new Criteria[loadDialogue.dialogueList.Count][];

		id = 0;
		myProperties = new Criterium();
		myProperties.id = id;
		myProperties.accuracy = 0;
		myProperties.choice = 0;
		myProperties.isOvertime = false;
		myProperties.relationshipWithKerensuki = 0;
		myProperties.relationshipWithRenin = 50;
		myProperties.relationshipWithSudarin = 0;
		AdvanceDialogue(id);
	}

	// Update is called once per frame
	private void Update()
	{
		//advanceDialogue(0);
	}
}
