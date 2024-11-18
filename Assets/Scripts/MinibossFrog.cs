using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinibossFrog : Frogs
{
    // Override the boost type to give a more powerful boost for the mini-boss
    public override BoostType GetBoostType()
    {
        return BoostType.ExtraHealth; // Mini-boss frogs give extra boost
    }
}
