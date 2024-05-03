using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Collider))]
public class Cube : MonoBehaviour
{
    [SerializeField] public Renderer _renderer;
    private int _maxChanceSplit = 100;
    private int _decreaceChance = 2;
    
    public event Action ClikedCube;
    
    [field: SerializeField] public Collider Collider { get; private set; }
    
    public int ChanceSplit { get; private set; }
    public int NumberGeneratedIterator { get; private set; }

    private void Awake()
    {
        ChanceSplit = _maxChanceSplit;
        NumberGeneratedIterator = 1;
    }
    
    private void OnMouseUpAsButton()
    {
        ClikedCube?.Invoke();
        Destroy(gameObject);
    }
    
    public void ReplaceColor(Color color)
    {
        _renderer.material.color = color;
    }
    
    public void ReduceChanceSplit()
    {
        ChanceSplit /= _decreaceChance;
    }
    
    public void ReplaceNumberGeneratedIterator(int numberGeneratedIterator)
    {
        NumberGeneratedIterator = numberGeneratedIterator;
    }
    
    public void DownScaleCube(float DownScaleIndex)
    {
        transform.localScale /= DownScaleIndex;
    }

    public bool TrySplitCube()
    {
        return Random.Range(0, _maxChanceSplit + 1) < ChanceSplit;
    }
}
