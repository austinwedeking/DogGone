using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlamAI : BaseAI
{
    private Rigidbody2D AIRigid;
    private Transform playerTransform;

    //Still need something for when it jumps
    //Need something for it to spawn prefabs that have own scripts
    //Need to add system to stop turning midair
    //Ground detection system

    public override void Attack() //Runs a co-routine to see how long the attack last for and then rechecks state
    {
        throw new System.NotImplementedException();
        //Might need to rework as the attack state ends when it hits the ground again so it would make less sense to have a timer for it
        //Dumb big brain solution of making the timer negative and count down roll over to do it so it lets me but set to 0 when lands, this would allow it to have a midair jump on a refresh timer of 2.268321 years so overall negligable impact
    }

    public override void Chase() //Runs a co-routine to see how long to chase for and if it can
    {
        throw new System.NotImplementedException();
        //So if its close enough to the player then it will go towards then
    }

    public override void ChaseWithinRange() //Unsure if needed still
    {
        throw new System.NotImplementedException();
    }
    public override void Wander() //Unused but still needed for later
    {
        throw new System.NotImplementedException();
        //Make it randomly (say 1/20) move left or right a bit and ocasionally jump slightly while doing it too
    }

    public override void FaceLeft(bool faceRight) //Something something animations
    {
        throw new System.NotImplementedException(); //Eventually we will force feed it a boolean to make it look where it's supposed to be looking
        //Likely update what way its facing when it re lands on ground and when it jumps
    }

    public override IEnumerator Die()
    {
        throw new System.NotImplementedException();
    }
}
