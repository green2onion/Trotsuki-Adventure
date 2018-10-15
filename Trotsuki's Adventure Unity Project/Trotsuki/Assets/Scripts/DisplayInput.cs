using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DisplayInput : MonoBehaviour
{
	private Text myText;
	private string myInput;
	public void DisplayText(string text)
	{
		myText.text = text;
	}

	// Use this for initialization
	private void Start()
	{
		myText = gameObject.GetComponent<Text>();
	}

	// Update is called once per frame
	private void Update()
	{

	}
}
