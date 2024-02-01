using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Status/Regen")]
public class RegenStatusFactory : StatusFactory<RegenStatusData, RegenStatusInstance> {}

[System.Serializable]
public class RegenStatusData : StatusData
{
    public int healthPerTick = 1;
    public float tickDelay = 1f;
    public float duration = 5f;
}

public class RegenStatusInstance : StatusInstance<RegenStatusData>
{
    Coroutine regenCoroutine = null;
    private Damagable damagable = null;

    public override void Apply()
    {
        if (target.TryGetComponent<Damagable>(out damagable))
        {
            regenCoroutine = target.StartCoroutine(RegenCoroutine());
        }
        else
        {
            Debug.Log("RegenBuff: The target is invincible and has no health!", target);
        }
    }

    public IEnumerator RegenCoroutine()
    {
        float elapsed = 0;
        float tickTimer = 0;
        while (elapsed < data.duration)
        {
            if (elapsed == 0 || tickTimer > data.tickDelay)
            {
                damagable.heal(data.healthPerTick);
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