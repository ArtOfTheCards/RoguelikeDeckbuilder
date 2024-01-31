
using System.Runtime.CompilerServices;
using UnityEngine;

public class AttackCheck : MonoBehaviour
{

    [SerializeField]
    private GameObject target;

    [SerializeField] private NpcPathFinder _npc;

    void Awake()
    {
        _npc = GetComponentInParent<NpcPathFinder>();

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == target)
        {
            string name = target.gameObject.name;
            Debug.Log(name + " has entered attack range");
            _npc.SetAttackStatus(true);
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == target)
        {
            string name = target.gameObject.name;
            Debug.Log(name + " has left attaack range");
            _npc.SetAttackStatus(false);
        }

    }



}
