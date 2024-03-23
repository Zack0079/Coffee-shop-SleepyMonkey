using UnityEngine;

public class DragAndDropSphere : MonoBehaviour
{
    private bool isDragging;
    private Vector3 mOffset;
    private float mZCoord;

    void OnMouseDown()
    {
        isDragging = true;
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseWorldPos();
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    void OnMouseUp()
    {
        isDragging = false;
    }

    void Update()
    {
        if (isDragging)
        {
            transform.position = GetMouseWorldPos() + mOffset;
        }
    }
}