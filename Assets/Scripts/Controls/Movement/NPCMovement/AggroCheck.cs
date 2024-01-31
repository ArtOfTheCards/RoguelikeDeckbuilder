using UnityEditor;
using UnityEngine;

public class AggroCheck : MonoBehaviour
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
            Debug.Log(name + " has entered aggro range");
            _npc.SetAggroStatus(true);
        }

    }


    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == target)
        {
            string name = target.gameObject.name;
            Debug.Log(name + " has left aggro range");
            _npc.SetAggroStatus(false);
        }

    }

}