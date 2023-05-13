using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public Collider2D swordCollider;
    public float swordDamage = 1;
    Vector2 rightAttackOffset;

    // Start is called before the first frame update
    private void Start()
    {
        if(swordCollider == null)
        {
            Debug.LogWarning("Sword Collider not set");
        }
        rightAttackOffset = transform.localPosition;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        collider.SendMessage("OnHit", swordDamage);
    }

    public void AttackRight() 
    {
        // print("Attack Right"); testing
        swordCollider.enabled = true;
        transform.localPosition = rightAttackOffset;
    }

    public void AttackLeft() 
    {
        // print("Attack Left"); testing
        swordCollider.enabled = true;
        transform.localPosition = new Vector3(rightAttackOffset.x * -1, rightAttackOffset.y);
    }

    public void StopAttack() 
    {
        swordCollider.enabled = false;
    }
}
