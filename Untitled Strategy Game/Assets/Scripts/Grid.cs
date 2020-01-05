using UnityEngine;

public class Grid : MonoBehaviour
{
    public Transform hexPrefab;
    int id = 0;

    public int GridWidth { get; private set; }
    public int GridHeight { get; private set; }

    float hexWidth = 1.732f;
    float hexHeight = 2.0f;
    float gap = 0.1f;

    Vector3 startPos;

    Grid()
    {
        AddGap();
    }

    public void GenerateGrid(int width, int height)
    {
        GridWidth = width;
        GridHeight = height;
       
        CalcStartPos();
        CreateGrid();

        ConnectNeighbours();
    }

    public void ClearGrid()
    {
        foreach (Transform child in transform)
            Destroy(child.gameObject);
    }

    void AddGap()
    {
        hexWidth += hexWidth * gap;
        hexHeight += hexHeight * gap;
    }

    void CalcStartPos()
    {
        float offset = 0;
        if (GridHeight / 2 % 2 != 0)
            offset = hexWidth / 2;

        float x = -hexWidth * (GridWidth / 2) - offset;
        float z = hexHeight * 0.75f * (GridHeight / 2);

        startPos = new Vector3(x, 0, z);
    }

    Vector3 CalcWorldPos(Vector2 gridPos)
    {
        float offset = 0;
        if (gridPos.y % 2 != 0)
            offset = hexWidth / 2;

        float x = startPos.x + gridPos.x * hexWidth + offset;
        float z = startPos.z - gridPos.y * hexHeight * 0.75f;

        return new Vector3(x, 0, z);
    }

    void CreateGrid()
    {
        for (int y = 0; y < GridHeight; y++)
        {
            for (int x = 0; x < GridWidth; x++)
            {
                Transform hex = Instantiate(hexPrefab) as Transform;
                Vector2 gridPos = new Vector2(x, y);
                hex.position = CalcWorldPos(gridPos);
                hex.parent = transform;
                hex.name = "Hex " + x + "x" + y;
                hex.GetComponent<Hex>().id = id;
                hex.GetComponent<Node>().position = new Node.Position(x, y);

                Storage.hexes.Add(hex.GetComponent<Hex>());
                id++;
            }
        }
    }

    void ConnectNeighbours()
    {
        foreach (Hex hex in Storage.hexes)
        {
            Node node = hex.GetComponent<Node>();
            node.ConnectNeighbours();
        }
    }
}