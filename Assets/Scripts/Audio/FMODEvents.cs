using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

// TODO:
/*
    add missing assets
    update Buses in FMOD, master is currently empty
    verify trigger locations
*/
[System.Serializable]
public class CardSFX
{
    [field: SerializeField] public EventReference Play { get; private set; }
    [field: SerializeField] public EventReference Throw { get; private set; }

}


public class FMODEvents : MonoBehaviour
{

    [field: Header("MUSIC")]
    [field: SerializeField] public EventReference BattleTheme { get; private set; }

    [field: Header("PLAYER")]
    [field: SerializeField] public EventReference PlayerFootsteps { get; private set; }
    

    [field: Header("CARDS")]
    [field: SerializeField] public EventReference DrawCard { get; private set; }
    [field: SerializeField] public EventReference CardOnHover { get; private set; }
    [field: SerializeField] public EventReference Throw { get; private set; }

    //============
    [field: SerializeField] public CardSFX BurstAttack { get; private set; } 
    [field: SerializeField] public CardSFX SpawnSkeletons { get; private set; }
    [field: SerializeField] public CardSFX AddPoison_0 { get; private set; }
    [field: SerializeField] public CardSFX AddPoison_1 { get; private set; }
    [field: SerializeField] public CardSFX AddVulnerable { get; private set; }
    [field: SerializeField] public CardSFX Ensnare { get; private set; }

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
