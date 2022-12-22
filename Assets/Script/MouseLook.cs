using UnityEngine;

public class MouseLookScript : MonoBehaviour
{
    public Transform body;
    public Transform head;

    public float yawDelta;
    public float pitchDelta;

    // Update is called once per frame
    void Update()
    {
        body.Rotate(Vector3.up, Input.GetAxisRaw("Mouse X") * yawDelta);

        head.Rotate(Vector3.right, -Input.GetAxisRaw("Mouse Y") * pitchDelta);

        var headPitch = head.localRotation.eulerAngles.x;
        if (headPitch >= 180)
        {
            headPitch -= 360;
        }

        headPitch = Mathf.Clamp(headPitch, -60, 60);
        head.localRotation = Quaternion.Euler(
            headPitch,
            0f, 0f
        );
    }
}