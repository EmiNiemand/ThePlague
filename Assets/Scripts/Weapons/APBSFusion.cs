using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class APBSFusion : AttackPattern
{
    private Vector3 cursorPos;
    private Camera mainCam;

    [Range(0.0f, 1.0f)]
    [SerializeField] private float speed;

    private void Start()
    {
        Setup();

        mainCam = Camera.main;
    }

    public override IEnumerator StartPattern()
    {
        if (isOnCooldown) yield break;
        if (weaponInstance != null) yield break;

        StartCoroutine(Cooldown());

        weaponInstance = Instantiate(weaponPrefab, transform.position, Quaternion.identity);
        weaponInstance.GetComponent<WeaponHitDetect>().pattern = this;
        cursorPos = mainCam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        cursorPos.z = 0;

        yield return new WaitUntil(() => isOnPosition(cursorPos));
        yield return speenOnPosition();
        yield return new WaitUntil(() => isOnPosition(transform.position));
        Destroy(weaponInstance);
    }

    protected override IEnumerator Cooldown()
    {
        isOnCooldown = true;
        yield return new WaitUntil(() => weaponInstance == null);
        yield return new WaitForSeconds(cooldownDuration);
        isOnCooldown = false;
    }


    private bool isOnPosition(Vector3 destination)
    {
        weaponInstance.transform.position = Vector3.MoveTowards(weaponInstance.transform.position, destination, speed);
        weaponInstance.transform.Rotate(0, 0, 100.0f);

        return Vector3.Distance(weaponInstance.transform.position, destination) < 0.1f;
    }

    private IEnumerator speenOnPosition()
    {
        float timePassed = 0;

        while (timePassed < 2)
        {
            weaponInstance.transform.Rotate(0, 0, 100.0f);
            timePassed += Time.smoothDeltaTime;
            yield return null;
        }
    }
}
