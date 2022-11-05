using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GamepadHaptics : MonoBehaviour
{

    public float attackRumbleIntensity;
    public float attackRumbleDuration;

    public IEnumerator Attack()
    {
        if(!gamepadConnected()) yield break;

		Gamepad.current.SetMotorSpeeds(attackRumbleIntensity, attackRumbleIntensity);
        yield return new WaitForSeconds(attackRumbleDuration);
		Gamepad.current.SetMotorSpeeds(0, 0);
    }


    bool gamepadConnected()
    {
        return (Gamepad.all.Count > 0);
    }
}
