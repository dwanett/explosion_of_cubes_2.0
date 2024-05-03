using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    public event Action ClikedCube;
    private MaterialPropertyBlock _materialPropertyBlock;
    
    private void OnMouseUpAsButton()
    {
        ClikedCube?.Invoke();
        Destroy(gameObject);
    }
}
