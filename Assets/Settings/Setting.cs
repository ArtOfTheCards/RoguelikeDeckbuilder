using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Setting", menuName = "Setting", order = 0)]
public class Setting : ScriptableObject {

    [Header("GamePlay")]

    public Dropdown Languge;

    [Header("Controls")]

    public Slider mouseSenitivity_X;

    public Slider mouseSenitivity_Y;


    public Toggle RebindMode;


    [Header("KeyBoard + Mosuse")]

    public KeyCode up;
    public KeyCode down;
    public KeyCode left;
    public KeyCode right;
    
    [Header("Audio")]

    public AudioSource MasterAudio;
    public AudioSource BackgroundAudio;

    public AudioSource[] SFX;




}

