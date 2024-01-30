using UnityEngine;
using System.Collections;

[System.Serializable]
public class DelayEffect : CardEffect
{
    [SerializeField, Tooltip("The amount of time, in seconds, we wait during this effect.")]
    public float delayAmount;
    public DelayEffect() { Debug_ID = "New Delay Effect"; }


    
    public override void Activate(CardUser caller, Card card, Targetable target)
    {
        caller.StartCoroutine(WaitRoutine(card));
    }

    public override void Activate(CardUser caller, Card card, Vector3 target)
    {
        caller.StartCoroutine(WaitRoutine(card));
    }

    private IEnumerator WaitRoutine(Card card)
    {
        yield return new WaitForSeconds(delayAmount);
        EndEffect(card);
    }
}