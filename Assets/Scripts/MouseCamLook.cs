using UnityEngine;

public class MouseCamLook : MonoBehaviour
{
    [SerializeField] private float sensitivity = 5.0f;
    [SerializeField] private float smoothing = 2.0f;
    public GameObject character;

    private Vector2 mouseLook;
    private Vector2 smoothV;
    private Quaternion initialRotation;

    private bool isInitialized = false;

    void Start()
    {
        character = this.transform.parent.gameObject;
    }

    void Update()
    {
        if (!isInitialized)
        {
            Initialize();
            isInitialized = true;
        }

        var md = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        md = Vector2.Scale(md, new Vector2(sensitivity * smoothing, sensitivity * smoothing));
        smoothV.x = Mathf.Lerp(smoothV.x, md.x, 1f / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, md.y, 1f / smoothing);
        mouseLook += smoothV;

        // Apply rotation to camera
        Quaternion yRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        Quaternion xRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);
        transform.localRotation = initialRotation * yRotation;
        character.transform.localRotation = xRotation;

    }

    void Initialize()
    {
        // Store the initial rotation of the camera
        initialRotation = transform.localRotation;
    }
}
