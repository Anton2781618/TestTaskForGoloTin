using ModularEventArchitecture;

public class WarehouseActions : IEventType
{
    public int Id { get; }
    public string EventName { get; }

    private WarehouseActions(string eventName)
    {
        EventName = eventName;
        // Получаем хеш-код имени события, который будет уникален
        // Добавляем префикс чтобы еще больше избежать коллизий
        Id = ("BasicActionsTypes_" + eventName).GetHashCode();
    }

    public static readonly IEventType Add_Resource = new WarehouseActions("AddResource");
    public static readonly IEventType Add_Resource_Count = new WarehouseActions("AddResourceCount");
    public static readonly IEventType Update_Warehouse_UI = new WarehouseActions("UpdateWarehouseUI");
}