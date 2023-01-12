using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCInterface : MonoBehaviour
{
	private TextMeshProUGUI nameText;
	private TextMeshProUGUI descriptionText;
	private TextMeshProUGUI interactionIndicator;
	
	public void Setup()
	{
		nameText = transform.Find("NameText").GetComponent<TextMeshProUGUI>();
		descriptionText = transform.Find("DescriptionText").GetComponent<TextMeshProUGUI>();
		interactionIndicator = transform.Find("InteractionText").GetComponent<TextMeshProUGUI>();
		SetInteractionIndicatorActive(false);
	}
	
	public void SetName(string newName) { nameText.text = newName; }
	public void SetDescription(string newDesc) { descriptionText.text = newDesc; }
	public void SetDescriptionActive(bool active) { descriptionText.enabled = active; }
	public void SetInteractionIndicatorActive(bool active) { interactionIndicator.enabled = active; }
	public void DisableInteraction(bool setActive = false)
	{
		descriptionText.gameObject.SetActive(false);
		interactionIndicator.gameObject.SetActive(false);
	}
}
