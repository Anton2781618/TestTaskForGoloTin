using UnityEngine;

public class UIResCard : MonoBehaviour
{
    //-----------------------------------------------------------
    [SerializeField] private UnityEngine.UI.Image _iconImage;
    [SerializeField] private TMPro.TextMeshProUGUI _text;
    //!-----------------------------------------------------------

    private string _resourceName;
    public void Setup(ResourceData resourceData)
    {
        _resourceName = resourceData.ResourceName;
        _text.text = $"{_resourceName}: 0.0";
        _iconImage.sprite = resourceData.Icon;
    }

    public void UpdateAmount(float amount) => _text.text = $"{_resourceName}: {amount:F1}";
}
