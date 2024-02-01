using UnityEngine;

[System.Serializable]
public class AutoDrawEffect : CardEffect
{
    [SerializeField, Tooltip("The number of cards we attempt to draw from our own hand.")]
    public int number;
    public AutoDrawEffect() { Debug_ID = "New AutoDraw Effect"; }


    
    public override void Activate(CardUser caller, Card card, Targetable target)
    {
        caller.DrawCards(number);
        EndEffect(card);
    }

    public override void Activate(CardUser caller, Card card, Vector3 target)
    {
        caller.DrawCards(number);
        EndEffect(card);
    }

    public override void Activate(CardUser caller, Card card)
    {
        caller.DrawCards(number);
        EndEffect(card);
    }
}