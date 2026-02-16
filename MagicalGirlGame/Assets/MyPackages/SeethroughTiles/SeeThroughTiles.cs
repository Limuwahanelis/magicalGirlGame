using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class SeeThroughTiles : MonoBehaviour
{
    [SerializeField] Color TransparentColor;
    [SerializeField] Color fullyTransparentColor;
    [SerializeField] Tilemap fullyTransparentMap;
    List<Vector3Int> cellPositions;
    List<Vector3Int> fullytransparentCellPositions;
    //[SerializeField] PlayerDetection playerDetection;
    private Tilemap map;
    private Color basicColor = new Color(1f, 1f, 1f, 1f);
    private float playerGroundColCenterX;
    private float extent;

    // Start is called before the first frame update
    void Start()
    {
        fullytransparentCellPositions = new List<Vector3Int>();
        cellPositions = new List<Vector3Int>();
        map=transform.GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(!enabled) return;
        playerGroundColCenterX = collision.bounds.center.x;
        extent = collision.bounds.extents.x;
        Vector3Int tempTile = map.WorldToCell(new Vector3(playerGroundColCenterX - extent - 0.5f, collision.bounds.center.y));
        if (map.GetTile(tempTile))
        {
            GetTilesLeft(tempTile);
        }
        if (map.GetTile(map.WorldToCell(new Vector3(playerGroundColCenterX + extent + 0.5f, collision.bounds.center.y))))
        {
            tempTile = map.WorldToCell(new Vector3(playerGroundColCenterX + extent + 0.5f, collision.bounds.center.y));
            GetTilesRight(tempTile);
        }
        if(fullyTransparentMap)
        {
            tempTile = fullyTransparentMap.WorldToCell(new Vector3(playerGroundColCenterX - extent - 0.5f, collision.bounds.center.y));
            if (fullyTransparentMap.GetTile(tempTile))
            {
                GetTilesLeft(tempTile,true);
            }
            tempTile = fullyTransparentMap.WorldToCell(new Vector3(playerGroundColCenterX + extent + 0.5f, collision.bounds.center.y));
            if (fullyTransparentMap.GetTile(tempTile))
            {
                GetTilesRight(tempTile,true);
            }

        }
        MakeTilesTransparent();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        RemoveTransparency();
    }
    void GetTilesRight(Vector3Int firstTileToShow,bool fullyTransparent=false)
    {
        Vector3Int curTileCellPos = firstTileToShow;
        while (map.GetTile(curTileCellPos))
        {
            cellPositions.Add(curTileCellPos);
            GetTilesUp(curTileCellPos);
            GetTilesDown(curTileCellPos);
            curTileCellPos = map.WorldToCell(new Vector3(curTileCellPos.x + 1.2f, curTileCellPos.y, 0));
        }
        curTileCellPos = firstTileToShow;
        if (fullyTransparent)
        {
            while (fullyTransparentMap.GetTile(curTileCellPos))
            {
                fullytransparentCellPositions.Add(curTileCellPos);
                GetTilesUp(curTileCellPos, true);
                GetTilesDown(curTileCellPos, true);
                curTileCellPos = fullyTransparentMap.WorldToCell(new Vector3(curTileCellPos.x + 1.2f, curTileCellPos.y, 0));
            }
        }
    }
    void GetTilesLeft ( Vector3Int firstTileToShow, bool fullyTransparent = false)
    {
        Vector3Int curTileCellPos = firstTileToShow;
        while (map.GetTile(curTileCellPos))
        {
            cellPositions.Add(curTileCellPos);
            GetTilesUp(curTileCellPos);
            GetTilesDown(curTileCellPos);
            curTileCellPos = map.WorldToCell(new Vector3(curTileCellPos.x - 0.5f, curTileCellPos.y, 0));   
        }
        curTileCellPos = firstTileToShow;
        if (fullyTransparent)
        {
            while (fullyTransparentMap.GetTile(curTileCellPos))
            {
                fullytransparentCellPositions.Add(curTileCellPos);
                GetTilesUp(curTileCellPos, true);
                GetTilesDown(curTileCellPos, true);
                curTileCellPos = fullyTransparentMap.WorldToCell(new Vector3(curTileCellPos.x - 0.5f, curTileCellPos.y, 0));
            }
        }
    }
    void GetTilesUp(Vector3Int startingTile, bool fullyTransparent = false)
    {
        Vector3Int curTileCellPos = map.WorldToCell(new Vector3(startingTile.x, startingTile.y + 1.2f, 0));
        while (map.GetTile(curTileCellPos))
        {
            cellPositions.Add(curTileCellPos);
            curTileCellPos = map.WorldToCell(new Vector3(curTileCellPos.x, curTileCellPos.y+1.2f, 0));
        }
        if (fullyTransparent)
        {
            curTileCellPos = startingTile;
            while (fullyTransparentMap.GetTile(curTileCellPos))
            {
                fullytransparentCellPositions.Add(curTileCellPos);
                curTileCellPos = fullyTransparentMap.WorldToCell(new Vector3(curTileCellPos.x, curTileCellPos.y + 1.2f, 0));
            }
        }
    }
    void GetTilesDown(Vector3Int startingTile, bool fullyTransparent = false)
    {
        Vector3Int curTileCellPos =  map.WorldToCell(new Vector3(startingTile.x, startingTile.y - 0.5f, 0));
        while (map.GetTile(curTileCellPos))
        {
            cellPositions.Add(curTileCellPos);
            curTileCellPos = map.WorldToCell(new Vector3(curTileCellPos.x, curTileCellPos.y - 0.5f, 0));
        }
        if (fullyTransparent)
        {
            curTileCellPos = startingTile;
            while (fullyTransparentMap.GetTile(curTileCellPos))
            {
                fullytransparentCellPositions.Add(curTileCellPos);
                curTileCellPos = fullyTransparentMap.WorldToCell(new Vector3(curTileCellPos.x, curTileCellPos.y - 0.5f, 0));
            }
        }
    }
    void MakeTilesTransparent()
    {

        for (int i = 0; i < cellPositions.Count; i++)
        {
            map.RemoveTileFlags(cellPositions[i], TileFlags.LockColor);
            map.SetColor(cellPositions[i], TransparentColor);

        }
        if (fullyTransparentMap)
        {
            for (int i = 0; i < fullytransparentCellPositions.Count; i++)
            {
                fullyTransparentMap.RemoveTileFlags(fullytransparentCellPositions[i], TileFlags.LockColor);
                fullyTransparentMap.SetColor(fullytransparentCellPositions[i], fullyTransparentColor);

            }
        }
    }

    void RemoveTransparency()
    {
        foreach(Vector3Int pos in cellPositions)
        {
            map.SetColor(pos, basicColor);
        }
        cellPositions.Clear();
        if(fullyTransparentMap)
        {
            foreach (Vector3Int pos in fullytransparentCellPositions)
            {
                fullyTransparentMap.SetColor(pos, basicColor);
            }
            fullytransparentCellPositions.Clear();
        }
    }
}
