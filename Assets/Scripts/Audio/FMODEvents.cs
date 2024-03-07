using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    [field: Header("Draw SFX")]
    [field: SerializeField] public EventReference DrawCard { get; private set; }

    [field: Header("PlayerFootsteps SFX")]
    [field: SerializeField] public EventReference PlayerFootsteps { get; private set; }

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
