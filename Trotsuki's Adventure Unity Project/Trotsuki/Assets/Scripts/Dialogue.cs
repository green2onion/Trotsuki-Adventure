public class Dialogue 
{
	public int id;
	public string[] texts = new string[6];
	public Criterium[] criteria = new Criterium[4];

	public void SetDialogueText(string dialogueText, int index) // normalize the dialogue, 1 = opponent, 2345 = choices
	{
		texts[index] = dialogueText.Replace("COMMA", ",");
	}
	public void SetCriterium(Criterium criteria, int index)
	{
		this.criteria[index] = criteria;
	}

}
