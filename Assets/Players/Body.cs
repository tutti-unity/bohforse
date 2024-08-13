using System;
using UnityEngine;

public class Body : MonoBehaviour
{
    [SerializeField] private Vector2 weight = new(0f, -1f);
    [SerializeField] private float maxVelocity = 100f;
    [SerializeField] private Rect feetRect;
    [SerializeField] private LayerMask groundLayerMask;
    
    private PlayerState PlayerState => PlayerState.Instance;

    void Update()
    {
        ApplyWeight();
    }

    private void OnDrawGizmosSelected()
    {
        var isGrounded = WillBeGrounded(transform.position);
        Gizmos.color = isGrounded ? Color.green : Color.red;

        var centeredRect = feetRect;
        centeredRect.center += (Vector2)transform.position;
        GizmosUtils.DrawRect(centeredRect);
    }

    private void ApplyWeight()
    {
        var newVelocity = Vector2.ClampMagnitude(weight * Time.deltaTime + PlayerState.Velocity, maxVelocity);
        var newPosition = transform.position + (Vector3)newVelocity * Time.deltaTime;
        
        if (WillBeGrounded(newPosition))
        {
            newVelocity.y = 0f;
        }
        
        PlayerState.Velocity = newVelocity;
        transform.position += (Vector3)newVelocity * Time.deltaTime;
    }
    
    private bool WillBeGrounded(Vector3 nextPosition)
    {
        var leftStart = nextPosition - Vector3.right * feetRect.xMin;
        var rightStart = nextPosition + Vector3.right * feetRect.xMin;
        var isLeftFootGrounded = Physics2D.Linecast(leftStart, leftStart + Vector3.down * (-feetRect.y + feetRect.height / 2), groundLayerMask);
        var isRightFootGrounded = Physics2D.Linecast(rightStart, rightStart + Vector3.down * (-feetRect.y + feetRect.height / 2), groundLayerMask);
        return isLeftFootGrounded || isRightFootGrounded;
    }
}
