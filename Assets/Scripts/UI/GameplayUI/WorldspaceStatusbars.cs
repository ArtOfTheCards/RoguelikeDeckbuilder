using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorldspaceStatusbars : MonoBehaviour
{
    [SerializeField, Tooltip("The prefab for our status display objects.")]
    private GameObject statusbarPrefab = null;
    [SerializeField, Tooltip("The total offset of our Statusbars from our entities.")]
    private Vector2 totalOffset = new();


    // Dict used to tie effectable to their GameObjects.
    private Dictionary<Effectable, GameObject> effectableToStatusBar = new();
    // Used to cache last positions and only update if we need to.
    private Dictionary<Effectable, Vector3> effectableToLastPosition = new();


    private void Update()
    {
        foreach (Effectable effectable in effectableToStatusBar.Keys.ToArray())
        {
            if (effectable.transform.position != effectableToLastPosition[effectable])
            {
                effectableToLastPosition[effectable] = effectable.transform.position;
                Vector3 newPosition = effectableToLastPosition[effectable] + (Vector3)totalOffset;
                effectableToStatusBar[effectable].transform.position = newPosition;
            }
        }
    }

    public void CreateStatusbar(Effectable effectable)
    {
        if (effectableToStatusBar.ContainsKey(effectable))
        {
            Debug.Log("WorldspaceStatusbars Error: CreateStatusbar failed. effectable was already present in dict.");
            return;
        }

        effectableToStatusBar[effectable] = Instantiate(statusbarPrefab, transform);
        effectableToLastPosition[effectable] = effectable.transform.position;

        effectableToStatusBar[effectable].GetComponent<StatusBar>().Initialize(effectable);
    }

    public void DeleteStatusbar(Effectable effectable)
    {
        Destroy(effectableToStatusBar[effectable]);
        effectableToStatusBar.Remove(effectable);
        effectableToLastPosition.Remove(effectable);
    }
}
