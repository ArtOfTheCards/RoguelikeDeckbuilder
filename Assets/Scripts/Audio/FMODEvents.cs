using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

// TODO:
/*
    add missing assets
    update Buses in FMOD, master is currently empty
    link volume to settings
    verify trigger locations
*/

public class FMODEvents : MonoBehaviour
{
    [field: Header("MUSIC")]
    [field: SerializeField] public EventReference BattleTheme { get; private set; }

    [field: Header("PLAYER")]
    [field: SerializeField] public EventReference PlayerFootsteps { get; private set; }
    

    [field: Header("CARDS")]
    [field: SerializeField] public EventReference DrawCard { get; private set; }
    [field: SerializeField] public EventReference CardOnHover { get; private set; }
    //============
    [field: SerializeField] public EventReference BurstAttack { get; private set; } // aka Magick Missile
    [field: SerializeField] public EventReference SpawnSkeletons { get; private set; } // aka Skeleton Summon
    [field: SerializeField] public EventReference Throw { get; private set; } // aka Throw
    [field: SerializeField] public EventReference AddPoison { get; private set; } // aka Venemous Bite

    // waiting for asset
    // [field: SerializeField] public EventReference AddVulnerable { get; private set; } // aka Bash

    [field: Header("ENEMIES")]
 

    public static FMODEvents instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one FMODEvents in the scene");
        }
        instance = this;
    }
}
