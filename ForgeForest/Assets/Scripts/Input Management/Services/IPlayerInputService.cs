using System;
using UnityEngine;

public interface IPlayerInputService
{
    event Action<Vector2> MoveEvent;
    event Action<Vector2> LookEvent;
    event Action InteractEvent;
    event Action SubmitEvent;
    event Action SprintEvent;
}
