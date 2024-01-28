[System.Serializable]
public class CardEffect
{
    public string Debug_ID = "base";
    
    public virtual void Activate<T>(CardUser caller, Card card, T target) 
    {
        EndEffect(card);
    }

    protected void EndEffect(Card card)
    {
        card.EffectFinished();
    }
}