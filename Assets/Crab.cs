using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using NaughtyAttributes;

public class Crab : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    public Card crabAttack;
    //[SerializeField]
    //private float tickLength = 1.5f; 
    public Damagable targetLock;
    void Start()
    {
        this.targetLock = GameObject.Find("NPC_Agent").GetComponentInChildren<Damagable>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
