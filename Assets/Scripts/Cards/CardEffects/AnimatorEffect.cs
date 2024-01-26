using UnityEngine;
using System.Collections;

[System.Serializable]
//[CreateAssetMenu(fileName = "New AnimatorEffect", menuName = "Cards/CardEffect/AnimatorEffect")]
public class AnimatorEffect : CardEffect
{
    public float delay;
    public AnimatorEffect() { Debug_ID = "animator"; }
    public override void Activate<T>(CardUser caller, Card card, T target)
    {
        Debug.Log("Animated whatever.");
        caller.StartCoroutine(WaitRoutine(card));
    }

    private IEnumerator WaitRoutine(Card card)
    {
        yield return new WaitForSeconds(delay);
        EndEffect(card);
    }
}