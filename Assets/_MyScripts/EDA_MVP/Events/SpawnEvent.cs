using System;
using ModularEventArchitecture;

public class SpawnEvent : IEventData
{
    public BuildingModule Building;
    public ResourceData ResourceData;
    public Action<BuildingViewModule> OnViewCreated;
}
