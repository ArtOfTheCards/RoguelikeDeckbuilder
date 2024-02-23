using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneTransition : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other) {

        if(other.gameObject.CompareTag("Player")){
            SceneManager.LoadSceneAsync("Sandbag-Level");
        }
        
    }
}
