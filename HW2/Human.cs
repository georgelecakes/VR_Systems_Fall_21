using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : Actor
{

    public Human(string name = "Human") : base(name, 10, 6, 15)
    {
    }

    public override int RollAttack()
    {
        return this.die.Next(0, this.dexterity);
    }

    public override int RollDamage()
    {
        return this.die.Next(0, this.strength);
    }
}
