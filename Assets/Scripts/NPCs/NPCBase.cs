using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class NPCBase : MonoBehaviour
{
	protected NPCScriptable npcInfo;
	protected NPCInterface npcUI;

	// Start is called before the first frame update
	protected void Setup()
	{
		npcUI = transform.Find("NPCInterface").GetComponent<NPCInterface>();
		npcUI.Setup();
		npcUI.SetName(npcInfo.characterName);
		npcUI.SetDescription(npcInfo.description);
		npcUI.SetDescriptionActive(false);
	}
	public virtual void OnBoundaryEntry() { npcUI.SetDescriptionActive(true); }
	public virtual void OnBoundaryExit() { npcUI.SetDescriptionActive(false); }

	public virtual void OnInteract()
	{
		GameObject.FindWithTag("Player").GetComponent<PlayerCombat>().SetCanAttack(false);
	}
	
	public virtual void OnInteractEnd()
	{
		GameObject.FindWithTag("Player").GetComponent<PlayerCombat>().SetCanAttack(true);
	}
}
