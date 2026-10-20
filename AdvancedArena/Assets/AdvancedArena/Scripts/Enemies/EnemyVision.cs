using UnityEngine;

public class EnemyVision : MonoBehaviour
{
    [SerializeField] private float viewDistance = 10f;
    [SerializeField, Range(0f, 360f)]
    private float viewAngle = 120f;

    [SerializeField] private float eyeHeight = 0.5f;

    [SerializeField] private LayerMask sightMask;

    public bool CanSee(Transform target)
    {
        if (target == null)
            return false;

        Vector3 origin =
            transform.position + Vector3.up * eyeHeight;

        Vector3 targetPoint =
            target.position + Vector3.up * 0.5f;

        Vector3 direction = targetPoint - origin;

        if (direction.magnitude > viewDistance)
            return false;

        Vector3 flatDirection = direction;
        flatDirection.y = 0f;

        if (Vector3.Angle(
            transform.forward,
            flatDirection) > viewAngle / 2f)
            return false;

        if (Physics.Raycast(
            origin,
            direction.normalized,
            out RaycastHit hit,
            viewDistance,
            sightMask))
        {
            return hit.transform == target ||
                   hit.transform.IsChildOf(target);
        }

        return false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(
            transform.position,
            viewDistance);
    }
}