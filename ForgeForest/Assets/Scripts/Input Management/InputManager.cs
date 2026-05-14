using UnityEngine;

public class InputManager : MonoBehaviour , IInputMangementService
{

    private GameInput inputActions;

    //This is refrence to are IInputManagement Service 
    public GameInput InputActions => inputActions;


    private void Awake()
    {
        //We create a new Gameinput script for us to refrence so we don't have to refrence on in the inspector
        inputActions = new GameInput();

        //We then register this class to the service locator so we have easy to grap refrence
        ServiceLocator.Register<IInputMangementService>(this);

        inputActions.Player.Enable();

    }

    private void OnDestroy()
    {
        inputActions.Disable();
        inputActions.Dispose();

        ServiceLocator.Unregister<IInputMangementService>();
    }


}
