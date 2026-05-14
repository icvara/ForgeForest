using UnityEngine;

public class Movement : MonoBehaviour
{

    protected IPlayerInputService playerInputService;

    [SerializeField] private float MovementSpeed = 10f;
    

    private Vector3 moveInput;

    private CharacterController CC;

    private void Awake()
    {
        CC = GetComponent<CharacterController>();

        ServiceLocator.Get<IPlayerInputService>(OnPlayerInputHandler);
        
    }


    private void OnPlayerInputHandler(IPlayerInputService Service)
    {
        playerInputService = Service;
        playerInputService.MoveEvent += MoveInput;
    }

    private void MoveInput(Vector2 MoveInput)
    {
        moveInput = new Vector3(MoveInput.x, 0, MoveInput.y);
        Debug.Log(moveInput);
    }


    private void Update()
    {
        PlayerMovement();
    }


    private void PlayerMovement()
    {
        Matrix4x4 isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));
        Vector3 moveDir = isoMatrix.MultiplyPoint3x4(moveInput).normalized;

        CC.Move(moveDir * MovementSpeed * Time.deltaTime);

    }

}
