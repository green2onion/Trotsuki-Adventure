using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DisplayInput : MonoBehaviour
{
	private Text myText;
	private string myInput;
	
	public List<int> wrongInputsIndexes;
	int renderIndex;
	public void ReceiveText(char text)
	{
		myInput += text;
	}
	/*private string UpdateString()
	{
		string displayText = myInput;
		
		foreach (int place in wrongInputsIndexes)
		{
			print("wrong input at " + place);
			displayText.Insert(place, "hello");
		}
		print(displayText);
		return displayText;
	}*/
	public void InputSuccess()
	{
		myInput = "";
	}
	// Use this for initialization
	private void Start()
	{
		myText = gameObject.GetComponent<Text>();
		wrongInputsIndexes = new List<int>();
	}

	// Update is called once per frame
	private void Update()
	{
		myText.text = myInput;
	}
}
