using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class LevelCaretakerNPC : NPCBase
{
    public bool offerFullHeal;
    public bool offerPlayerUpgrade;
    protected ChooseUpgradeDialogue chooseDialogue;
    [SerializeField] protected CaretakerScriptable caretakerInfo;

    // Start is called before the first frame update
    void Start()
    {
        base.npcInfo = caretakerInfo;
        base.Setup();

		chooseDialogue = transform.Find("ChooseDialogue").GetComponent<ChooseUpgradeDialogue>();

        chooseDialogue.Setup(this);
        var chosenItem = GetItemsDict().First();
        chooseDialogue.AddItemsToSlots(chosenItem.Key, chosenItem.Value);
		chooseDialogue.gameObject.SetActive(false);
    }

	public override void OnInteract()
	{
		base.OnInteract();
        if(chooseDialogue) chooseDialogue.gameObject.SetActive(true);
	}

	public void OnItemChoose(string chosenItem) 
	{
        var playerEvents = FindObjectOfType<PlayerEvents>();
        if(chosenItem.Equals("FullHeal")){
            if(!playerEvents.OnFullHeal(100))
                return;}
        else if(chosenItem.Equals("SpeedUpgrade")){
            if(!playerEvents.OnPlayerSpeedUpgrade(5, 100))
                return;}
        else
        {
            foreach(var goodie in caretakerInfo.goodies)
            {
                if(goodie.name == chosenItem)
                {
                    playerEvents.OnEquipWeapon(goodie);
                    break;
                }
            }
        }
		Destroy(chooseDialogue.gameObject);
        gameObject.transform.Translate(2, 0, 0);
        npcUI.DisableInteraction();
        base.OnInteractEnd();
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
