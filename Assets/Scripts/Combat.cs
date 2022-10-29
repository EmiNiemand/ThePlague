using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    GameObject playerSprite;
    Camera mainCam;
    Vector3 cursorPos, lerpCursorPos;
    // Start is called before the first frame update
    void Start()
    {
        playerSprite = transform.GetChild(0).gameObject;

        mainCam = Camera.main;
        cursorPos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        lerpCursorPos = cursorPos;
    }


    // Update is called once per frame
    void Update()
    {
        //Debug.Log(lerpCursorPos);
        
        cursorPos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        lerpCursorPos = Vector3.Lerp(lerpCursorPos, cursorPos, 0.01f);

        playerSprite.transform.LookAt(GameObject.Find("Square").transform, new Vector3(0, 0, -1));
    }
}
