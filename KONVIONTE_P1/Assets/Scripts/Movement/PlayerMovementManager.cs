using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ESTE SCRIPT VA ATACHADO AL OBJETO PLAYER
public class PlayerMovementManager : MonoBehaviour
{
    #region Properties
    private PlayerInputActions _playerInputActions;
    private MovementComponent _playerMovementComponent;

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Enable();
        _playerMovementComponent = GetComponent<MovementComponent>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _playerMovementComponent.SetDirection(_playerInputActions.Player.HorizontalMove.ReadValue<Vector2>());
    }
}
