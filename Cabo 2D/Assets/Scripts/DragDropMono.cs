using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDropMono : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Canvas;
    private bool isDragging = false;
    private bool isDraggable = true; //grants authority for those who pick up card to move it
    private bool isOverDropZone;
    private GameObject startParent;
    private GameObject dropZone;
    private Vector2 startPos;

    // Update is called once per frame

     void Start()
    {
        Canvas = GameObject.Find("TestCanvas"); 
        this.transform.localScale = Vector2.one;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //  Debug.Log("Colliding!");
        isOverDropZone = true;
        dropZone = collision.gameObject;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Debug.Log("UnColliding!");
        isOverDropZone = false;
        dropZone = null;
    }

    public void StartDrag()
    {
        if (!isDraggable) return;
        isDragging = true;
        startParent = transform.parent.gameObject;
        startPos = transform.position;
    }

    public void StopDrag()
    {

        if (!isDraggable) return;
        isDragging = false;

        if (isOverDropZone)
        {
            transform.SetParent(dropZone.transform, false);

            isDraggable = false;

        }
        else
        {
            transform.position = startPos;
            transform.SetParent(startParent.transform, false);
        }
    }



    void Update()
    {
        if (isDragging)
        {
            transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            transform.SetParent(Canvas.transform, true);
        }
    }

}
