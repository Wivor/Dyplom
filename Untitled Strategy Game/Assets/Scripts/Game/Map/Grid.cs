using System;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public Transform hexPrefab;
    private int _id = 0;


    private Vector3 _startPos;
    private float _hexWidth;
    private float _hexHeight;
    private float _offset;
    private int _gridWidth;
    private int _gridHeight;
    private const float Size = 1;
    private const float Gap = 0.1f;

    public int GridWidth { get => _gridWidth; private set => _gridWidth = value; }
    public int GridHeight { get => _gridHeight; private set => _gridHeight = value; }

    private Grid()
    {
        CalcHexSize();
    }

    public void GenerateGrid(int width, int height)
    {
        _gridWidth = width;
        _gridHeight = height;
        
        ClearGrid();
        CalcStartPos();
        CreateGrid();
        ConnectNeighbours();
    }

    public void ClearGrid()
    {
        _id = 0;
        Storage.ClearStorage();
        foreach (Transform child in transform)
            Destroy(child.gameObject);
    }

    private void AddGap()
    {
        _hexWidth += _hexWidth * Gap;
        _hexHeight += _hexHeight * Gap;
    }

    private void CalcHexSize()
    {
        _hexWidth = (float)Math.Sqrt(3) * Size;
        _hexHeight = Size * 2;
        _offset = _hexWidth / 2;
        AddGap();
    }

    private void CalcStartPos()
    {
        float x = -_hexWidth * (_gridWidth / 2);
        float z = _hexHeight * (_gridHeight / 2);

        _startPos = new Vector3(x, 0, z);
    }

    private void CreateGrid()
    {
        for (int y = 0; y < _gridHeight; y++)
        {
            for (int x = 0; x < _gridWidth; x++)
            {
                Transform hex = Instantiate(hexPrefab, transform, true);
                Vector2 gridPos = new Vector2(x, y);
                hex.position = CalcWorldPos(gridPos);
                hex.name = "Hex " + x + "x" + y;
                hex.GetComponent<Hex>().id = _id;
                hex.GetComponent<Node>().position = new Node.Position(x, y);

                Storage.hexes.Add(hex.GetComponent<Hex>());
                _id++;
            }
        }
    }
    
    Vector3 CalcWorldPos(Vector2 gridPos)
    {
        float x;
        if (gridPos.y % 2 != 0)
            x = _startPos.x + gridPos.x * _hexWidth + _offset;
        else
            x = _startPos.x + gridPos.x * _hexWidth ;

        float z = _startPos.z - gridPos.y * _hexHeight * 0.75f;
        
        return new Vector3(x, 0, z);
    }

    private void ConnectNeighbours()
    {
        foreach (Hex hex in Storage.hexes)
        {
            hex.GetComponent<Node>().ConnectNeighbours();
        }
    }
}