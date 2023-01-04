using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Flamethrower : Traps
{
    private enum CollisionChangeType
    {
        Shrink,
        Expand
    }
    
    private ParticleSystem particles;
    private BoxCollider2D boxCollider;
    private Vector2 initBoxSize;
    private Vector2 initBoxOffset;
    private Light2D light;
    private float timeCounter;
    protected override void Start()
    {
        timeCounter = 0;
        particles = GetComponentInChildren<ParticleSystem>();
        boxCollider = GetComponent<BoxCollider2D>();
        initBoxSize = boxCollider.size;
        initBoxOffset = boxCollider.offset;
        light = GetComponentInChildren<Light2D>();
        base.Start();
    }
    
    void Update()
    {
        if (attackTime == 0) return;
        if (timeCounter >= attackTime)
        {
            StartCoroutine(OnCooldown());
            timeCounter = 0;
            return;
        }

        if (!isOnCooldown)
        {
            timeCounter += Time.deltaTime;
            CheckCollision();
        }
    }

    private IEnumerator OnCooldown()
    {
        isOnCooldown = true;
        particles.Stop();
        light.enabled = false;
        yield return StartCoroutine(ChangeCast(CollisionChangeType.Shrink));
        boxCollider.enabled = false;
        yield return new WaitForSeconds(cooldown);
        boxCollider.enabled = true;
        particles.Play();
        yield return StartCoroutine(ChangeCast(CollisionChangeType.Expand));
        light.enabled = true;
        isOnCooldown = false;
    }

    private void CheckCollision(float sizeMultiplier = 1, float offsetMultiplier = 1)
    {
        boxCollider.size = new Vector2(initBoxSize.x * sizeMultiplier, initBoxSize.y);
        boxCollider.offset = new Vector2(0.3f + (initBoxOffset.x - 0.3f) * offsetMultiplier, initBoxOffset.y);
    }

    private IEnumerator ChangeCast(CollisionChangeType type)
    {
        float timer = 1.0f;

        while (timer > 0)
        {
            if (timer <= 0) break;
            if(type == CollisionChangeType.Shrink) CheckCollision(timer, 2 - timer);
            else CheckCollision(1 - timer, 1 - timer);
            timer -= Time.smoothDeltaTime;
            yield return null;
        }
    }
    
    protected override IEnumerator OnEnter(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            col.gameObject.GetComponent<PlayerCombat>().OnReceiveDamage(damage);
        }
        
        else if(col.gameObject.CompareTag("Enemy"))
        {
            col.gameObject.GetComponent<Enemy>().OnReceiveDamage(damage);
        }

        yield break;
    }
}
