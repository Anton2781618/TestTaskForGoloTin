using ModularEventArchitecture;

public class AddResourceEvent : IEventData
{
    public ResourceData ResourceData;

}
public class AddResourceCountEvent : IEventData
{
    public string ResourceName;
    public float Amount;

}

