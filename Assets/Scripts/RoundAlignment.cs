using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[ExecuteInEditMode]
public class RoundAlignment : MonoBehaviour
{
    private Grid gridRef;
    private Vector3Int cellPos;
    private Vector3 cellCenter;

    private void Start()
    {
        gridRef = transform.parent.GetComponent<Grid>();
        SnapToWorldGrid();
    }

    public void SnapToWorldGrid()
    {
        Vector2 pos = new Vector2(transform.position.x, transform.position.y);

        cellPos = gridRef.WorldToCell(pos);
        cellCenter = gridRef.GetCellCenterWorld(cellPos);

        transform.position = cellCenter;

        this.enabled = false;
    }
}
