using UnityEngine;
using System.Collections;

[CreateAssetMenu(menuName = "Status/Explode")]
public class ExplodeDebuffFactory : StatusFactory<ExplodeDebuffData, ExplodeDebuffInstance> {}

[System.Serializable]
public class ExplodeDebuffData : StatusData
{
    public float duration = 60f;
    public int healthChange = -50;
}

public class ExplodeDebuffInstance : StatusInstance<ExplodeDebuffData>
{
    private Coroutine explodeCoroutine = null;

    public override void Apply()
    {
        explodeCoroutine = target.StartCoroutine(ExplodeCoroutine());
    }

    public IEnumerator ExplodeCoroutine()
    {
        float elapsed = 0;
        while (elapsed < data.duration)
        { 
            elapsed += Time.deltaTime;
            yield return null;
        }

        target.ChangeHealth(data.healthChange);
        End();
    }

    public override void End(bool prematurely = false)
    {
        if (explodeCoroutine != null) target.StopCoroutine(explodeCoroutine);
        base.End(prematurely);
    }
}