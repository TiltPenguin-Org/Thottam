using UnityEngine;

public class IsoCamera : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] Transform target;
    [SerializeField] float distance = 12f;
    [SerializeField] float pitch = 35f;
    [SerializeField] float yaw = 45f;

    private void LateUpdate()
    {
        if (!target) return;
        Quaternion rot = Quaternion.Euler(pitch, yaw, 0);
        Vector3 dir = rot * Vector3.forward;
        Vector3 pos = target.position - dir * distance;
        transform.position = pos;
        transform.rotation = Quaternion.LookRotation(target.position - transform.position, Vector3.up);
    }
}
