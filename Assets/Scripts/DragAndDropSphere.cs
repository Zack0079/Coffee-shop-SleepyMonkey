using UnityEngine;

public class DragAndDropSphere : MonoBehaviour
{
    private bool isDragging;
    private Vector3 mOffset;
    private float mZCoord;
    private int draggableLayer;
    private Transform objectToDrag;

    void Start()
    {
        draggableLayer = LayerMask.NameToLayer("Draggable");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit[] hits = Physics.RaycastAll(ray);

            foreach (RaycastHit hit in hits)
            {
                if (hit.transform.gameObject.layer == draggableLayer)
                {
                    isDragging = true;
                    objectToDrag = hit.transform;
                    mZCoord = Camera.main.WorldToScreenPoint(objectToDrag.position).z;
                    mOffset = objectToDrag.position - GetMouseWorldPos();
                    break;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            objectToDrag.position = GetMouseWorldPos() + mOffset;
        }
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}