using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Highlight : MonoBehaviour
{
    private bool highlighted;
    
    [SerializeField]
    GameObject highlightObject;

    public void highlightTest()
    {
        highlightObject.SetActive(true);
    }
    public void dehighlightTest()
    {
        highlightObject.SetActive(false);
    }
}
