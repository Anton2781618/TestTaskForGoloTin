using ModularEventArchitecture;
using UnityEngine;
using UniRx;
using System;

public class BuildingModule : ModuleBase
{
    //-----------------------------------------------------------
    [Information("Этот модуль отвечает за логику здания, его производство ресурсов и взаимодействие с визуальным представлением. Если его удалить, то здание не будет производить ресурсы и не будет взаимодействовать с визуальным представлением.", InformationAttribute.InformationType.Info, false)]
    [SerializeField] private ResourceData resourceData;

    private BuildingViewModule _buildingView;

    private BuildingModel Model;

    private IDisposable _produceSubscription;

    //!-----------------------------------------------------------

    public override void Initialize()
    {
        Model = new BuildingModel(resourceData);

        Entity.SubscribeLocalEvent<UseEvent>(MoveActions.Use_target, OnUse);


        Observable.Timer(System.TimeSpan.FromSeconds(0.1f))
            .Subscribe(_ =>
            {
                Entity.PublishGlobalEvent(WarehouseActions.Add_Resource, new AddResourceEvent { ResourceData = resourceData });

                Entity.PublishLocalEvent(SpawnActions.Spawn_View, new SpawnEvent
                {
                    Building = this,
                    ResourceData = resourceData,
                    OnViewCreated = view =>
                    {
                        _buildingView = view;
                        // Debug.Log($"View создан и связан: {view}");
                    }
                });
            })
            .AddTo(this);

        _produceSubscription = Observable.Interval(System.TimeSpan.FromSeconds(1))
            .Subscribe(_ => Produce()).AddTo(this);
    }

    private void OnUse(UseEvent @event)
    {
        var info = Model.GetResourceInfo();

        Entity.PublishGlobalEvent(WarehouseActions.Add_Resource_Count, new AddResourceCountEvent
        {
            ResourceName = info.Item1,
            Amount = info.Item2
        });

        Model.ResetProduction();

        UdateUpdateView();
    }

    private void Produce()
    {
        Model.Produce();

        UdateUpdateView();
    }

    private void UdateUpdateView()
    {
        var info = Model.GetResourceInfo();
        _buildingView.SetResourceInfo($"{info.Item1}: {info.Item2:F1}");
        
    }
}