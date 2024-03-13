using UnityEngine;
using FMODUnity;

[System.Serializable]
public class CardEffect
{
    public string Debug_ID = "base";
    
    public virtual void Activate(CardUser caller, Card card, Targetable target) 
    {
        throw new System.NotImplementedException();
    }

    public virtual void Activate(CardUser caller, Card card, Vector3 target) 
    {
        throw new System.NotImplementedException();
    }

    public virtual void Activate(CardUser caller, Card card) 
    {
        throw new System.NotImplementedException();
    }

    protected void EndEffect(Card card)
    {
        card.EffectFinished();
    }

    internal void Activate<T>(CardUser caller, Card card, T target)
    {
        throw new System.NotImplementedException();
    }
}