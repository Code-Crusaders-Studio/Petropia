using UnityEngine;

public class CameraControls : MonoBehaviour
{
    [Header("Pan Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float edgePadding = 0.1f; // Margem para evitar sair dos limites
    
    [Header("Zoom Settings")]
    [SerializeField] private float zoomSpeedTouch = 0.5f;
    [SerializeField] private float minZoom = 5f;
    [SerializeField] private float maxZoom = 20f;
    [SerializeField] private float zoomDamping = 5f;

    [Header("Camera Bounds")]
    [SerializeField] private Vector2 minBounds;
    [SerializeField] private Vector2 maxBounds;

    private Camera cam;
    private Vector3 touchStart;
    private float targetZoom;
    private bool isDragging;

    private void Awake()
    {
        cam = GetComponent<Camera>();
        targetZoom = cam.orthographicSize;
    }

    private void Update()
    {
        HandleTouchInput();
        ApplyZoom();
    }

    private void HandleTouchInput()
    {
        // Movimento com um dedo
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    touchStart = cam.ScreenToWorldPoint(touch.position);
                    isDragging = true;
                    break;

                case TouchPhase.Moved:
                    if (isDragging)
                    {
                        Vector3 direction = touchStart - cam.ScreenToWorldPoint(touch.position);
                        MoveCamera(direction);
                    }
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    isDragging = false;
                    break;
            }
        }
        // Zoom com dois dedos (movimento de pinça)
        else if (Input.touchCount == 2)
        {
            isDragging = false;

            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            Zoom(difference * zoomSpeedTouch);
        }
    }

    private void MoveCamera(Vector2 direction)
    {
        Vector2 newPosition = new Vector2(transform.position.x, transform.position.y) + direction;
        newPosition.x = Mathf.Clamp(newPosition.x, minBounds.x + edgePadding, maxBounds.x - edgePadding);
        newPosition.y = Mathf.Clamp(newPosition.y, minBounds.y + edgePadding, maxBounds.y - edgePadding);
        
        transform.position = Vector2.Lerp(transform.position, newPosition, Time.deltaTime * moveSpeed);
    }

    private void Zoom(float increment)
    {
        targetZoom = Mathf.Clamp(targetZoom - increment, minZoom, maxZoom);
    }

    private void ApplyZoom()
    {
        if (Mathf.Abs(cam.orthographicSize - targetZoom) > 0.01f)
        {
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, targetZoom, Time.deltaTime * zoomDamping);
        }
    }

    // Método para definir os limites da câmera (pode ser chamado externamente)
    public void SetCameraBounds(Vector2 min, Vector2 max)
    {
        minBounds = min;
        maxBounds = max;
    }
}