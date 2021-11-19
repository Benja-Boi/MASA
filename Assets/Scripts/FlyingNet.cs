using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingNet : MonoBehaviour
{
    //List<FlyingNetCorner> corners = new List<FlyingNetCorner>();
    public FlyingNetCorner frontRightCorner;
    public FlyingNetCorner frontLeftCorner;
    public FlyingNetCorner backRightCorner;
    public FlyingNetCorner backLeftCorner;


    bool isShot = false;
    public bool IsShot { get { return isShot; } set { isShot = value; } }

    public float airTime = 0.5f;
    public float minSpeed = 0.5f;
    public float speed;
    public float spread;
    public float captureTime = 3f;

    float timeShotFired;
    float frontDrag;
    float backDrag;
    bool netLaid = false;
    Mesh mesh;


    void Start()
    {
        timeShotFired = Time.time;
        backDrag = 1f;
        frontDrag = 1f;
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    void Update()
    {
        if (isShot && frontDrag * speed >= minSpeed)
        {
            MoveCorners();
            CalcDrag();
            if (frontDrag * speed < minSpeed)
            {
                LayNet();
                netLaid = true;
                StartCoroutine(DestroyNet());
            }
        }
    }

    IEnumerator DestroyNet()
    {
        yield return new WaitForSeconds(captureTime);
        frontRightCorner.DestroyCorner();
        frontLeftCorner.DestroyCorner();
        backRightCorner.DestroyCorner();
        backLeftCorner.DestroyCorner();
        Destroy(this.gameObject);
    }

    void RotateCorners()
    {
        float timeInAir = 50f * (Time.time - timeShotFired);
        frontRightCorner.transform.rotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, 0, (1.5f * spread * timeInAir * Time.deltaTime) / airTime));
        frontLeftCorner.transform.rotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, 0, -(1.5f * spread * timeInAir * Time.deltaTime) / airTime));
        backRightCorner.transform.rotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, 0, (spread * timeInAir * Time.deltaTime) / airTime));
        backLeftCorner.transform.rotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, 0, -(spread* timeInAir * Time.deltaTime) / airTime));
    }

    void MoveCorners()
    {
        RotateCorners();
        frontRightCorner.transform.Translate(Vector2.up * speed * Time.deltaTime * frontDrag);
        frontLeftCorner.transform.Translate(Vector2.up * speed * Time.deltaTime * frontDrag);
        backRightCorner.transform.Translate(Vector2.up * speed * Time.deltaTime * backDrag);
        backLeftCorner.transform.Translate(Vector2.up * speed * Time.deltaTime * backDrag);
        LayNet();
    }

    void CalcDrag()
    {
        float t = Time.time - timeShotFired;
        backDrag = Mathf.Max(BackDragFunc(t, .1f), 0);
        frontDrag = Mathf.Max(FrontDragFunc(t), 0);
    }

    float BackDragFunc(float x, float backCornersDelay = 0.25f)
    {
        float at = airTime * 1.2f;
        return (-Mathf.Pow((x - backCornersDelay), 2) / at) + at;
    }

    float FrontDragFunc(float x)
    {
        return (-Mathf.Pow((x), 2) / airTime) + airTime;
    }

    void LayNet()
    {
        PolygonCollider2D collider = GetComponent<PolygonCollider2D>();
        if (collider == null)
        {
            collider = this.gameObject.AddComponent<PolygonCollider2D>();
        }
        Vector2[] cornerPoints = new Vector2[4];
        Vector3[] meshPoints = new Vector3[4];
        meshPoints[0] = (Vector2)frontRightCorner.transform.localPosition;
        meshPoints[1] = (Vector2)frontLeftCorner.transform.localPosition;
        meshPoints[3] = (Vector2)backRightCorner.transform.localPosition;
        meshPoints[2] = (Vector2)backLeftCorner.transform.localPosition; // Indices 2 and 3 swapped to create correct order of points in the trapezoid.

        for (int i = 0; i < 4; i++)
            cornerPoints[i] = (Vector2)meshPoints[i];

        collider.SetPath(0, cornerPoints);
        UpdateMesh(meshPoints); 
    }

    void UpdateMesh(Vector3[] vertices)
    {
        int[] triangles = new int[]{
            1, 0, 3,
            1, 3, 2
        };

        mesh.vertices = vertices;
        mesh.triangles = triangles;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Animal"))
        {
            Rbit rbit = collision.GetComponent<Rbit>();
            if (GetComponent<Collider2D>().bounds.Contains(rbit.transform.position) && !netLaid)
            {
                rbit.Tranquilize(captureTime);
            }
        }
    }
}