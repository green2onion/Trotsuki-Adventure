﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
	public double timer;
	private double tempTimer;
	public Slider timerSlider;
	public AdvanceStory advanceStory;

	public void StartTimer(double timer)
	{
		print("StartTimer is triggered");
		this.timer = timer;
		tempTimer = this.timer;
		advanceStory.SetOvertime(false);
	}
	private void CountDown()
	{
		if (tempTimer >= 0)
		{
			tempTimer -= Time.deltaTime;
		}
		else
		{
			TimerFinish();
		}
		timerSlider.value = (float)(tempTimer / timer);
	}
	private void TimerFinish()
	{
		advanceStory.SetOvertime(true);
	}
	// Use this for initialization
	private void Start()
	{
		tempTimer = timer;
	}

	// Update is called once per frame
	private void Update()
	{
		CountDown();

	}
}
