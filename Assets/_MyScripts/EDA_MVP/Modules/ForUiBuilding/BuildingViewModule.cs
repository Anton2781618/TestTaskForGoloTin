using UnityEngine;
using TMPro;
using ModularEventArchitecture;

public class BuildingViewModule : ModuleBase
{
    //-----------------------------------------------------------
    [Information("Этот модуль отвечает за отображение информации о здании. Если его удалить, то информация о здании не будет отображаться в UI.", InformationAttribute.InformationType.Info, false)]
    [SerializeField] private TextMeshProUGUI _resourceInfoText;
    [SerializeField] private UnityEngine.UI.Image _image;
    private Camera _cameraMain;
    //!-----------------------------------------------------------

    public override void Initialize()
    {
        _cameraMain = Camera.main;
    }

    // View ничего не знает о модели, только отображает данные
    public void SetResourceInfo(string info)
    {
        if (_resourceInfoText != null)
            _resourceInfoText.text = info;
    }

    public void SetImage(Sprite sprite) => _image.sprite = sprite;

    public override void UpdateMe() => RotateHpBar();

    private void RotateHpBar() => transform.rotation = _cameraMain.transform.rotation;
}
