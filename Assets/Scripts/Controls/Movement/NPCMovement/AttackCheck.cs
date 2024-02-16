
using System.Runtime.CompilerServices;
using UnityEngine;

public class AttackCheck : MonoBehaviour
{

    private NpcPathFinder _npc;
    private string _npcTag;

    void Awake()
    {
        _npc = GetComponentInParent<NpcPathFinder>();
        _npcTag = _npc.gameObject.tag;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((_npcTag == "Ally" && other.CompareTag("Enemy")) ||
            (_npcTag == "Enemy" && (other.CompareTag("Player") || other.CompareTag("Ally"))))
        {
            string name = other.gameObject.name;
            Debug.Log(name + " has entered attack range");
            _npc.AddTarget(other.gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        _npc.UpdateTargets();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if ((_npcTag == "Ally" && other.CompareTag("Enemy")) ||
            (_npcTag == "Enemy" && (other.CompareTag("Player") || other.CompareTag("Ally"))))
        {
            string name = other.gameObject.name;
            Debug.Log(name + " has exited attack range");
            _npc.RemoveTarget(other.gameObject);
        }
    }

}
