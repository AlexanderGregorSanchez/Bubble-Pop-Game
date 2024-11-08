using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SnapToGrid : MonoBehaviour
{
    private Grid gridRef;
    private Vector3Int cellPos;
    private Vector3 cellCenter;

    public UnityEvent OnGridSnap;

    private void Awake()
    {
        gridRef = transform.parent.GetComponent<Grid>();
    }
    private void Start()
    {
        if (gridRef)
        {
            SnapToWorldGrid();
        } 
    }

    public void SnapToWorldGrid()
    {
        Vector2 pos = new Vector2(transform.position.x, transform.position.y);

        cellPos = gridRef.WorldToCell(pos);
        cellCenter = gridRef.GetCellCenterWorld(cellPos);
        
        transform.position = cellCenter;
        OnGridSnap?.Invoke();
    }
}
