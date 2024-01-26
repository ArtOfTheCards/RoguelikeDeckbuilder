using UnityEngine;

[System.Serializable]
public class CardEffect
{
    public string Debug_ID = "base";
    
    public virtual void Activate<T>(CardUser caller, T target) { Debug.Log("Did absolutely nothing."); }
}