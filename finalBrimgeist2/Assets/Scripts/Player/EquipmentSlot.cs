public class EquipmentSlot: ItemSlot
{
    public EquipmentType type;

    protected override void OnValidate()
    {
        base.OnValidate();
        gameObject.name = type.ToString() + " Slot";
    }

    public override bool CanReceiveItem(Item item)
    {
        if (item == null)
            return true;
        var equippableItem = item as EquippableItem;
        return equippableItem != null && equippableItem.type == type;
    }
}