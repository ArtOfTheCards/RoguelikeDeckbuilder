using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorldspaceHealthbars : MonoBehaviour
{
    [SerializeField, Tooltip("The prefab for our healthbar objects.")]
    private GameObject healthbarPrefab = null;
    [SerializeField, Tooltip("The offset of our healthbars from our entities.")]
    private Vector2 offset = new();


    // Dict used to tie damagable to their GameObjects.
    private Dictionary<Damagable, GameObject> damagableToHealthbar = new();
    // Used to cache last positions and only update if we need to.
    private Dictionary<Damagable, Vector3> damagableToLastPosition = new();


    private void Update()
    {
        foreach (Damagable damagable in damagableToHealthbar.Keys.ToArray())
        {
            if (damagable.transform.position != damagableToLastPosition[damagable])
            {
                damagableToLastPosition[damagable] = damagable.transform.position;
                Vector3 newPosition = damagableToLastPosition[damagable] + (Vector3)offset;
                damagableToHealthbar[damagable].transform.position = newPosition;
            }
        }
    }

    public void CreateHealthbar(Damagable damagable)
    {
        if (damagableToHealthbar.ContainsKey(damagable))
        {
            Debug.Log("WorldspaceHealthbars Error: CreateHealthbar failed. Damagable was already present in dict.");
            return;
        }

        damagableToHealthbar[damagable] = Instantiate(healthbarPrefab, transform);
        damagableToLastPosition[damagable] = damagable.transform.position;

        damagableToHealthbar[damagable].GetComponent<HealthBar>().InitializeDamagable(damagable);
    }

    public void DeleteHealthbar(Damagable damagable)
    {
        Destroy(damagableToHealthbar[damagable]);
        damagableToHealthbar.Remove(damagable);
        damagableToLastPosition.Remove(damagable);
    }
}