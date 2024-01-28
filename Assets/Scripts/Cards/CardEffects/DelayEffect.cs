using UnityEngine;
using System.Collections;

[System.Serializable]
public class DelayEffect : CardEffect
{
    public float delay;
    public DelayEffect() { Debug_ID = "New Delay Effect"; }
    public override void Activate<T>(CardUser caller, Card card, T target)
    {
        caller.StartCoroutine(WaitRoutine(card));
    }

    private IEnumerator WaitRoutine(Card card)
    {
        yield return new WaitForSeconds(delay);
        EndEffect(card);
    }
}