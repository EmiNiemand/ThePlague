using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPCBase : MonoBehaviour
{
	protected NPCScriptable npcInfo;
	protected NPCInterface npcUI;
	protected GameObject chooseDialogue;

	// Start is called before the first frame update
	protected void Setup()
	{
		npcUI = transform.Find("NPCInterface").GetComponent<NPCInterface>();
		npcUI.Setup();
		npcUI.SetName(npcInfo.characterName);
		npcUI.SetDescription(npcInfo.description);
		npcUI.SetDescriptionActive(false);
		chooseDialogue = transform.Find("ChooseDialogue").gameObject;
		chooseDialogue.SetActive(false);
	}
	public virtual void OnBoundaryEntry() { npcUI.SetDescriptionActive(true); }
	public virtual void OnBoundaryExit() { npcUI.SetDescriptionActive(false); }
	public virtual void OnInteract() 
	{
		if(chooseDialogue) chooseDialogue.SetActive(true); 
	}

	public virtual void OnItemChoose() 
	{
		Destroy(chooseDialogue);
	}
}
