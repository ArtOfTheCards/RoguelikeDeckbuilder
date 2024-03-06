using UnityEngine;

[System.Serializable]
public class SpawnEffect : CardEffect
{
    public GameObject toSpawn;
    [Tooltip("The (min, max) horizontal range of randomness, applied as an offset from the target position.")]
    public Vector2 xVariance = new();
    [Tooltip("The (min, max) vertical range of randomness, applied as an offset from the target position.")]
    public Vector2 yVariance = new();
    public SpawnEffect() { Debug_ID = "spawn"; }



    public override void Activate(CardUser caller, Card card, Targetable target)
    {
        GameObject spawned = GameObject.Instantiate(toSpawn, target.transform.position+GetOffset(), Quaternion.identity);
        TryInitializeAllyBoss(caller, spawned);

        EndEffect(card);
    }

    public override void Activate(CardUser caller, Card card, Vector3 target)
    {
        GameObject spawned = GameObject.Instantiate(toSpawn, target+GetOffset(), Quaternion.identity);
        TryInitializeAllyBoss(caller, spawned);

        EndEffect(card);
    }

    private Vector3 GetOffset()
    {
        return new(Random.Range(xVariance.x, xVariance.y), Random.Range(yVariance.x, yVariance.y), 0);
    }

    private void TryInitializeAllyBoss(CardUser caller, GameObject spawned)
    {
        AllyPathFinder ally = spawned.GetComponentInChildren<AllyPathFinder>();
        if (ally != null)
        {
            Targetable boss = caller.GetComponentInChildren<Targetable>();
            if (boss != null) ally.InitializeBoss(boss);
        }
    }
}