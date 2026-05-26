using UnityEngine;

public class PlacedPlant : MonoBehaviour
{
    public int ObjectID;
    public Vector2Int Size = Vector2Int.one;
    public int Phase = 0;

    private void Start()
    {
        PlantGrowthSystem plantGrowth = GetComponentInChildren<PlantGrowthSystem>();
        
        plantGrowth.SetPhase(Phase);
    }



}
