using UnityEngine;

[ExecuteAlways]
public class TransformFreeze : MonoBehaviour
{
    [SerializeField] private Vector3 position;
    [SerializeField] private Vector3 rotation;
    [SerializeField] private Vector3 scale;
    
    void Update()
    {
        transform.localPosition = position;
        transform.localEulerAngles = rotation;
        transform.localScale = scale;
    }
}
