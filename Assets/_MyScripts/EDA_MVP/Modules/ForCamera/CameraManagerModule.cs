using ModularEventArchitecture;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraManagerModule : ModuleBase
{
    //-----------------------------------------------------------
    [Information("Этот модуль отвечает за управление камерой в игре. Он позволяет перемещать камеру с помощью клавиатуры и мыши, а также поддерживает сенсорное управление на мобильных устройствах. ", InformationAttribute.InformationType.Info, false)]
    [SerializeField] private Camera _cameraTarget;
    [SerializeField] private Camera _helpCamera;
    [SerializeField] private float _keyboardMoveSpeed = 5f;
    [SerializeField] private float _mouseYOffset = 10f;
    [SerializeField] private bool _pc = true; 

    private Vector3 _startPosition;
    private Vector3 _cameraStartPosition;

    //!-----------------------------------------------------------
    
    public override void Initialize()
    {

    }

    public override void UpdateMe()
    {
        if (_pc)
        {
            HandlePCInput();
        }
        else
        {
            HandleTouchInput();
        }
    }

    private void HandleTouchInput()
    {
        Ray ray = _cameraTarget.ScreenPointToRay(Mouse.current.position.ReadValue());

        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            if (Mouse.current.leftButton.isPressed)
            {
                _startPosition = hitInfo.point;
                _cameraStartPosition = transform.position;
            }

            if (Mouse.current.leftButton.wasReleasedThisFrame)
            {
                Vector3 offset = hitInfo.point - _startPosition;
                transform.position = _cameraStartPosition + offset;
            }
        }

        
    }

    private void HandlePCInput()
    {
        Vector2 moveAmount = Vector2.zero;

        if (Keyboard.current.wKey.isPressed || Mouse.current.position.y.ReadValue() > Screen.height - 10f)
        {
            moveAmount.y += _keyboardMoveSpeed;
        }

        if (Keyboard.current.sKey.isPressed || Mouse.current.position.y.ReadValue() < 10f)
        {
            moveAmount.y -= _keyboardMoveSpeed;
        }

        if (Keyboard.current.aKey.isPressed || Mouse.current.position.x.ReadValue() < 10f)
        {
            moveAmount.x -= _keyboardMoveSpeed;
        }

        if (Keyboard.current.dKey.isPressed || Mouse.current.position.x.ReadValue() > Screen.width - 10f)
        {
            moveAmount.x += _keyboardMoveSpeed;
        }

        moveAmount *= Time.deltaTime;

        _cameraTarget.transform.position += new Vector3(moveAmount.x, 0, moveAmount.y);


        //плавное отдаление и приближение камеры
        if (Mouse.current.scroll.ReadValue().y != 0)
        {
            float scrollAmount = Mouse.current.scroll.ReadValue().y * Time.deltaTime * _mouseYOffset;
            Vector3 newPosition = _cameraTarget.transform.position + _cameraTarget.transform.forward * scrollAmount;
            _cameraTarget.transform.position = newPosition;
        }
        Debug.Log(Mouse.current.position.x.ReadValue() + " " + Mouse.current.position.y.ReadValue());
    }
    
    
}
