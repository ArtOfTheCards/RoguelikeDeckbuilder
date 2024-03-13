using UnityEngine;
using System.Reflection;
using FMODUnity;

[System.Serializable]
public class CardEffect
{
    public string Debug_ID = "base";
    
    public virtual void Activate(CardUser caller, Card card, Targetable target) 
    {
        PlaySFXByID(card);
        throw new System.NotImplementedException();
    }

    public virtual void Activate(CardUser caller, Card card, Vector3 target) 
    {
        PlaySFXByID(card);
        throw new System.NotImplementedException();
    }

    public virtual void Activate(CardUser caller, Card card) 
    {
        PlaySFXByID(card);
        throw new System.NotImplementedException();
    }

    private void PlaySFXByID(Card card) {

        string propertyName = card.debug_ID;

        PropertyInfo propertyInfo = typeof(FMODEvents).GetProperty(propertyName);

        if (propertyInfo != null)
        {
            object eventRef = propertyInfo.GetValue(FMODEvents.instance);
            AudioManager.instance.PlayOneShot((EventReference)eventRef, new Vector3(0, 0, 0));
        }
        else
        {
            Debug.LogError("SFX Fail: Property '" + propertyName + "' not found in FMODEvents");
        }
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