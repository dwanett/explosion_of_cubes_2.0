
using System.Collections.Generic;
using UnityEngine;

public class ExplodeCube : MonoBehaviour
{
    [SerializeField] private GenerateCube _generateCube;
    [SerializeField] private Cube _controllerCube;
    [SerializeField] private float _forceExplode;
    [SerializeField] private float _radiusExplode;
    
    private void OnEnable()
    {
        _generateCube.GeneratedCubes += Explode;
    }

    private void OnDisable()
    {
        _generateCube.GeneratedCubes -= Explode;
    }
    
    private void Explode(int multiplierExplode)
    {
        Vector3 explosionPos = _controllerCube.gameObject.transform.position;
        float radius = _radiusExplode * multiplierExplode;
        
        List<Collider> colliders = new List<Collider>(Physics.OverlapSphere(explosionPos, radius));
        colliders.Remove(_controllerCube.gameObject.GetComponent<Collider>());
        
        foreach (Collider collider in colliders)
        {
            Rigidbody rigidbody = collider.GetComponent<Rigidbody>();
            
            if (rigidbody != null)
                rigidbody.AddExplosionForce(_forceExplode * multiplierExplode, explosionPos, radius);
        }
    }
}
