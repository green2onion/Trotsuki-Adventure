using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue 
{
	public int id;
	public string[] texts = new string[6];

	public void SetDialogueText(string dialogueText, int index) // normalize the dialogue, 1 = opponent, 2345 = choices
	{
		texts[index] = dialogueText.Replace("COMMA", ",");
	}

}
