using ModularEventArchitecture;
using UnityEngine;


[RequireComponent(typeof(Outline)),]
public class SelectedModule : ModuleBase
{
    //-----------------------------------------------------------
    [Information("Этот модуль нужен для выделения объектов. Если его удалить то обект на котором модуль стоит не будет выделятся и все.", InformationAttribute.InformationType.Info, false)]
    [SerializeField] private Outline _outline;
    [SerializeField] private Transform _point;

    //!-----------------------------------------------------------
    
    public override void Initialize()
    {

    }

    private void OnValidate() 
    {
        if (_outline == null)
        {
            _outline = GetComponent<Outline>();
            _outline.enabled = false;
        }
    }

    public void OnMouseEnter()
    {
        _outline.enabled = true;
    }

    public void OnMouseExit()
    {
        _outline.enabled = false;
    }

    private void OnMouseDown()
    {
        Entity.PublishGlobalEvent<MoveToPointEvent>(MoveActions.Move_To_point, new MoveToPointEvent
        {
            Point = _point.position
        });
        
        Entity.PublishGlobalEvent<SetTargetEvent>(MoveActions.Set_target, new SetTargetEvent
        {
            Target = Entity
        });
        
    }
}
