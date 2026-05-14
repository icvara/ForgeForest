using UnityEngine;

/// <summary>
/// This is base class for handeler's used throughout the game for inputs 
/// </summary>
public class BaseInputHandeler : MonoBehaviour
{
    protected GameInput inputActions;
    protected IInputMangementService inputMangementService;

    private void OnEnable()
    {
        ServiceLocator.Get<IInputMangementService>(OnGameInputManager);
    }

    private void OnDisable()
    {
        UnBindInputs();
    }

    private void OnGameInputManager(IInputMangementService Service)
    {
        inputMangementService = Service;
        inputActions = inputMangementService.InputActions;
        BindInputs();
    }

    public virtual void BindInputs() { }

    public virtual void UnBindInputs() { }
}
