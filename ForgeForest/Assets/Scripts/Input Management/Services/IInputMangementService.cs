using UnityEngine;

/// <summary>
/// This is service for Input management in this cass its getting the Input actions from New Input actions system 
/// and Allowing us to register it and then gain acess as needed 
/// </summary>
public interface IInputMangementService
{
    public GameInput InputActions { get; }
}
