using UnityEngine;
using static Types;

[CreateAssetMenu]
public class EquippableItem : Item
{
    public int StrengthBonus;
    public int AgilityBonus;
    public int IntelligenceBonus;
    public int VitalityBonus;
    public float StrengthPercentBonus;
    public float AgilityPercentBonus;
    public float IntelligencePercentBonus;
    public float VitalityPercentBonus;

    public EquipmentType type;

    public void Equip(InventoryManager i)
    {
        if(StrengthBonus != 0)
            i.Strength.AddModifier(new StatModifier(StrengthBonus, StatModType.Flat, this));
        if (AgilityBonus != 0)
            i.Agility.AddModifier(new StatModifier(AgilityBonus, StatModType.Flat, this));
        if (IntelligenceBonus != 0)
            i.Intelligence.AddModifier(new StatModifier(IntelligenceBonus, StatModType.Flat, this));
        if (VitalityBonus != 0)
            i.Vitality.AddModifier(new StatModifier(VitalityBonus, StatModType.Flat, this));

        if (StrengthPercentBonus != 0)
            i.Strength.AddModifier(new StatModifier(StrengthPercentBonus, StatModType.PercentMult, this));
        if (AgilityPercentBonus != 0)
            i.Agility.AddModifier(new StatModifier(AgilityPercentBonus, StatModType.PercentMult, this));
        if (IntelligencePercentBonus != 0)
            i.Intelligence.AddModifier(new StatModifier(IntelligencePercentBonus, StatModType.PercentMult, this));
        if (VitalityPercentBonus != 0)
            i.Vitality.AddModifier(new StatModifier(VitalityPercentBonus, StatModType.PercentMult, this));
    }
    public void Unequip(InventoryManager i)
    {
        i.Strength.RemoveAllModifiersFromSource(this);
        i.Agility.RemoveAllModifiersFromSource(this);
        i.Intelligence.RemoveAllModifiersFromSource(this);
        i.Vitality.RemoveAllModifiersFromSource(this);
    }
}
