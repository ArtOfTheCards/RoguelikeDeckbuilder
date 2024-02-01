using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Status/Explode")]
public class ExplodeStatusFactory : StatusFactory<ExplodeStatusData, ExplodeStatusInstance> {}

[System.Serializable]
public class ExplodeStatusData : StatusData
{
    public float duration = 5f;
    public int healthChange = -5;
}

public class ExplodeStatusInstance : StatusInstance<ExplodeStatusData>
{
    private Coroutine explodeCoroutine = null;
    private Damagable damagable = null;

    public override void Apply()
    {
        if (target.TryGetComponent<Damagable>(out damagable))
        {
            explodeCoroutine = target.StartCoroutine(ExplodeCoroutine());
        }
        else
        {
            Debug.Log("ExplodeStatus: The target is invincible and has no health!", target);
        }
    }

    public IEnumerator ExplodeCoroutine()
    {
        float elapsed = 0;
        while (elapsed < data.duration)
        { 
            elapsed += Time.deltaTime;
            yield return null;
        }

        damagable.damage(data.healthChange);
        End();
    }

    public override void End(bool prematurely = false)
    {
        if (explodeCoroutine != null) target.StopCoroutine(explodeCoroutine);
        base.End(prematurely);
    }
}