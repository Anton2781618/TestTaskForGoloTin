using ModularEventArchitecture;
using UnityEngine;
using UnityEngine.UI;

public class SpawnerViewModule : ModuleBase
{
    //-----------------------------------------------------------
    [Information("Этот модуль отвечает за создание визуального представления здания при его спавне. Если его удалить, то UI над зданием не будут отображаться в игре.", InformationAttribute.InformationType.Info, false)]
    [SerializeField] private BuildingViewModule _PrefabView;
    [SerializeField] private Vector2 _offset;
    //!-----------------------------------------------------------
    
    public override void Initialize()
    {
        Entity.SubscribeLocalEvent<SpawnEvent>(SpawnActions.Spawn_View, OnSpawnView);
    }

    private void OnSpawnView(SpawnEvent @event)
    {
        var canvas = @event.Building.transform.GetComponentInChildren<Canvas>();
        if (canvas == null)
        {
            var canvasObject = new GameObject("BuildingCanvas");
            canvasObject.transform.SetParent(@event.Building.transform);
            canvasObject.transform.localPosition = _offset;
            canvas = canvasObject.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.WorldSpace;
            canvasObject.AddComponent<CanvasScaler>();
            canvasObject.AddComponent<GraphicRaycaster>();
        }

        var view = Instantiate(_PrefabView, canvas.transform);

        view.SetImage(@event.ResourceData.Icon);

        view.SetResourceInfo($"{@event.ResourceData.ResourceName}: 0.0");

        @event.OnViewCreated?.Invoke(view);
    }
}
