using ModularEventArchitecture;

public class SpawnActions : IEventType
    {
        public int Id { get; }
        public string EventName {get;}

        private SpawnActions(string eventName)
        {
             EventName = eventName;
            // Получаем хеш-код имени события, который будет уникален
            // Добавляем префикс чтобы еще больше избежать коллизий
            Id = ("BasicActionsTypes_" + eventName).GetHashCode();
        }

    public static readonly IEventType Spawn_View = new SpawnActions("SpawnView");
            
}
