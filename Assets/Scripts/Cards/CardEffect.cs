using System;
using UnityEngine;

[System.Serializable]
public class CardEffect
{
    public string Debug_ID = "base";
    
    public virtual void Activate(CardUser caller, Card card, Targetable target) 
    {
        EndEffect(card);
    }

    public virtual void Activate(CardUser caller, Card card, Vector3 target) 
    {
        EndEffect(card);
    }

    protected void EndEffect(Card card)
    {
        card.EffectFinished();
    }

    internal void Activate<T>(CardUser caller, Card card, T target)
    {
        throw new NotImplementedException();
    }
}