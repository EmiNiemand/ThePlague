using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaretakerNPC : NPCBase
{
    [SerializeField] protected CaretakerScriptable caretakerInfo;

    // Start is called before the first frame update
    void Start()
    {
        base.npcInfo = caretakerInfo;
        base.Setup();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
