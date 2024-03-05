using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Setting", menuName = "Setting", order = 0)]
public class Setting : ScriptableObject {

    [Header("GamePlay")]

    public int language;

    [Header("Camera")]

    public bool freeLook;

    public float freeLookSensitivity_X;

    public float freeLookSensitivity_Y;
   

    [Header("Controls")]

    public bool pathFindingEnabled;

    public KeyCode up;
    public KeyCode down;
    public KeyCode left;
    public KeyCode right;

    public KeyCode interact;
    
    [Header("Audio")]

    public float masterVolume = 0.5f;

    public float ambientVolume = 0.5f;

    public float sfxVolume = 0.5f;




}

