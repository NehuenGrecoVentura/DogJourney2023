using UnityEngine;

public class PlayerController : IController
{
    private PlayerModel _model;

    public PlayerController(PlayerModel model)
    {
        _model = model;
    }

    public void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.F))
            _model.Interact();
    }
}
