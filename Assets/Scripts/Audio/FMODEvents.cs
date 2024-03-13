using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    [field: Header("PLAYER")]
    [field: SerializeField] public EventReference PlayerFootsteps { get; private set; }
    

    [field: Header("CARDS")]
    [field: SerializeField] public EventReference DrawCard { get; private set; }
    [field: SerializeField] public EventReference CardOnHover { get; private set; }
    //============
    [field: SerializeField] public EventReference BurstAttack { get; private set; } // aka Magick Missile
    [field: SerializeField] public EventReference SpawnSkeletons { get; private set; } // aka Skeleton Summon
    [field: SerializeField] public EventReference Throw { get; private set; }
    [field: SerializeField] public EventReference AddPoison { get; private set; } // aka Venemous Bite

    [field: Header("ENEMIES")]
 
    public string test = "dummy";

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
