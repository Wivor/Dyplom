using UnityEngine;

public class Grid : MonoBehaviour
{
    public Transform hexPrefab;
    private int _id = 0;

    public int GridWidth { get; private set; }
    public int GridHeight { get; private set; }

    private float _hexWidth = 1.732f;
    private float _hexHeight = 2.0f;
    private const float Gap = 0.1f;

    private Vector3 _startPos;

    private Grid()
    {
        AddGap();
    }

    public void GenerateGrid(int width, int height)
    {
        ClearGrid();

        GridWidth = width;
        GridHeight = height;
       
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

    void AddGap()
    {
        _hexWidth += _hexWidth * Gap;
        _hexHeight += _hexHeight * Gap;
    }

    void CalcStartPos()
    {
        float offset = 0;
        if (GridHeight / 2 % 2 != 0)
            offset = _hexWidth / 2;

        float x = -_hexWidth * (GridWidth / 2) - offset;
        float z = _hexHeight * 0.75f * (GridHeight / 2);

        _startPos = new Vector3(x, 0, z);
    }

    Vector3 CalcWorldPos(Vector2 gridPos)
    {
        float offset = 0;
        if (gridPos.y % 2 != 0)
            offset = _hexWidth / 2;

        float x = _startPos.x + gridPos.x * _hexWidth + offset;
        float z = _startPos.z - gridPos.y * _hexHeight * 0.75f;

        return new Vector3(x, 0, z);
    }

    private void CreateGrid()
    {
        for (int y = 0; y < GridHeight; y++)
        {
            for (int x = 0; x < GridWidth; x++)
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

    void ConnectNeighbours()
    {
        foreach (Hex hex in Storage.hexes)
        {
            hex.GetComponent<Node>().ConnectNeighbours();
        }
    }
}