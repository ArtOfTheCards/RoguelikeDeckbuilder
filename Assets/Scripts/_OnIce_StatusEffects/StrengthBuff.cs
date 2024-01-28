using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Status/Strength")]
public class StrengthStatusFactory: StatusFactory<StrengthStatusData, StrengthStatusInstance> { }

[System.Serializable]
public class StrengthStatusData : StatusData
{
    public int strengthToAdd = 5;
    public float duration = 60f;
}

public class StrengthStatusInstance : StatusInstance<StrengthStatusData>
{
    Coroutine unapplicationCoroutine = null;

    public override void Apply()
    {
        // target.ChangeAttack(data.strengthToAdd);
        // unapplicationCoroutine = target.StartCoroutine(UnapplicationCoroutine());
    }

    public IEnumerator UnapplicationCoroutine()
    {
        yield return new WaitForSeconds(data.duration);

        End();
    }

    public override void End(bool prematurely = false)
    {
        // if (unapplicationCoroutine != null) target.StopCoroutine(unapplicationCoroutine);
        // target.ChangeAttack(-data.strengthToAdd);
        base.End(prematurely);
    }
}