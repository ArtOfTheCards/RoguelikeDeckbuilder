using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public enum ModifierStackType {Flat_Linear, Percent_Linear, Percent_Exponential};



[System.Serializable]
public class StatModifier
{
    // Helper class representing a single modifier, with a type and value.
    // ================

    [Tooltip("The way this modifier stacks.\n\n"
           + "Flat Linear: Each additional stack adds a flat value to the base.\n"
           + "Percent Linear: Each additional stack adds a constant percentage of the base value TO the base.\n"
           + "Percent Exponential: Each additional stack multiplies the base value by some percentage.\n")]
    public ModifierStackType stackType;
    [Tooltip("The value of this modifier. If the stackType is flat, this is a flat value. " 
           + "If the stackType is percent, this is a percentage out of 100.")]
    public float value;
    [Tooltip("The priority of this modifier. Lower modifiers are applied first.")]
    public int priority = 0;

    public StatModifier()
    {
        stackType = ModifierStackType.Flat_Linear;
        value = 0;
    }

    public StatModifier(ModifierStackType _type, float _value)
    {
        stackType = _type;
        value = _value;
    }

    public override string ToString() { return $"{stackType}, {value}"; }
}



public class StatModifierBank
{
    // A list of modifiers.
    private List<StatModifier> modifiers = new();
    // Used to store stacks.
    private Dictionary<StatModifier, int> stacks = new();



    public bool ModifierPresent(StatModifier pair)
    {
        return modifiers.Contains(pair);
    }

    public void AddModifier(StatModifier pair, int currentStacks)
    {
        // Adds a modifier pair to our modifiers queue.
        // ================

        if (modifiers.Contains(pair)) return;    // For now, modifiers cannot contain clones.
        modifiers.Add(pair);
        stacks[pair] = currentStacks;
    }

    public float Calculate(float input)
    {
        // Iterates through our queue of modifiers, and applies each one successively
        // to our input value. Returns the output.
        // ================

        // Generate a ordered list of modifiers by sorting the priorities.
        // Ensures we run through our list in priority order.
        List<StatModifier> orderedModifiers = modifiers.OrderBy(x => x.priority).ToList();

        // Iterate through our pairs and apply them to the output value.
        float output = input;
        foreach (StatModifier pair in orderedModifiers)
        {
            output = CalculateStacks(output, pair.value, stacks[pair], pair.stackType);
        }

        return output;
    }

    public float CalculateStacks(float baseValue, float addValue, int stacks, ModifierStackType modifierType)
    {
        switch (modifierType)
        {
            case ModifierStackType.Flat_Linear:
                // Flat Linear: Each additional stack adds a flat value to the base.
                // EXAMPLE:
                // If addValue is +50, each stack will increase the value by 50, additively.
                // 1 stack: 50 + base
                // 2 stacks: 100 + base
                // 3 stacks: 150 + base
                return (stacks * addValue) + baseValue;
            case ModifierStackType.Percent_Linear:
                // Percent Linear: Each additional stack adds a constant percentage of the base value TO the base.
                // EXAMPLE:
                // If addValue is +50, each stack will increase the value by 50%, additively.
                // 1 stack: 150% of base (*1.5) 
                // 2 stacks: 200% of base (*2) 
                // 3 stacks: 250% of base (*2.5)
                return (1 + (stacks * addValue/100f)) * baseValue;
            case ModifierStackType.Percent_Exponential:
                // Percent Exponential: Each additional stack multiplies the base value by some percentage.
                // EXAMPLE:
                // If addValue is +50, each stack will increase the value by 50%, multiplicatively.
                // 1 stack: 150% of base (*1.5) 
                // 2 stacks: 225% of base (*2.25) 
                // 3 stacks: 337.5% of base (*3.375)
                return (float)Mathf.Pow(1 + (addValue/100f), stacks) * baseValue;
            default:
                return baseValue;
        }
    }
}