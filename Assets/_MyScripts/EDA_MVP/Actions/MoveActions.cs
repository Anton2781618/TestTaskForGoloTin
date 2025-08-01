using ModularEventArchitecture;
using UnityEngine;

public class MoveActions : IEventType
{
    public int Id { get; }
    public string EventName { get; }

    private MoveActions(string eventName)
    {
        EventName = eventName;
        // Получаем хеш-код имени события, который будет уникален
        // Добавляем префикс чтобы еще больше избежать коллизий
        Id = ("BasicActionsTypes_" + eventName).GetHashCode();
    }

    public static readonly IEventType Move_To_point = new MoveActions("MoveToPoint");
    public static readonly IEventType Set_target = new MoveActions("SetTarget");
    public static readonly IEventType Use_target = new MoveActions("UseTarget");
}


