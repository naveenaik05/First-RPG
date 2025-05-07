using UnityEngine;
using UnityEngine.EventSystems;

public class UI_CraftSlot : UI_ItemSlot
{
    private void OnEnable()
    {
        UpdateSlot(item);
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        //Inventory craft item data
        ItemData_Equipment craftData = item.data as ItemData_Equipment;

        if (Inventory.instance.canCraft(craftData, craftData.craftingMaterials))
        {
            Debug.Log("Successful");
        }
    }
}
