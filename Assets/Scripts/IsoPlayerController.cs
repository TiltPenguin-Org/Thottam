using Unity.Android.Gradle.Manifest;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class IsoPlayerController : MonoBehaviour
{
    [Header("Move")]
    public float moveSpeed = 5f;
    public float acceleration = 12f;

    [Header("Gravity")]
    public float gravity = -20f;

    CharacterController ctrl;
    Vector3 velocity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        ctrl = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Gathering raw input
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        input = Vector2.ClampMagnitude(input, 1f);

        // Converting to camera-relative world directions
        var cam = Camera.main;
        Vector3 camForward = cam.transform.forward; camForward.y = 0; camForward.Normalize();
        Vector3 camRight = cam.transform.right; camRight.y = 0; camRight.Normalize();

        Vector3 desired = (camForward * input.y + camRight * input.x).normalized * moveSpeed;

        // Smooth acce;erate toward desired planar velocity
        Vector3 planar = new Vector3(velocity.x, 0f, velocity.z);
        planar = Vector3.MoveTowards(planar, desired,acceleration * Time.deltaTime);
        velocity.x = planar.x;
        velocity.z = planar.z;

        // Simple gravity + ground stick
        if (ctrl.isGrounded)
        {
            velocity.y = -2f;
        }
        else
        {
            velocity.y += gravity * Time.deltaTime;
        }

        //Move
        ctrl.Move(velocity * Time.deltaTime);

        // Face move direction
        if(planar.sqrMagnitude > 0.0001f) {
            Quaternion face = Quaternion.LookRotation(planar, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, face, 12f * Time.deltaTime);
        
        }
    }
}
