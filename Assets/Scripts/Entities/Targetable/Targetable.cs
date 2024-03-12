using UnityEngine;

public enum TargetAffiliation { NONE, Player, Enemy }

public class Targetable : MonoBehaviour 
{
    public TargetAffiliation affiliation;
}