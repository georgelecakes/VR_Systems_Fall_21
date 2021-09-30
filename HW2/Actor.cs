using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Actor
{
    public string name;
    public int[] position = {0,0};
    public float verticalOffset = 0.0f;

    public int strength;
    public int dexterity;

    public int health;
    public int maxHealth;

    public int initiative;

    protected System.Random die;

    public Actor(string name = "default",
                    int str = 10,
                    int dex = 10,
                    int maxHealth = 10)
    {
        this.name = name;
        this.strength = str;
        this.dexterity = dex;
        this.maxHealth = maxHealth;
        this.health = this.maxHealth;
        this.initiative = 10;
        die = new System.Random();
    }

    public virtual void RollInitiative()
    {
        this.initiative = die.Next(0, 20);
    }

    public virtual bool Evaded(int value)
    {
        if (value < this.dexterity)
            return true;

        return false;
    }

    public virtual void TakeDamage(int damage)
    {
        this.health -= damage;
    }

    public bool IsDead()
    {
        if (this.health <= 0)
            return true;

        return false;
    }

    public abstract int RollAttack();

    public abstract int RollDamage();
}
