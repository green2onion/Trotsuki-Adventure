using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdvanceStory : MonoBehaviour
{
	public struct Criteria
	{
		public double accuracy; // every right type + 100, every wrong type - 100, player score/best possible score = accuracy
		public bool overtime; // bool overtime or not
		public int relationshipWithKerensuki; // from -100 to 100, starts from 0
		public int relationshipWithRenin; // from -100 to 100, starts from 0
		public int relationshipWithSudarin; // from -100 to 100, starts from 0
	}
	public Criteria[][] criterias;
	private Criteria myProperties;
	private InputCheckUI inputCheckUI;
	private LoadDialogue loadDialogue;
	private Text opponent;
	private int id;
	private void AdvanceDialogue(int id)
	{
		for (int i = 0; i < 4; i++)
		{
			inputCheckUI.choiceStrings[i] = loadDialogue.dialogueList[id].texts[i + 2]; // put the text from dialogueList into the choiceStrings in InputCheckUI
		}
		opponent.text = loadDialogue.dialogueList[id].texts[1];
		this.id = id;
	}
	private bool CheckConditions(int choice) // if the condition is successful
	{
		if (myProperties.accuracy >= criterias[id][choice].accuracy)
		{
			if (myProperties.overtime == criterias[id][choice].overtime)
			{
				if (myProperties.relationshipWithKerensuki >= criterias[id][choice].relationshipWithKerensuki)
				{
					if (myProperties.relationshipWithRenin >= criterias[id][choice].relationshipWithRenin)
					{
						if (myProperties.relationshipWithSudarin >= criterias[id][choice].relationshipWithSudarin)
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
			case 0:
				if (CheckConditions(choice))
				{
					AdvanceDialogue(1);
				}
				break;
			default:
				print("GoToDialogue is not working");
				break;
		}
	}
	// Use this for initialization
	private void Start()
	{
		inputCheckUI = gameObject.GetComponent<InputCheckUI>();
		loadDialogue = gameObject.GetComponent<LoadDialogue>();
		opponent = GameObject.FindGameObjectWithTag("Opponent").GetComponent<Text>();
		criterias = new Criteria[loadDialogue.dialogueList.Count][];
		for (int i = 0; i < criterias.Length; i++)
		{
			criterias[i] = new Criteria[4];
		}
		myProperties = new Criteria();
		id = 0;
		AdvanceDialogue(id);
	}

	// Update is called once per frame
	private void Update()
	{
		//advanceDialogue(0);
	}
}
