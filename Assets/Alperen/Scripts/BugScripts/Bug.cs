using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bug : MonoBehaviour
{
    public event System.Action OnDeath;

    [SerializeField] protected int startingHealth;
    protected bool dead;
    protected int health;

    protected virtual void Start()
    {
        health = startingHealth;
    }


    public virtual void TakeBite(int damage)
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
        else
        {
            print("ondeath null");
        }
    }

    public bool GetDeadSituation()
    {
        return dead;
    }
}
