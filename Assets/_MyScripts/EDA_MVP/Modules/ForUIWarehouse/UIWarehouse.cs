using System.Collections.Generic;
using ModularEventArchitecture;
using UnityEngine;

public class UIWarehouse : ModuleBase
{
    //-----------------------------------------------------------
    [Information("Этот модуль отвечает за отображение ресурсов на складе. Если его удалить, то ресурсы не будут отображаться в UI.", InformationAttribute.InformationType.Info, false)]
    [SerializeField] private UIResCard _uiResCardPrefab;
    private Dictionary<string, UIResCard> _resCards = new Dictionary<string, UIResCard>();
    //!-----------------------------------------------------------
    public override void Initialize()
    {
        Entity.SubscribeGlobalEvent<AddResourceEvent>(WarehouseActions.Add_Resource, OnAddResource);

        Entity.SubscribeGlobalEvent<UpdateWarehouseUIEvent>(WarehouseActions.Update_Warehouse_UI, OnUpdateCard);
    }

    private void OnAddResource(AddResourceEvent resource)
    {
        var resCard = Instantiate(_uiResCardPrefab, transform);

        resCard.Setup(resource.ResourceData);

        if (_resCards.ContainsKey(resource.ResourceData.name))
        {
            Debug.LogError($"Ресурс {resource.ResourceData.name} уже существует в складе. Вы скорее всего добавляете его повторно. прверьте поле ResourceData в зданиях, которые добавляют ресурсы на склад.");
            return;
        }

        _resCards.Add(resource.ResourceData.name, resCard);
    }

    private void OnUpdateCard(UpdateWarehouseUIEvent resourceCountEvent)
    {
        _resCards[resourceCountEvent.ResourceName].UpdateAmount(resourceCountEvent.Amount);

        Debug.Log($"Обновление UI: ресурс {resourceCountEvent.ResourceName}, количество: {resourceCountEvent.Amount}");        
    }
}
