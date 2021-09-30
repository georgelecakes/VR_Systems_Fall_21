using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raccoon : Actor
{

    public Raccoon(string name = "Raccoon") : base(name, 7, 16, 7)
    {
    }

    public override int RollAttack()
    {
        return this.die.Next(0, this.dexterity);
    }

    public override int RollDamage()
    {
        return this.die.Next(0, this.dexterity);
    }

}
