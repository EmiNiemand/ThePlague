using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCInterface : MonoBehaviour
{
	private TextMeshProUGUI nameText;
	private TextMeshProUGUI descriptionText;
	
	public void Setup()
	{
		nameText = transform.Find("NameText").GetComponent<TextMeshProUGUI>();
		descriptionText = transform.Find("DescriptionText").GetComponent<TextMeshProUGUI>();
	}
	
	public void SetName(string newName) { nameText.text = newName; }
	public void SetDescription(string newDesc) { descriptionText.text = newDesc; }
	public void SetDescriptionActive(bool active) { descriptionText.enabled = active; }
}
