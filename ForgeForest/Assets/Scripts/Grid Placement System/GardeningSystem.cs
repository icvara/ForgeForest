using System.Collections.Generic;
using System;
using UnityEngine;

public class GardeningSystem : MonoBehaviour
{
    protected IPlayerInputService playerInputService;


    [SerializeField] private GameObject Indicator;
    [SerializeField] private PlayerControler player;

    [SerializeField] private Grid grid;

    [SerializeField] private ObjectDatabaseSO Database;
    [SerializeField] private int SelectedObjectIndex = -1;

    [SerializeField] private ObjectData SelectedObjectData;

    private GridData PlantData;

    private List<GameObject> placedGameObjects = new();

    private void Awake()
    {
        ServiceLocator.Get<IPlayerInputService>(OnPlayerInputHandler);
        PlantData = new();
    }

    private void Start()
    {
        SelectObject(1);
        RegisterExistingPlants();
    }

    private void Update()
    {
        if(SelectedObjectIndex < 0)
        {
            Vector3Int gridPostion = grid.WorldToCell(player.RayCastPostion);
            Indicator.transform.position = new Vector3(grid.CellToWorld(gridPostion).x, 0.001f, grid.CellToWorld(gridPostion).z);
        }
   
        
    }

    private void RegisterExistingPlants()
    {
        PlacedPlant[] existingPlants =
            FindObjectsOfType<PlacedPlant>();

        foreach (PlacedPlant plant in existingPlants)
        {
            Vector3Int gridPosition =
                grid.WorldToCell(plant.transform.position);

            placedGameObjects.Add(plant.gameObject);

            PlantData.AddObject(
                gridPosition,
                plant.Size,
                plant.ObjectID,
                placedGameObjects.Count - 1
            );
        }
    }

    private void OnPlayerInputHandler(IPlayerInputService Service)
    {
        playerInputService = Service;
        playerInputService.InteractEvent += OnInteract;
    }

    //Selected object will need a UI Set UP , Currently I will be using a buttion click on the UI but I will set Up a Hot bar



    public void SelectObject(int ID)
    {

        SelectedObjectIndex = Database.ObjectsData.FindIndex(data => data.ID == ID);
        if(SelectedObjectIndex < 0)
        {
            Debug.Log($"No ID found {ID}");
            return;
        }

        SelectedObjectData = Database.ObjectsData[SelectedObjectIndex];
        
    }


    private void OnInteract()
    {
        switch (SelectedObjectData.objectType)
        {
            case ObjectTypes.Plant:
                PlantPlant();
                break;
            case ObjectTypes.Tool:
                RemovePlant();
                break;
            default:
                Debug.Log("No Tool Selected");
                break;
        }
    }


    private void PlantPlant()
    {
        Vector3Int gridPosition = grid.WorldToCell(player.RayCastPostion);

        bool PlacementValidity = CheckPlacementValidity(gridPosition, SelectedObjectIndex);
        //we can add a check placement preview
        
        if(PlacementValidity == false)
        {
            return;
        }

        GameObject NewObject = Instantiate(SelectedObjectData.prefab);
        NewObject.transform.position = new Vector3(grid.CellToWorld(gridPosition).x, 0.001f, grid.CellToWorld(gridPosition).z);
        placedGameObjects.Add(NewObject);
        PlantData.AddObject(gridPosition, Database.ObjectsData[SelectedObjectIndex].size, Database.ObjectsData[SelectedObjectIndex].ID, placedGameObjects.Count - 1);
    }

    private bool CheckPlacementValidity(Vector3Int gridPosition, int selectedObjectIndex)
    {
        return PlantData.CanPlaceObjectAT(gridPosition, Database.ObjectsData[selectedObjectIndex].size);
    }

    private void RemovePlant()
    {
        Vector3Int gridPosition =
            grid.WorldToCell(player.RayCastPostion);

        PlacementData data =
            PlantData.GetPlacementData(gridPosition);

        if (data == null)
        {
            Debug.Log("No plant found");
            return;
        }

        GameObject plant =
            placedGameObjects[data.placedObjectIndex];

        if (plant != null)
        {
            Destroy(plant);
        }

        placedGameObjects[data.placedObjectIndex] = null;

        PlantData.RemoveObjectAt(gridPosition);
    }


}