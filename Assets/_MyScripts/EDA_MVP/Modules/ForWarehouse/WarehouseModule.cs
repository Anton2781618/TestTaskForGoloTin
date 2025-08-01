using System.Collections.Generic;
using ModularEventArchitecture;
using UnityEngine;

public class WarehouseModule : ModuleBase
{
    //-----------------------------------------------------------
    private Dictionary<string, float> _resorces = new Dictionary<string, float>();

    //!-----------------------------------------------------------
    public override void Initialize()
    {
        Entity.SubscribeGlobalEvent<AddResourceEvent>(WarehouseActions.Add_Resource, OnAddResource);

        Entity.SubscribeGlobalEvent<AddResourceCountEvent>(WarehouseActions.Add_Resource_Count, OnAddResourceCount);
    }

    private void OnAddResource(AddResourceEvent resource) => _resorces.Add(resource.ResourceData.name, 0);
    private void OnAddResourceCount(AddResourceCountEvent @event)
    {
        //проверка на существование ресурса
        if (!_resorces.ContainsKey(@event.ResourceName))
        {
            Debug.LogWarning($"Ресурс {@event.ResourceName} не найден на складе.");
            return;
        }

        _resorces[@event.ResourceName] += @event.Amount;

        //обновить UI склада
        Entity.PublishGlobalEvent(WarehouseActions.Update_Warehouse_UI, new UpdateWarehouseUIEvent
        {
            ResourceName = @event.ResourceName,
            Amount = _resorces[@event.ResourceName]
        });

        Debug.Log($" ресурс: {@event.ResourceName}, количество: {@event.Amount}");
    }
}