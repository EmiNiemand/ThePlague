using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaretakerNPC : NPCBase
{
    private ChooseDialogue chooseDialogue;
    [SerializeField] protected CaretakerScriptable caretakerInfo;

    // Start is called before the first frame update
    void Start()
    {
        base.npcInfo = caretakerInfo;
        base.Setup();

		chooseDialogue = transform.Find("ChooseDialogue").GetComponent<ChooseDialogue>();

        chooseDialogue.Setup(this);
        chooseDialogue.AddItemsToSlots(GetItemsDict());
		chooseDialogue.gameObject.SetActive(false);
    }

	public override void OnInteract()
	{
		base.OnInteract();
        if(chooseDialogue) chooseDialogue.gameObject.SetActive(true);
	}

	public void OnItemChoose(string chosenItem) 
	{
		Destroy(chooseDialogue.gameObject);
        foreach(var goodie in caretakerInfo.goodies)
        {
            if(goodie.name == chosenItem)
            {
                FindObjectOfType<PlayerCombat>().EquipWeapon(goodie);
                break;
            }
        }
        gameObject.transform.Translate(2, 0, 0);
        npcUI.SetDescription("");
	}

    private Dictionary<string, Sprite> GetItemsDict()
    {
        Dictionary<string, Sprite> result = new Dictionary<string, Sprite>();
        foreach (GameObject item in caretakerInfo.GetGoodies(3))
        {
            result.Add(item.name, item.GetComponent<AttackPattern>().weaponIcon);
        }

        return result;
    }
}
