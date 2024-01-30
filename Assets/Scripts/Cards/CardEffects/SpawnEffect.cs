using UnityEngine;

[System.Serializable]
//[CreateAssetMenu(fileName = "New SpawnEffect", menuName = "Cards/CardEffect/SpawnEffect")]
public class SpawnEffect : CardEffect
{
    public GameObject toSpawn;
    public SpawnEffect() { Debug_ID = "spawn"; }


    
    public override void Activate(CardUser caller, Card card, Targetable target)
    {
        GameObject.Instantiate(toSpawn, target.transform.position, Quaternion.identity);

        EndEffect(card);
    }

    public override void Activate(CardUser caller, Card card, Vector3 target)
    {
        GameObject.Instantiate(toSpawn, target, Quaternion.identity);

        EndEffect(card);
    }
}