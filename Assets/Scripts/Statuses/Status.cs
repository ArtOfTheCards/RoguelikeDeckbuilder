using UnityEngine;

// Status effect framework adapted from work by Edward Lu on Stray Pixels:
// https://straypixels.net/statuseffects-framework/

// SUMMARY ========================================================================================
// Status effects must have easily-editable parameters (duration, power, etc),
// but ALSO must be easily instanced at runtime (so that multiple enemies can
// be on fire at once, etc). Thus, each status effect is actually composed
// of 3 classes:
// 
// 1. A serializable 'Data' class containing an arbitrary set of parameters.
// 2. An instantiable 'Instance' class, which has access to the Data class,
//    and stores methods relevant to running the effect.
// 3. A 'Factory' class, which centralizes the process of creating an
//    Instance class from a Data class. THE FACTORY IS OUR MAIN "STATUS EFFECT" ASSET.
//
// Factories are created in the Unity Editor. Data can be edited on the Factory object.



// StatusData =======================================================================================
public abstract class StatusData
{
    // ================================================================
    // Abstract base class defining a StatusData, used to mention a
    // StatusData of unspecific type.
    // ================================================================

    public string ID = "";
    public override string ToString() { return ID; }
}



// StatusInstance ====================================================================================
[System.Serializable]
public abstract class StatusInstance 
{ 
    // ================================================================
    // Abstract base class defining a StatusInstance, used to mention a
    // StatusInstance of unspecific type.
    // ================================================================

    public System.Action<StatusInstance> statusEnded;
    public string ID { get {return ToString();} }

    public abstract void Apply();
    public virtual void Update() {}
    public virtual void End(bool prematurely=false) { if (!prematurely) statusEnded?.Invoke(this); }
}
public abstract class StatusInstance<StatusData_type> : StatusInstance
{
    // ================================================================
    // An abstract generic class defining a StatusInstance from some 
    // arbitrary StatusData.
    // 
    // StatusInstances are runtime objects 'applied' to a creature, which 
    // carry out an instance of a status effect on them.
    // ================================================================

    public StatusData_type data;
    public Effectable target;

    public override string ToString() { return data.ToString(); }
}



// StatusFactory ====================================================================================
public abstract class StatusFactory : ScriptableObject 
{
    // ================================================================
    // Abstract base class defining a StatusFactory, used to mention
    // a StatusFactory of unspecific type.
    // ================================================================
    public abstract StatusInstance CreateStatusInstance(Effectable target);
    public abstract bool Matches(StatusInstance instance);
    public string ID { get {return ToString();} }
}
public class StatusFactory<StatusData_type, StatusInstance_type> : StatusFactory 
where StatusInstance_type : StatusInstance<StatusData_type>, new()
{
    // ================================================================
    // A generic factory class used to create new StatusInstances from
    // arbitrary StatusData.
    //
    // StatusFactories contain StatusData objects, which hold data 
    // relevant to a Status's operation, editable in the inspector. 
    // They can return a StatusInstance, which are runtime objects that
    // can be 'applied' to Creatures.
    // ================================================================
    public StatusData_type StatusData;

    public override StatusInstance CreateStatusInstance(Effectable _target)
    {
        return new StatusInstance_type { data = StatusData, target = _target };
    }

    public override bool Matches(StatusInstance instance) 
    {
        // An instance matches this factory if it has the same instance type AND the same status data ID as us.
        return instance is StatusInstance_type && instance.ID == ID; 
    }
    public override string ToString() { return StatusData.ToString(); }
}