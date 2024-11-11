using System;
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

    public static Action OnSnapToGrid;

    private void Awake()
    {
        FindGrid();
    }
    private void Start()
    {
        if (gridRef)
            SnapToWorldGrid();
    }

    public void SnapToWorldGrid()
    {
        if (!gridRef)
            FindGrid();

        Vector2 pos = new Vector2(transform.position.x, transform.position.y);

        cellPos = gridRef.WorldToCell(pos);
        cellCenter = gridRef.GetCellCenterWorld(cellPos);

        transform.position = cellCenter;
        OnGridSnap?.Invoke();
        OnSnapToGrid?.Invoke();
    }

    private void FindGrid()
    {
        gridRef = transform.parent.GetComponent<Grid>();
    }
}