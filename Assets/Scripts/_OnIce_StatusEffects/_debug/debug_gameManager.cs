using UnityEngine;
using NaughtyAttributes;

public class debug_gameManager : MonoBehaviour
{
    public Creature target;
    [Expandable] public StatusFactory strengthBuff;
    [Expandable] public StatusFactory regenBuff;
    [Expandable] public StatusFactory godRegenBuff;
    [Expandable] public StatusFactory explodeDebuff;


    private void OnGUI()
    {
        // Make a background box
        GUI.Box(new Rect(10,10,100,240), "Debug Menu");
    
        if(GUI.Button(new Rect(20,40,80,20), "Strength"))
        {
            print("Strength");
            target.AddStatusEffect(strengthBuff);
        }
    
        if(GUI.Button(new Rect(20,70,80,20), "Regen")) 
        {
            print("Regen");
            target.AddStatusEffect(regenBuff);
        }

        if(GUI.Button(new Rect(20,100,80,20), "God Regen")) 
        {
            print("God Regen");
            target.AddStatusEffect(godRegenBuff);
        }

        if(GUI.Button(new Rect(20,130,80,20), "Explode")) 
        {
            print("Explode");
            target.AddStatusEffect(explodeDebuff);
        }

        if(GUI.Button(new Rect(20,160,80,20), "Remove All")) 
        {
            print("Remove All");
            target.RemoveAllStatuses();
        }

        if(GUI.Button(new Rect(20,190,80,20), "Remove Strength")) 
        {
            print("Remove Strength");
            target.RemoveStatusesOfType(strengthBuff);
        }

        if(GUI.Button(new Rect(20,220,80,20), "Remove Regen")) 
        {
            print("Remove Regen");
            target.RemoveStatusesOfType(regenBuff);
        }

        GUI.Box(new Rect(120,10,100,90), $"health: {target.health}\nattack: {target.attack}");
    }
}