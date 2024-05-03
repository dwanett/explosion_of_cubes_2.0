using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class GenerateCube : MonoBehaviour
{
    [SerializeField] private Cube cube;
    [SerializeField] private float _scaleReduce;
    [SerializeField] private int _minCountGenerate;
    [SerializeField] private int _maxCountGenerate;
    [SerializeField] private float _radiusGenerate;
    
    public event Action<int> GeneratedCubes;
    
    private void OnEnable()
    {
        cube.ClikedCube += GenerateCubes;
    }

    private void OnDisable()
    {
        cube.ClikedCube -= GenerateCubes;
    }
    
    private void GenerateCubes()
    {
        if (cube.TrySplitCube())
        {
            int count = Random.Range(_minCountGenerate, _maxCountGenerate + 1);
            
            for (int i = 0; i < count; i++)
            {
                Vector3 oldCubePosition = cube.transform.position;
                oldCubePosition.y += _radiusGenerate;
                Cube newCube = Instantiate(cube, Random.insideUnitSphere * _radiusGenerate + oldCubePosition, Quaternion.identity);
                
                newCube.ReplaceColor(new Color(Random.value, Random.value, Random.value));
                newCube.ReduceChanceSplit();
                newCube.ReplaceNumberGeneratedIterator(cube.NumberGeneratedIterator + 1);
                newCube.DownScaleCube(_scaleReduce);
            }
        }

        GeneratedCubes?.Invoke(cube.NumberGeneratedIterator);
    }
}
