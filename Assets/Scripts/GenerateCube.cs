using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class GenerateCube : MonoBehaviour
{
    [SerializeField] private Cube _controllerCube;
    [SerializeField] private float _scaleReduce;
    [SerializeField] private int _minCountGenerate;
    [SerializeField] private int _maxCountGenerate;
    [SerializeField] private float _radiusGenerate;
    public event Action<int> GeneratedCubes;
    private int _maxChanceSplit = 100;
    private int _chanceSplit;
    private int _decreaceChance = 2;
    private int _numberGeneratedCubes = 1;
    
    private void Awake()
    {
        _chanceSplit = _maxChanceSplit;
    }
    
    private void OnEnable()
    {
        _controllerCube.ClikedCube += GenerateCubes;
    }

    private void OnDisable()
    {
        _controllerCube.ClikedCube -= GenerateCubes;
    }

    public void SetChanceSplit(int newChanceSplit)
    {
        _chanceSplit = newChanceSplit;
    }
    
    public void SetNumberGeneratedCubes(int newNumberGeneratedCubes)
    {
        _numberGeneratedCubes = newNumberGeneratedCubes;
    }
    
    private void GenerateCubes()
    {
        int count = Random.Range(_minCountGenerate, _maxCountGenerate + 1);
        
        if (Random.Range(0, _maxChanceSplit + 1) < _chanceSplit)
        {
            for (int i = 0; i < count; i++)
            {
                Vector3 oldCubePosition = _controllerCube.gameObject.transform.position;
                oldCubePosition.y += _radiusGenerate;
                GameObject newCube = Instantiate(_controllerCube.gameObject, Random.insideUnitSphere * _radiusGenerate + oldCubePosition, Quaternion.identity);
                newCube.GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value);
                GenerateCube generateCube = newCube.GetComponent<GenerateCube>();
                generateCube.SetChanceSplit(_chanceSplit / _decreaceChance);
                generateCube.SetNumberGeneratedCubes(_numberGeneratedCubes + 1);
                newCube.transform.localScale /= _scaleReduce;
            }
        }

        GeneratedCubes?.Invoke(_numberGeneratedCubes);
    }
}
