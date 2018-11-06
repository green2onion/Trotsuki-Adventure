using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadDialogue : MonoBehaviour
{
	private TextAsset dialogueData;
	private string[] dialogues; // the entire csv file
	public List<Dialogue> dialogueList; // this stores all the dialogues
	private TextAsset criteriaData;
	public List<Criterium> criteriaList;
	private string[] criterias;
	private void AddCriterias()
	{
		criteriaList = new List<Criterium>();
		dialogueData = Resources.Load<TextAsset>("Branching Criteria");
		criterias = dialogueData.text.Split(new char[] { '\n' });// split by row

		for (int i = 1; i<criterias.Length-1;i++)
		{
			string[] row = criterias[i].Split(new char[] { ',' });
			Criterium criterium = new Criterium();
			int.TryParse(row[0], out criterium.id);
			int.TryParse(row[1], out criterium.choice);
		}
	}
	private void AddDialogues()
	{
		dialogueList = new List<Dialogue>();
		dialogueData = Resources.Load<TextAsset>("story"); // load the csv
		dialogues = dialogueData.text.Split(new char[] { '\n' }); // split by row


		for (int i = 1; i < dialogues.Length - 1; i++) // for each row
		{
			string[] row = dialogues[i].Split(new char[] { ',' }); // split by column
			Dialogue dialogue = new Dialogue(); // Dialogue is a class that contains all the dialogue info
			int.TryParse(row[0], out dialogue.id); // copy the id from the column into the dialogue instance


			for (int j = 1; j < row.Length; j++)
			{
				dialogue.SetDialogueText(row[j], j); // copy the dialogueText from the column into the dialogue instance
			}

			dialogueList.Add(dialogue); // add the dialogue to our list
		}
	}
	private void AddCriterias()
	{

	}
	private void Awake()
	{
		AddDialogues();
	}

	// Update is called once per frame
	private void Update()
	{

	}
}
