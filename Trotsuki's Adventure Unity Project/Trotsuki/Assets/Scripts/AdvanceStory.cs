using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AdvanceStory : MonoBehaviour
{
	private Criterium myProperties; // accuracy is actually inaccuracy
	private InputCheckUI inputCheckUI;
	private LoadDialogue loadDialogue;
	private Timer timer;
	private Text opponent;
	private Image speakerImage;
	public int id { get; private set; }
	public double timerDefault;
	public Sprite[] speakers;
	public IDictionary<string, Sprite> speakerDictionary;
	public AudioSource audioSource;
	public AudioClip[] audioClips;

	public void SetOvertime(bool isOvertime)
	{
		myProperties.isOvertime = isOvertime;
	}
	private void AdvanceDialogue(int id)
	{
		//if (id == 233)
		//{
		//	SceneManager.LoadScene("End");
		//}
		//if (id == 234)
		//{
		//	SceneManager.LoadScene("Stroke");
		//}
		if (id == 254 || id == 255)
		{
			if (audioSource.clip != audioClips[1])
			{
				audioSource.clip = audioClips[1]; // alarm sound
				audioSource.loop = false;
				audioSource.Play();
			}

		}
		else if (id >= 256 && id <= 308)
		{
			if (audioSource.clip != audioClips[2])
			{
				audioSource.clip = audioClips[2]; // showtime
				audioSource.loop = true;
				audioSource.Play();
			}

		}
		else
		{
			if (audioSource.clip != audioClips[0])
			{
				audioSource.clip = audioClips[0]; // default
				audioSource.loop = true;
				audioSource.Play();
			}
		}

		for (int i = 0; i < 4; i++)
		{
			inputCheckUI.choiceStrings[i] = loadDialogue.dialogueList[id].texts[i + 2]; // put the text from dialogueList into the choiceStrings in InputCheckUI
			inputCheckUI.displayChoiceStrings[i] = loadDialogue.dialogueList[id].texts[i + 2];
			inputCheckUI.choiceObjects[i].GetComponent<Text>().text = inputCheckUI.ColorizeChoice(i);
			opponent.text = loadDialogue.dialogueList[id].texts[1];
			speakerImage.sprite = speakerDictionary[loadDialogue.dialogueList[id].speaker];
		}
		this.id = id;
		if (loadDialogue.dialogueList[id].timeLimit == 0.0)
		{

			timer.StartTimer(timerDefault);
		}
		else
		{
			timer.StartTimer(loadDialogue.dialogueList[id].timeLimit);
		}
		SetOvertime(false);
		SetAccuracy(0);
		timer.SetTimerOn(false);
	}
	private bool CheckConditions(int choice) // if the condition is met
	{

		if (myProperties.accuracy <= loadDialogue.dialogueList[id].criteria[choice].accuracy)
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
	public void SetAccuracy(double accuracy) // wrong input percentage without decimals
	{
		myProperties.accuracy = (int)(accuracy * 100);
	}
	// Use this for initialization
	private void Start()
	{
		inputCheckUI = gameObject.GetComponent<InputCheckUI>();
		loadDialogue = gameObject.GetComponent<LoadDialogue>();
		timer = gameObject.GetComponent<Timer>();
		timer.advanceStory = this;
		opponent = GameObject.FindGameObjectWithTag("Opponent").GetComponent<Text>();
		speakerImage = GameObject.FindGameObjectWithTag("Speaker").GetComponent<Image>();
		speakerDictionary = new Dictionary<string, Sprite>()
		{
			{"Narrator",speakers[0]},
			{"Kerensuki",speakers[1]},
			{"Renin",speakers[2]},
			{"Sudarin",speakers[3]},
			{"Shadow Sudarin",speakers[4]},
			{"Stepnya",speakers[5]}
		};
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
