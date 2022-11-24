using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseDialogue : MonoBehaviour
{
    private List<GameObject> itemSlots = new List<GameObject>();
    private CaretakerNPC npc;
    // Start is called before the first frame update
    public void Setup(CaretakerNPC listenerNpc)
    {
        npc = listenerNpc;
        for(int i=1;; i++) 
        {
            GameObject item;
            try { item = transform.Find("Item"+i).gameObject; }
            catch (System.NullReferenceException) { break; }

            itemSlots.Add(item);
        }
    }

    public void AddItemsToSlots(Dictionary<string, Sprite> inputItems) 
    {
        int i = 0;
        foreach(var pair in inputItems)
        {
            if(i >= itemSlots.Count) break;
            Image itemSprite = itemSlots[i].transform.GetChild(1).GetComponent<Image>();

            itemSprite.sprite = pair.Value;
            itemSprite.color = Color.white;
            itemSlots[i].GetComponent<Button>().onClick.AddListener(() => npc.OnItemChoose(pair.Key));

            i++;
        }
    }
}
