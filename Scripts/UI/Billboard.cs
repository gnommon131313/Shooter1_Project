using UnityEngine;

public class Billboard : MonoBehaviour
{
    // Way 1
    private void LateUpdate() => transform.rotation = Quaternion.LookRotation(Camera.main.transform.forward);

    // Way 2
    //[SerializeField] private Camera _camera;
    //private void Start() => _camera = Camera.main;
    //private void LateUpdate() => transform.LookAt(transform.position + Camera.main.transform.forward);
}
