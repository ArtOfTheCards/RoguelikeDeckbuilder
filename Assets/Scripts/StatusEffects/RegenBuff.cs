using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Status/Regen")]
public class RegenStatusFactory : StatusFactory<RegenStatusData, RegenStatusInstance> {}

[System.Serializable]
public class RegenStatusData : StatusData
{
    public int healthPerTick = 5;
    public float tickDelay = 0.5f;
    public float duration = 60f;
}

public class RegenStatusInstance : StatusInstance<RegenStatusData>
{
    Coroutine regenCoroutine = null;

    public override void Apply()
    {
        regenCoroutine = target.StartCoroutine(RegenCoroutine());
    }

    public IEnumerator RegenCoroutine()
    {
        float elapsed = 0;
        float tickTimer = 0;
        while (elapsed < data.duration)
        {
            if (elapsed == 0 || tickTimer > data.tickDelay)
            {
                target.ChangeHealth(data.healthPerTick);
                tickTimer = 0;
            }
            
            elapsed += Time.deltaTime;
            tickTimer += Time.deltaTime;
            yield return null;
        }

        End();
    }

    public override void End(bool prematurely = false)
    {
        if (regenCoroutine != null) target.StopCoroutine(regenCoroutine);
        base.End(prematurely);
    }
}