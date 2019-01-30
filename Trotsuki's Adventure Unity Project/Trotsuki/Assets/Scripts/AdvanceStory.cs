using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdvanceStory : MonoBehaviour
{
	private Criterium myProperties;
	private InputCheckUI inputCheckUI;
	private LoadDialogue loadDialogue;
	private Text opponent;
	private int id;
	private void AdvanceDialogue(int id)
	{
		print(loadDialogue.dialogueList[id]);
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
	private void SwitchOnChoice(int choice, int choice1True, int choice1False, int choice2True, int choice2False, int choice3True, int choice3False, int choice4True, int choice4False)
	{
		switch (choice)
		{
			case 0:
				if (CheckConditions(choice))
				{
					AdvanceDialogue(choice1True);
				}
				else
				{
					AdvanceDialogue(choice1False);
				}
				break;
			case 1:
				if (CheckConditions(choice))
				{
					AdvanceDialogue(choice2True);
				}
				else
				{
					AdvanceDialogue(choice2False);
				}
				break;
			case 2:
				if (CheckConditions(choice))
				{
					AdvanceDialogue(choice3True);
				}
				else
				{
					AdvanceDialogue(choice3False);
				}
				break;
			case 3:
				if (CheckConditions(choice))
				{
					AdvanceDialogue(choice4True);
				}
				else
				{
					AdvanceDialogue(choice4False);
				}
				break;
			default:
				print("SwitchOnChoice is not working");
				break;
		}
	}
	public void GoToDialogue(int choice)
	{
		SwitchOnChoice(choice, loadDialogue.branchList[id].nextLine[1], loadDialogue.branchList[id].nextLine[2], loadDialogue.branchList[id].nextLine[3], loadDialogue.branchList[id].nextLine[4], loadDialogue.branchList[id].nextLine[5], loadDialogue.branchList[id].nextLine[6], loadDialogue.branchList[id].nextLine[7], loadDialogue.branchList[id].nextLine[8]);
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

		id = 1;
		myProperties = new Criterium
		{
			id = id,
			accuracy = 0,
			choice = 0,
			isOvertime = false,
			relationshipWithKerensuki = 0,
			relationshipWithRenin = 50,
			relationshipWithSudarin = 0
		};
		AdvanceDialogue(id);
	}

	// Update is called once per frame
	private void Update()
	{
		//advanceDialogue(0);
	}
}
