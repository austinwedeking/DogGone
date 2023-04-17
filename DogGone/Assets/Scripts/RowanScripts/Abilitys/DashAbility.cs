using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashAbility : BaseAbility
{
    public override void Cast()
    {
        Debug.Log("The player used the dash ability");
    }

    public override void Upgrade()
    {
        cooldownTimer -= 0.5f;
        Debug.Log(cooldownTimer);
    }
}
