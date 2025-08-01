using System.Collections.Generic;
using ModularEventArchitecture;

public class UpdateWarehouseUIEvent : IEventData
{
    public string ResourceName { get; set; }
    public float Amount { get; set; }
}
