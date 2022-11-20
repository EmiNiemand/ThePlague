using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Caretaker", menuName = "NPCs/Caretaker")]
public class CaretakerScriptable : NPCScriptable
{
    // Item prefabs
    public List<GameObject> goodies;

    public List<GameObject> GetGoodies(int number) {
        List<GameObject> result = new List<GameObject>();
        if(number > goodies.Count) number = goodies.Count;

        for (int i = 0; i < number; i++)
        {
            GameObject resultToAdd;
            do {
                resultToAdd = goodies[Random.Range(0, goodies.Count)];
            } while(result.Contains(resultToAdd));

            result.Add(resultToAdd);
        }

        return result;
    }
}
