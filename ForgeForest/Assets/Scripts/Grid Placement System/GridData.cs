using UnityEngine;
using System.Collections.Generic;
using System;

public class GridData
{
    Dictionary<Vector3Int, PlacementData> placedObjects = new();

    public void AddObject(Vector3Int gridPosition, Vector2Int objectSize, int ID , int PlacedObjectIndex)
    {
        List<Vector3Int> PositionsToOccupy = CalculatePositions(gridPosition, objectSize);
        PlacementData data = new PlacementData(PositionsToOccupy,ID,PlacedObjectIndex); 
        foreach(var pos in PositionsToOccupy)
        {
            if (placedObjects.ContainsKey(pos))
            {
                throw new Exception($"Dictionary already contains this cell position {pos} ");

            }

            placedObjects[pos] = data;

        }
    }

    private List<Vector3Int> CalculatePositions(Vector3Int gridPosition, Vector2Int objectSize)
    {
        List<Vector3Int> returnVal = new();
        for (int x = 0; x < objectSize.x; x++)
        {
            for(int y = 0; y < objectSize.y; y++)
            {
                returnVal.Add(gridPosition + new Vector3Int(x, 0, y)); 
            }
        }

        return returnVal;
    }

    public bool CanPlaceObjectAT(Vector3Int gridPosition, Vector2Int objectsize)
    {
        List<Vector3Int> PositionsToOccupy = CalculatePositions(gridPosition, objectsize);
        foreach (var pos in PositionsToOccupy)
        {
            if (placedObjects.ContainsKey(pos))
                return false;
        }
        return true;
    }

    public PlacementData GetPlacementData(Vector3Int gridPosition)
    {
        if (placedObjects.TryGetValue(gridPosition, out PlacementData data))
        {
            return data;
        }

        return null;
    }

    public void RemoveObjectAt(Vector3Int gridPosition)
    {
        if (!placedObjects.TryGetValue(gridPosition, out PlacementData data))
            return;

        foreach (Vector3Int pos in data.OccupiedPositions)
        {
            placedObjects.Remove(pos);
        }
    }
}



public class PlacementData
{
    public List<Vector3Int> OccupiedPositions;

    public int ID { get; private set; }

    public int placedObjectIndex { get; private set; }

    public PlacementData(List<Vector3Int> occupiedPositions, int iD, int PlaceObjectIndex)
    {
        this.OccupiedPositions = occupiedPositions;
        ID = iD;
        placedObjectIndex = PlaceObjectIndex;
    }


}