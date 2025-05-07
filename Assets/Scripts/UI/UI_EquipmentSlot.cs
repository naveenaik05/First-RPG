using UnityEngine;
using UnityEngine.EventSystems;

public class UI_EquipmentSlot : UI_ItemSlot
{
    public EquipmentType slotType;

    void OnValidate()
    {
        gameObject.name = "Equipment Slot-" + slotType.ToString();
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        //Invetory unequip item
        Inventory.instance.UnequipItem(item.data as ItemData_Equipment);
        Inventory.instance.AddItem(item.data as ItemData_Equipment);

        CleanUpSlot();
    }
}
