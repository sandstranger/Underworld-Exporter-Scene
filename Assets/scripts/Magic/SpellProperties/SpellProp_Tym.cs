﻿using UnityEngine;
public class SpellProp_Tym : SpellProp
{

    public override void onHitPlayer()
    {
        //Freeze player for 15 seconds.
        UWCharacter.Instance.ParalyzeTimer = 4 + Random.Range(0, 16);
        //000~001~355~You feel your limbs stiffen.  You are unable to move. \n
        UWHUD.instance.MessageScroll.Add(StringController.instance.GetString(1, 355));
    }
}
