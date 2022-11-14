using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public struct RumbleSettings
{
    [Range(0.0f, 1.0f)]
    public float intensity;
    [Range(0.0f, 5.0f)]
    public float duration;
}

public class GamepadHaptics : MonoBehaviour
{
    public RumbleSettings attackSettings;

    public IEnumerator Attack()
    {
        if(!gamepadConnected()) yield break;

		Gamepad.current.SetMotorSpeeds(attackSettings.intensity, attackSettings.intensity);
        yield return new WaitForSeconds(attackSettings.duration);
		Gamepad.current.SetMotorSpeeds(0, 0);
    }


    bool gamepadConnected()
    {
        return (Gamepad.all.Count > 0);
    }
}
