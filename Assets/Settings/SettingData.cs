using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

[CreateAssetMenu(fileName = "Setting", menuName = "Setting", order = 0)]
public class SettingData : ScriptableObject {

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

    public void SetTo(SettingData settingData){
        this.language = settingData.language;
        this.freeLook = settingData.freeLook;
        this.freeLookSensitivity_X = settingData.freeLookSensitivity_X;
        this.freeLookSensitivity_Y = settingData.freeLookSensitivity_Y;
        this.pathFindingEnabled = settingData.pathFindingEnabled;
        this.up = settingData.up;
        this.down = settingData.down;
        this.left = settingData.left;
        this.right = settingData.right;
        this.interact = settingData.interact;
        this.masterVolume = settingData.masterVolume;
        this.ambientVolume = settingData.ambientVolume;
        this.sfxVolume = settingData.sfxVolume;

    }



}

