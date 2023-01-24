using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCCollider : MonoBehaviour
{
	private NPCBase npc;
	// Start is called before the first frame update
	void Start()
	{
		npc = transform.parent.GetComponent<NPCBase>();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if(other.CompareTag("Player")) npc.OnBoundaryEntry();
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if(other.CompareTag("Player")) npc.OnBoundaryExit();
	}

	//TODO: need to do some kind of universal interaction system
	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			other.GetComponent<PlayerEvents>().OnPickupIndicator(true);
			if (other.GetComponent<PlayerEvents>().isUsePressed == true)
			{
				npc.OnInteract();
			}
		}
	}
}
