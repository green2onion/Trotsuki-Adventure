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

	private TextAsset branchData;
	private string[] branches;
	public List<Branch> branchList;


	private void AddCriterias()
	{
		criteriaList = new List<Criterium>();
		criteriaData = Resources.Load<TextAsset>("Branching Criteria");
		criterias = criteriaData.text.Split(new char[] { '\n' });// split by row
		
		for (int i = 1; i < criterias.Length - 1; i++)
		{

			string[] row = criterias[i].Split(new char[] { ',' });
			Criterium criterium = new Criterium();
			int.TryParse(row[0], out criterium.id);
			int.TryParse(row[1], out criterium.choice);
			int.TryParse(row[2], out criterium.accuracy);
			bool.TryParse(row[3], out criterium.isOvertime);
			int.TryParse(row[4], out criterium.relationshipWithKerensuki);
			int.TryParse(row[5], out criterium.relationshipWithRenin);
			int.TryParse(row[6], out criterium.relationshipWithSudarin);
			criteriaList.Add(criterium); // add the criteria to our list
		}
		for (int i = 1; i < dialogueList.Count; i++)
		{
			for (int j = 0; j < 4; j++)
			{
				dialogueList[i].SetCriterium(criteriaList[(i-1) * 4 + j], j);
				//print(dialogueList.Count);
				//print(i * 4 + j);
			}
		}
	}
	private void AddDialogues()
	{
		dialogueList = new List<Dialogue>();
		dialogueData = Resources.Load<TextAsset>("story"); // load the csv
		dialogues = dialogueData.text.Split(new char[] { '\n' }); // split by row
		dialogueList.Add(new Dialogue());

		for (int i = 1; i < dialogues.Length - 1; i++) // for each row
		{
			string[] row = dialogues[i].Split(new char[] { ',' }); // split by column
			Dialogue dialogue = new Dialogue(); // Dialogue is a class that contains all the dialogue info
			int.TryParse(row[0], out dialogue.id); // copy the id from the column into the dialogue instance


			for (int j = 1; j < row.Length; j++)
			{
				dialogue.SetDialogueText(row[j], j); // copy the dialogueText from the column into the dialogue instance
			}

			dialogueList.Add(dialogue); // add the dialogue to our list, im bad
		}
	}
	private void AddBranches()
	{
		branchList = new List<Branch>();
		branchData = Resources.Load<TextAsset>("branches");
		branches = branchData.text.Split(new char[] { '\n' });// split by row
		branchList.Add(new Branch());
		for (int i = 1; i < branches.Length - 1; i++)
		{

			string[] row = branches[i].Split(new char[] { ',' });
			Branch branch = new Branch();
			int.TryParse(row[0], out branch.id);
			for (int j = 1; j < branch.nextLine.Length; j++)
			{
				int.TryParse(row[j], out branch.nextLine[j]);
			}
			branchList.Add(branch); // add the branch to our list
		}
	}
	private void Awake()
	{
		AddDialogues();
		AddCriterias();
		AddBranches();
	}

	// Update is called once per frame, stil bad
	private void Update()
	{

	}
}
