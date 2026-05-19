using System;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{

    protected IPlayerInputService playerInputService;

    [SerializeField] private float MovementSpeed = 10f;

    [SerializeField] private Transform placementRayCastStart;

    [SerializeField] private Vector3 LastStoredPosition = new Vector3(1,0,0);
    [SerializeField] private LayerMask PlacementLayerMask;
    public Vector3 RayCastPostion;


    private Matrix4x4 isoMatrix = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0));

    private Vector3 moveInput;


    private CharacterController CC;

    private void Awake()
    {
        CC = GetComponent<CharacterController>();

        ServiceLocator.Get<IPlayerInputService>(OnPlayerInputHandler);
        
    }

    public void InteractEvent()
    {
        
    }


    private void OnPlayerInputHandler(IPlayerInputService Service)
    {
        playerInputService = Service;
        playerInputService.MoveEvent += MoveInput;
    }

    private void OnDisable()
    {
        playerInputService.MoveEvent -= MoveInput;
    }

    #region Input Binds

    private void MoveInput(Vector2 MoveInput)
    {
        moveInput = new Vector3(MoveInput.x, 0, MoveInput.y);
       
    }

  

    #endregion

    private void Update()
    {
        PlayerMovement();
        placementSystemPosition();
    }

    private Vector3  MoveDir()
    {

        return moveInput;
        //return isoMatrix.MultiplyPoint3x4(moveInput).normalized;

    }

    private void PlayerMovement()
    {
        CC.Move(MoveDir() * MovementSpeed * Time.deltaTime);
    }

    private void placementSystemPosition()
    {
        if (MoveDir() == Vector3.zero) return;

        LastStoredPosition = MoveDir();

        placementRayCastStart.position = transform.position + (LastStoredPosition * 1.5f);

        RaycastHit hitinfo;

        if(Physics.Raycast(placementRayCastStart.position ,Vector3.down,out  hitinfo,2f, PlacementLayerMask))
        {
            RayCastPostion = hitinfo.point;
        }
        
        
    }

}
