using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    public Camera cam;
    public Transform followTarget;

    Vector2 startPosition;

    Vector2 camMoveSinceStart => (Vector2)cam.transform.position - startPosition;

    float startingZ;

    float parallaxFactor => Mathf.Abs(zDistanceFromTarget) / clippingPlane;

    float zDistanceFromTarget => transform.position.z - followTarget.transform.position.z;

    float clippingPlane => (cam.transform.position.z + (zDistanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane));

    private void Start()
    {
        startPosition = transform.position;
        startingZ = transform.position.z;
    }

    private void Update()
    {
        Vector2 newPosition = startPosition + camMoveSinceStart * parallaxFactor;
        transform.position = new Vector3(newPosition.x, newPosition.y, startingZ);
    }
}
