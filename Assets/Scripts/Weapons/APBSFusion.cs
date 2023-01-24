using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class APBSFusion : AttackPattern
{
    private Vector3 cursorPos;
    private Camera mainCam;

	[Range(0.0f, 20.0f)]
    [SerializeField] private int speed;
	private bool wallHitDetected;

    private void Start()
    {
		Setup();

        mainCam = Camera.main;
		wallHitDetected = false;
    }

    public override IEnumerator StartPattern()
    {
        if (isOnCooldown) yield break;
        if (weaponInstance != null) yield break;
        
        isOnCooldown = true;

        weaponInstance = Instantiate(weaponPrefab, transform.position, Quaternion.identity);
        weaponInstance.GetComponent<WeaponHitDetect>().pattern = this;

        StartCoroutine(Cooldown());

		weaponIndicator.targetObject = weaponInstance;

        cursorPos = mainCam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        cursorPos.z = 0;

        yield return new WaitUntil(() => isOnPosition(cursorPos) || wallHitDetected);
        yield return speenOnPosition();
        yield return new WaitUntil(() => isOnPosition(transform.position));
		wallHitDetected = false;

		weaponIndicator.targetObject = null;
        Destroy(weaponInstance);
    }

    protected override IEnumerator Cooldown()
    {
        yield return new WaitUntil(() => weaponInstance == null);
        yield return new WaitForSeconds(cooldownDuration - upgrades.attackSpeed);
        isOnCooldown = false;
    }


    private bool isOnPosition(Vector3 destination)
    {
        weaponInstance.transform.position = Vector3.MoveTowards(
            weaponInstance.transform.position, 
            destination, 
            (speed + upgrades.attackSpeed) * Time.deltaTime * 4);
        weaponInstance.transform.Rotate(0, 0, speed * Time.deltaTime * 400);

        return Vector3.Distance(weaponInstance.transform.position, destination) < 0.1f;
    }

    private IEnumerator speenOnPosition()
    {
        float timePassed = 0;

        while (timePassed < 2)
        {
            weaponInstance.transform.Rotate(0, 0, speed * Time.deltaTime * 400);
            timePassed += Time.smoothDeltaTime;
            yield return null;
        }
    }

    public override void WallHit()
	{
		wallHitDetected = true;
	}
}
