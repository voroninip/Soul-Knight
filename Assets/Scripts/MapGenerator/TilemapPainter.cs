using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapPainter : MonoBehaviour
{
    public Tilemap wall;
    public Tilemap floor;

    public List<Tile> wallTiles;
    public List<Tile> sideTiles;
    public List<Tile> floorTiles;
    public GameObject enemyPrefab;

    public void PaintMap(int[,] map)
    {
        PreprocessMap(map);
        int width = map.GetLength(0);
        int height = map.GetLength(1);
        CenterTilemap(width, height);
        wall.ClearAllTiles();
        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 0; y < map.GetLength(1); y++)
            {
                if (map[x, y] == 1)
                {
                    int idx = Random.Range(0, wallTiles.Count - 1);
                    wall.SetTile(new Vector3Int(x, y, 0), wallTiles[idx]);
                }
                else if (map[x, y] == 2)
                {
                    int idx = Random.Range(0, sideTiles.Count - 1);
                    wall.SetTile(new Vector3Int(x, y, 0), sideTiles[idx]);
                }
                else if (System.Math.Abs(x - width / 2) > 10 && System.Math.Abs(y - height / 2) > 10)
                {
                    //spawn enemy whith small chance
                    // spawn enemy with small chance
                    if (Random.value < 0.01f)
                    {
                        Instantiate(enemyPrefab, new Vector3(x-64, y-64, 0), Quaternion.identity);
                    }
                }
            }
        }
        PostProcessMap(width, height);
        PaintFloor(width, height);
    }

    public void CenterTilemap(int width, int height)
    {
        wall.GetComponent<Transform>().position = new Vector3(-width / 2, -height / 2, 0);
        floor.GetComponent<Transform>().position = new Vector3(-width / 2, -height / 2, 0);
    }

    public void ClearMap()
    {
        wall.ClearAllTiles();
    }


    void PreprocessMap(int[,] map) {
        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int y = 1; y < map.GetLength(1); y++)
            {
                if (map[x, y] == 1 && map[x, y - 1] == 0) {
                    map[x, y] = 2;
                }
            }
        }
    }

    void PaintFloor(int width, int height)
    {
        floor.ClearAllTiles();
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                int idx = Random.Range(0, floorTiles.Count - 1);
                floor.SetTile(new Vector3Int(x, y, 0), floorTiles[idx]);
            }
        }
    }

    void PostProcessMap(int width, int height, int offset = 20) {
        /*FillWall(-10, -10, width + 10, 0);
        FillWall(-10, 0, 0, height + 10);
        FillWall(width, 0, width + 10, height + 10);
        FillWall(0, height, width, height + 10);*/
        FillWall(-offset, -offset, width + offset, 0);
        FillWall(-offset, 0, 0, height + offset);
        FillWall(width, 0, width + offset, height + offset);
        FillWall(0, height, width, height + offset);
    }

    void FillWall(int x1, int y1, int x2, int y2) {
        for (int x = x1; x < x2; x++) {
            for (int y = y1; y < y2; y++) {
                int idx = Random.Range(0, wallTiles.Count - 1);
                wall.SetTile(new Vector3Int(x, y, 0), wallTiles[idx]);
            }
        }
    }

}
