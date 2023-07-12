using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bug : MonoBehaviour
{
    public event System.Action OnDeath;

    [SerializeField] protected float startingHealth;
    protected bool dead;
    protected float health;

    protected virtual void Start()
    {
        health = startingHealth;
    }


    public virtual void TakeBite(float damage)
    {
        health -= damage;

        if (health <= 0 && !dead)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        health = 0;
        dead = true;

        if (OnDeath != null)
        {
            OnDeath();
        }
    }

    public bool GetDeadSituation()
    {
        return dead;
    }
}
