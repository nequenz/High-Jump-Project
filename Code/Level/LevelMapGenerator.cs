using UnityEngine;

public class LevelMapGenerator : MonoBehaviour
{
    [SerializeField] private MapCube _cubeElement;
    private Vector3 _pathToGenerate;

    private void Awake()
    {
        _pathToGenerate = transform.position;
    }

    public void GeneratePath()
    {

    }

    public void GenerateMap()
    {

    }
}
