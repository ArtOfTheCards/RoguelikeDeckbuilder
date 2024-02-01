using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Status/Damage Over Time")]
public class DamageOverTimeStatusFactory : StatusFactory<DamageOverTimeStatusData, DamageOverTimeStatusInstance> {}

[System.Serializable]
public class DamageOverTimeStatusData : StatusData
{
    public int damagePerTick = 1;
    public float tickDelay = 1f;
    public float duration = 5f;
}

public class DamageOverTimeStatusInstance : StatusInstance<DamageOverTimeStatusData>
{
    Coroutine damageOverTimeCoroutine = null;
    private Damagable damagable = null;

    public override void Apply()
    {
        if (target.TryGetComponent<Damagable>(out damagable))
        {
            damageOverTimeCoroutine = target.StartCoroutine(DamageOverTimeCoroutine());
        }
        else
        {
            Debug.Log("DamageOverTimeBuff: The target is invincible and has no health!", target);
        }
    }

    public IEnumerator DamageOverTimeCoroutine()
    {
        float elapsed = 0;
        float tickTimer = 0;
        while (elapsed < data.duration)
        {
            if (elapsed == 0 || tickTimer > data.tickDelay)
            {
                damagable.damage(data.damagePerTick);
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
        if (damageOverTimeCoroutine != null) target.StopCoroutine(damageOverTimeCoroutine);
        base.End(prematurely);
    }
}