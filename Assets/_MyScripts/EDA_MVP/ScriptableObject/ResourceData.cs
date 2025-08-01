using UnityEngine;



[CreateAssetMenu(fileName = "ResourceData", menuName = "ResourceData", order = 0)]
public class ResourceData : ScriptableObject 
{
    [Header("Основные параметры ресурса")]
    public string ResourceName;
    public Sprite Icon;
    [Tooltip("Скорость производства в секунду")] public float ProductionRate = 1f;
}
