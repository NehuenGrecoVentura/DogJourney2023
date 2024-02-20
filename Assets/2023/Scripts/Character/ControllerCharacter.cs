using UnityEngine;

public class ControllerCharacter : MonoBehaviour
{
    private ModelCharacter _model;

    public ControllerCharacter(ModelCharacter model)
    {
        _model = model;
    }

    public void Move(bool itemPicked)
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        if (hor != 0 || ver != 0)
        {
            _model.Movement(hor, ver);
            InputsDogsInMove(itemPicked);
        }

        else InputsDogsInIdle(itemPicked);       
    }

    public void OnUpdate()
    {
        _model.SelectDogs(KeyCode.Alpha1, KeyCode.Alpha2);
    }

    private void InputsDogsInMove(bool itemPicked)
    {
        // Si no estoy corriendo y no estoy agachado, entonces camino.
        if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.LeftControl)) _model.Walk(itemPicked);

        // Si no estoy agachado, entonces corro.
        else if (Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.LeftControl)) _model.Run();

        // Si no corro, entonces estoy caminando agachado.
        else if (Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.LeftShift)) _model.CrouchWalk();

        if (Input.GetKeyDown(KeyCode.Q)) _model.CallMoveDog();
        if (Input.GetKeyDown(KeyCode.R)) _model.CallAllDogs();
        if (Input.GetKeyDown(KeyCode.E)) _model.StopDog();
    }

    private void InputsDogsInIdle(bool itemPicked)
    {
        // Si no me agacho o corro, entonces estoy en idle.
        if (!Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.LeftShift)) _model.Idle(itemPicked);

        // Si me agacho y no corro, entonces estoy en agachado en idle.
        else if (Input.GetKey(KeyCode.LeftControl) && !Input.GetKey(KeyCode.LeftShift)) _model.IdleCrouch();

        if (Input.GetKeyDown(KeyCode.Q)) _model.CallIdleDog();
        else if (Input.GetKeyDown(KeyCode.R)) _model.CallAllDogs();
        if (Input.GetKeyDown(KeyCode.E)) _model.StopDog();
    }
}