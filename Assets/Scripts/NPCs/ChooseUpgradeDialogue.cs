using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseUpgradeDialogue : MonoBehaviour
{
    private List<GameObject> itemSlots = new List<GameObject>();
    private List<GameObject> priceSlots = new List<GameObject>();
    private LevelCaretakerNPC npc;
    // Start is called before the first frame update
    public void Setup(LevelCaretakerNPC listenerNpc)
    {
        npc = listenerNpc;
        for(int i=1;; i++) 
        {
            GameObject item;
            try { item = transform.Find("Item"+i).gameObject; }
            catch (System.NullReferenceException) { break; }

            itemSlots.Add(item);
        }
        // for(int i=1;; i++) 
        // {
        //     GameObject item;
        //     try { item = transform.Find("Price"+i).gameObject; }
        //     catch (System.NullReferenceException) { break; }

        //     priceSlots.Add(item);
        // }
    }

    // TODO: improve, temporary code
    public void AddItemsToSlots(string itemName, Sprite itemImage) 
    {
        itemSlots[0].SetActive(npc.offerFullHeal);
        itemSlots[2].SetActive(npc.offerPlayerUpgrade);
        
        Image itemSprite = itemSlots[1].transform.GetChild(1).GetComponent<Image>();
        itemSprite.sprite = itemImage;
        itemSprite.color = Color.white;

        itemSlots[0].GetComponent<Button>().onClick.AddListener(() => npc.OnItemChoose("FullHeal"));
        itemSlots[1].GetComponent<Button>().onClick.AddListener(() => npc.OnItemChoose(itemName));
        itemSlots[2].GetComponent<Button>().onClick.AddListener(() => npc.OnItemChoose("SpeedUpgrade"));
    }
}
