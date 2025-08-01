using ModularEventArchitecture;
using UnityEngine;

namespace Entitys.LevelManagerModules.RaycastModule
{
    public class RaycastModule : ModuleBase
    {
        //-----------------------------------------------------------
        [Information("Этот модуль отвечает за обработку кликов мыши по земле. Если его удалить, то игрок не сможет передвигаться именно по земле.", InformationAttribute.InformationType.Info, false)]
        [SerializeField] private LayerMask _layerMask;
        [SerializeField] private Transform debugTransform;
        private RaycastHit _hit = new RaycastHit();
        private Camera _camera;
        //!-----------------------------------------------------------

        public override void Initialize()
        {
            if (Camera.main != null)
            {
                _camera = Camera.main;
            }

        }

        
        public override void UpdateMe()
        {
            Raycaster();
        }


        private void Raycaster()
        {
            if (_camera != null)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                
                if (Physics.Raycast(ray, out _hit, 150f))
                {
                    if ((_layerMask & (1 << _hit.collider.gameObject.layer)) == 0)
                    {
                        return; // Игнорируем объекты, которые не входят в слой
                    }

                    if (debugTransform != null) debugTransform.position = _hit.point;

                    if (Input.GetMouseButtonDown(0))
                    { 
                        Entity.PublishGlobalEvent<MoveToPointEvent>(MoveActions.Move_To_point, new MoveToPointEvent
                        {
                            Point = _hit.point
                        });                 
                    }

                }
            }
        }
    }
}
