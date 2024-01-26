using UnityEngine;

[System.Serializable]
//[CreateAssetMenu(fileName = "New AnimatorEffect", menuName = "Cards/CardEffect/AnimatorEffect")]
public class AnimatorEffect : CardEffect
{
    public Vector3 test3;
    public AnimatorEffect() { Debug_ID = "animator"; }
    public override void Activate<T>(T target)
    {
        Debug.Log("Animated whatever.");
    }
}