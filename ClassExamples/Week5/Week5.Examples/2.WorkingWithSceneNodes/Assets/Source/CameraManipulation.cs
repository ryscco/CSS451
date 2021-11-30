using UnityEngine;
public class CameraManipulation : MonoBehaviour
{
    public Transform LookAtPosition = null;
    void Start()
    {
        Debug.Assert(LookAtPosition != null);
    }
    void Update()
    {
        // Viewing vector is from transform.localPosition to the lookat position
        Vector3 V = LookAtPosition.localPosition - transform.localPosition;
        Vector3 W = Vector3.Cross(-V, transform.up);
        Vector3 U = Vector3.Cross(W, -V);
        transform.localRotation = Quaternion.LookRotation(V, U);
        if (Input.GetKey(KeyCode.LeftAlt) && Input.GetMouseButton(1))
        {
            RotateDelta = (Input.mousePosition.x - Screen.width / 2f) * 0.001f;
        }
        else RotateDelta = 0f;
        ComputeHorizontalOrbit();
    }
    float RotateDelta = 10f / 60;  // about 10-degress per second
    float Direction = 1f;
    void ComputeHorizontalOrbit()
    {

        // orbit with respect to the transform.right axis

        // 1. Rotation of the viewing direction by right axis
        Quaternion q = Quaternion.AngleAxis(Direction * RotateDelta, transform.up);

        // 2. we need to rotate the camera position
        Matrix4x4 r = Matrix4x4.Rotate(q);
        Matrix4x4 invP = Matrix4x4.TRS(-LookAtPosition.localPosition, Quaternion.identity, Vector3.one);
        r = invP.inverse * r * invP;
        Vector3 newCameraPos = r.MultiplyPoint(transform.localPosition);
        transform.localPosition = newCameraPos;

        // transform.LookAt(LookAtPosition);
        transform.localRotation = q * transform.localRotation;

        //if (Mathf.Abs(Vector3.Dot(newCameraPos.normalized, Vector3.up)) > 0.7071f) // this is about 45-degrees
        //{
        //    Direction *= -1f;
        //}
    }

}
