using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvanceStory : MonoBehaviour
{
	private InputCheckUI inputCheckUI;
	private LoadDialogue loadDialogue;
	public int id;
	void advanceDialogue(int id)
	{
		foreach(string text in loadDialogue.dialogueList[id].texts)
		{
			print(text);
		}
		for (int i = 0; i < 4; i++)
		{
			inputCheckUI.choiceStrings[i] = loadDialogue.dialogueList[id].texts[i+2]; // put the text from dialogueList into the choiceStrings in InputCheckUI
		}
	}
	// Use this for initialization
	private void Start()
	{
		inputCheckUI = gameObject.GetComponent<InputCheckUI>();
		loadDialogue = gameObject.GetComponent<LoadDialogue>();
		id = 0;
	}

	// Update is called once per frame
	private void Update()
	{
		advanceDialogue(2);
	}
}
