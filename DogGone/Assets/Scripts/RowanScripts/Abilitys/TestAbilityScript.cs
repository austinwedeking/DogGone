using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAbilityScript : BaseAbility
{
    public override void Cast()
    {
        Debug.Log($"The ability was cast, this ability does {damage} damage");
    }

    public override void Upgrade()
    {
        
    }
}
