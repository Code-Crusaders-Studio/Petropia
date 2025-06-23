using UnityEngine;

public class OilWellsGenerator : MonoBehaviour
{
    [SerializeField] private int num_OilWells;

    private GameObject[,] obj_NodesWorld;

    public void GenerateOilWells(GameObject[,] objects)
    {
        obj_NodesWorld = new GameObject[objects.GetLength(0), objects.GetLength(1)];

        for (int x = 0; x < objects.GetLength(0); x++)
        {
            for (int y = 0; y < objects.GetLength(1); y++)
            {
                obj_NodesWorld[x, y] = objects[x, y];
            }
        }

        for (int i = 0; i < num_OilWells; i++)
        {
            int x = Random.Range(0, obj_NodesWorld.GetLength(0));
            int y = Random.Range(0, obj_NodesWorld.GetLength(1));

            obj_NodesWorld[x, y].gameObject.tag = "Oil Wells";
            Debug.Log(obj_NodesWorld[x, y].name);
        }
    }
}