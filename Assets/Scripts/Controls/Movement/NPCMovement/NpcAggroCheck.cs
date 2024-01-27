using UnityEditor;
using UnityEngine;

public class NpcAggroCheck : MonoBehaviour
{

    private GameObject playerTarget { get; set;}
    private string tag;
    private GameObject[] enemyTargets;

    
    

    void Awake()
    {
        playerTarget = GameObject.FindGameObjectWithTag("Player");
        // tag = GetComponentInParent<GameObject>().tag;

        // enemyTargets = (tag == "Enemy")? GameObject.FindGameObjectsWithTag("Enemy"): GameObject.FindGameObjectsWithTag("Ally");
     
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == playerTarget){

        }

    }

    private void OnTriggerExit(Collider other)
    {

        
    }

}