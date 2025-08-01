public class BuildingModel
{
    //-----------------------------------------------------------
    public ResourceData ResourceData { get; private set; }
    public float CurrentAmount { get; private set; }
    private float deltaTime = 1f; // это время между вызовами Produce

    //!-----------------------------------------------------------

    public BuildingModel(ResourceData resourceData)
    {
        ResourceData = resourceData;
        CurrentAmount = 0f;
    }

    public void Produce()
    {
        if (ResourceData == null || ResourceData.ProductionRate <= 0f)
            return;
        CurrentAmount += ResourceData.ProductionRate * deltaTime;
    }

    public (string, float) GetResourceInfo() => (ResourceData.ResourceName, CurrentAmount);

    public void ResetProduction() => CurrentAmount = 0f;
}