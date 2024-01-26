using UnityEngine;

[System.Serializable]
//[CreateAssetMenu(fileName = "New SpawnEffect", menuName = "Cards/CardEffect/SpawnEffect")]
public class SpawnEffect : CardEffect
{
    public GameObject toSpawn;
    public SpawnEffect() { Debug_ID = "spawn"; }
    public override void Activate<T>(T target)
    {
        Debug.Log("Spawned whatever.");
    }
}