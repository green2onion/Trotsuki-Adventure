using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue 
{
	public int id;
	public string dialogueText;
	public string[] choices;

	public void SetDialogueText(string dialogueText, int index) // normalize the dialogue, 0 = opponent, 1234 = choices
	{
		this.dialogueText = dialogueText.Replace("COMMA", ",");
	}

}
