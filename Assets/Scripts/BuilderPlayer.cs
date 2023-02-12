using System.Collections.Generic;
using UnityEngine;

public class BuilderPlayer : APlayer
{
    private const string PlatformName = "Platform";

    [SerializeField] protected Vector3 platformSize;
    [SerializeField] protected Material platformMaterial;
    [SerializeField] protected Material platformSideMaterial;
    [SerializeField] protected Material platformSectionMaterial;
    [SerializeField] protected float rotationSpeed;
    [SerializeField] protected float buildingManaUsageSpeed;

    private List<Vector3> leftVertices = new List<Vector3>();
    private List<Vector3> rightVertices = new List<Vector3>();
    private List<Vector3> bottomVertices = new List<Vector3>();
    private List<Vector3> topVertices = new List<Vector3>();
    private List<Vector3> backVertices = new List<Vector3>();
    private List<Vector3> frontVertices = new List<Vector3>();

    private MeshFilter platformMeshFilter;
    private MeshCollider platformMeshCollider;
    private Vector3 oldDirection;
    private bool building;
    private Rigidbody rb;

    protected override void Awake()
    {
        base.Awake();

        rb = GetComponent<Rigidbody>();
    }

    #region Movement

    private void FixedUpdate()
    {
        if (!GameManager.Instance.GameFinished)
        {
            MovePlayer();
            RotatePlayer();
        }
    }

    protected override void MovePlayer()
    {
        rb.MovePosition(transform.position + transform.forward * direction.y * movementSpeed * Time.deltaTime);
    }

    protected override void RotatePlayer()
    {
        rb.MoveRotation(transform.rotation * Quaternion.Euler(Vector3.up * rotationSpeed * direction.x * Time.deltaTime));
    }

    protected override void Update()
    {
        base.Update();

        if (!GameManager.Instance.GameFinished && transform.position.y < -5)
        {
            SetHealth(0);
            rb.useGravity = false;
            rb.velocity = Vector3.zero;
            return;
        }

        if (Mana > 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                building = true;
                CreatePlatform();
                SetMana(Mana - buildingManaUsageSpeed * Time.deltaTime);
            }
            else if (building && direction.y >= 0 && Input.GetKey(KeyCode.Space))
            {
                UpdatePlatform();
                SetMana(Mana - buildingManaUsageSpeed * Time.deltaTime);
            }
            else
            {
                building = false;
            }
        }
    }

    #endregion Movement

    #region PlatformGeneration
    private void CreatePlatform()
    {
        Vector3 direction = transform.forward;

        leftVertices.Clear();
        rightVertices.Clear();
        bottomVertices.Clear();
        topVertices.Clear();
        backVertices.Clear();
        frontVertices.Clear();

        GameObject obj = new GameObject(PlatformName);
        obj.transform.position = transform.position - (1 + platformSize.y) * 0.5f * Vector3.up;
        platformMeshFilter = obj.AddComponent<MeshFilter>();
        MeshRenderer mr = obj.AddComponent<MeshRenderer>();
        platformMeshCollider = obj.AddComponent<MeshCollider>();

        AddPoints(false);
        AddPoints();

        GenerateMesh();

        //mr.material = platformMaterial;
        mr.materials = new Material[] { platformMaterial, platformSideMaterial, platformSectionMaterial };


        oldDirection = direction;
    }

    private void UpdatePlatform()
    {
        Vector3 direction = transform.forward;

        if (direction == oldDirection)
        {
            RemoveFrontPoints();
        }
        AddPoints();

        GenerateMesh();

        oldDirection = direction;
    }

    private void AddPoints(bool forward = true)
    {
        Vector3 basePos = transform.position - platformMeshFilter.transform.position;
        basePos.y = 0;
        Vector3 extends = platformSize * 0.5f;

        float sign = forward ? 1 : -1; 
        Vector3 leftBottom = new Vector3(-extends.x, -extends.y, sign * extends.z);
        Vector3 rightBottom = new Vector3(extends.x, -extends.y, sign * extends.z);
        Vector3 leftTop = new Vector3(-extends.x, extends.y, sign * extends.z);
        Vector3 rightTop = new Vector3(extends.x, extends.y, sign * extends.z);
        leftBottom = transform.rotation * leftBottom;
        rightBottom = transform.rotation * rightBottom;
        leftTop = transform.rotation * leftTop;
        rightTop = transform.rotation * rightTop;

        leftVertices.Add(basePos + leftBottom);
        leftVertices.Add(basePos + leftTop);

        rightVertices.Add(basePos + rightBottom);
        rightVertices.Add(basePos + rightTop);

        bottomVertices.Add(basePos + rightBottom);
        bottomVertices.Add(basePos + leftBottom);

        topVertices.Add(basePos + rightTop);
        topVertices.Add(basePos + leftTop);

        if (forward)
        {
            frontVertices.Clear();
            frontVertices.Add(basePos + leftBottom);
            frontVertices.Add(basePos + rightBottom);
            frontVertices.Add(basePos + leftTop);
            frontVertices.Add(basePos + rightTop);
        }
        else
        {
            backVertices.Clear();
            backVertices.Add(basePos + leftBottom);
            backVertices.Add(basePos + rightBottom);
            backVertices.Add(basePos + leftTop);
            backVertices.Add(basePos + rightTop);
        }
    }

    private void RemoveFrontPoints()
    {
        leftVertices.RemoveRange(leftVertices.Count - 2, 2);
        rightVertices.RemoveRange(rightVertices.Count - 2, 2);
        bottomVertices.RemoveRange(bottomVertices.Count - 2, 2);
        topVertices.RemoveRange(topVertices.Count - 2, 2);

        frontVertices.Clear();
    }

    private void GenerateMesh()
    {
        Mesh mesh = platformMeshFilter.mesh;
        if (mesh == null)
        {
            mesh = new Mesh();
        }
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();
        List<Vector3> normals = new List<Vector3>();
        List<Vector2> uv = new List<Vector2>();

        List<int> topTriangles = new List<int>();
        List<int> sideTriangles = new List<int>();
        List<int> sectionTriangles = new List<int>();


        //AddVertices(leftVertices, false, vertices, triangles, normals, uv);
        //AddVertices(rightVertices, true, vertices, triangles, normals, uv);

        //AddVertices(bottomVertices, false, vertices, triangles, normals, uv);
        //AddVertices(topVertices, true, vertices, triangles, normals, uv);

        //AddVertices(backVertices, false, vertices, triangles, normals, uv);
        //AddVertices(frontVertices, true, vertices, triangles, normals, uv);



        AddVertices(topVertices, true, vertices, topTriangles, normals, uv);

        AddVertices(leftVertices, false, vertices, sideTriangles, normals, uv);
        AddVertices(rightVertices, true, vertices, sideTriangles, normals, uv);
        AddVertices(bottomVertices, false, vertices, sideTriangles, normals, uv);

        AddVertices(backVertices, false, vertices, sectionTriangles, normals, uv);
        AddVertices(frontVertices, true, vertices, sectionTriangles, normals, uv);

        mesh.subMeshCount = 3;

        mesh.vertices = vertices.ToArray();
        mesh.SetTriangles(topTriangles.ToArray(), 0);
        mesh.SetTriangles(sideTriangles.ToArray(), 1);
        mesh.SetTriangles(sectionTriangles.ToArray(), 2);

        //mesh.vertices = vertices.ToArray();
        //mesh.triangles = triangles.ToArray();
        mesh.normals = normals.ToArray();
        mesh.uv = uv.ToArray();
        mesh.RecalculateBounds();

        platformMeshFilter.sharedMesh = mesh;
        platformMeshCollider.sharedMesh = mesh;
    }

    private void AddVertices(List<Vector3> curVertices, bool clockwise, List<Vector3> vertices, List<int> triangles, List<Vector3> normals, List<Vector2> uv)
    {
        List<int> curTriangles;
        Vector3 curNormal = Vector3.zero;
        Vector3 prevNormal;
        Vector3 resultNormal;
        float sectorLength = 0;
        for (int i = 2; i < curVertices.Count - 1; i += 2)
        {
            sectorLength += Vector3.Distance((curVertices[i] + curVertices[i + 1]) / 2f, (curVertices[i - 2] + curVertices[i - 1]) / 2f);
        }


        float curLength = 0;
        int baseIndex = vertices.Count;
        for (int i = 0; i < curVertices.Count - 3; i += 2)
        {
            int curIndex = baseIndex + i;

            curTriangles = new List<int>();

            curTriangles.Add(curIndex);
            curTriangles.Add(curIndex + 1);
            curTriangles.Add(curIndex + 2);

            curTriangles.Add(curIndex + 3);
            curTriangles.Add(curIndex + 2);
            curTriangles.Add(curIndex + 1);

            curNormal = Vector3.Cross((curVertices[i + 1] - curVertices[i]).normalized, (curVertices[i + 2] - curVertices[i]).normalized);
            if (i > 1)
            {
                prevNormal = Vector3.Cross((curVertices[i + 1] - curVertices[i]).normalized, (curVertices[i] - curVertices[i - 2]).normalized);
            }
            else
            {
                prevNormal = curNormal;
            }

            if (!clockwise)
            {
                curTriangles.Reverse();
                curNormal *= -1;
                prevNormal *= -1;
            }

            resultNormal = (curNormal + prevNormal).normalized;

            triangles.AddRange(curTriangles);

            normals.Add(resultNormal);
            normals.Add(resultNormal);

            float v = 0;
            if (sectorLength > 0)
            {
                v = curLength / sectorLength;
            }
            curLength += Vector3.Distance((curVertices[i + 2] + curVertices[i + 3]) / 2f, (curVertices[i] + curVertices[i + 1]) / 2f);
            uv.Add(new Vector2(1, v));
            uv.Add(new Vector2(0, v));
        }
        normals.Add(curNormal);
        normals.Add(curNormal);
        uv.Add(Vector2.one);
        uv.Add(Vector2.up);
        vertices.AddRange(curVertices);
    }

    #endregion PlatformGeneration

    //protected override void Update()
    //{
    //    base.Update();

    //    if (Input.GetKeyUp(KeyCode.Space))
    //    {
    //        CreateSampleMesh();
    //    }
    //}

    private void CreateSampleMesh()
    {
        Mesh cubeMesh = GetComponent<MeshFilter>().mesh;

        GameObject obj = new GameObject(PlatformName);
        obj.transform.position = transform.position + Vector3.up * 1.5f;

        MeshFilter mf = obj.AddComponent<MeshFilter>();
        MeshRenderer mr = obj.AddComponent<MeshRenderer>();

        Vector3 size = Vector3.one;
        Vector3 extends = size * 0.5f;


        Mesh mesh = new Mesh();

        Vector3[] verts = new Vector3[8];
        verts[0] = new Vector3(-extends.x, -extends.y, -extends.z);
        verts[1] = new Vector3(-extends.x, -extends.y, extends.z);
        verts[2] = new Vector3(extends.x, -extends.y, extends.z);
        verts[3] = new Vector3(extends.x, -extends.y, -extends.z);

        verts[4] = new Vector3(-extends.x, extends.y, -extends.z);
        verts[5] = new Vector3(-extends.x, extends.y, extends.z);
        verts[6] = new Vector3(extends.x, extends.y, extends.z);
        verts[7] = new Vector3(extends.x, extends.y, -extends.z);




        int[] triangles = new int[36] {
            0, 4, 7, 0, 7, 3,
            1, 6, 5, 1, 2, 6,
            0, 2, 1, 0, 3, 2,
            4, 5, 6, 4, 6, 7,
            0, 5, 4, 0, 1, 5,
            2, 7, 6, 2, 3, 7
        };


        //Vector3[] normals = new Vector3[8] { Vector3.right, Vector3.right, Vector3.right, Vector3.right, Vector3.right, Vector3.right, Vector3.right, Vector3.right };
        Vector3[] normals = new Vector3[8] { (Vector3.left + Vector3.down + Vector3.back).normalized, (Vector3.left + Vector3.down + Vector3.forward).normalized, (Vector3.right + Vector3.down + Vector3.forward).normalized, (Vector3.right + Vector3.down + Vector3.back).normalized, (Vector3.left + Vector3.up + Vector3.back).normalized, (Vector3.left + Vector3.up + Vector3.forward).normalized, (Vector3.right + Vector3.up + Vector3.forward).normalized, (Vector3.right + Vector3.up + Vector3.back).normalized };
        for (int i = 0; i < verts.Length; i++)
        {
            Transform tr = new GameObject(i.ToString()).transform;
            tr.position = verts[i] + obj.transform.position;
            tr.rotation = Quaternion.LookRotation(normals[i], Vector3.up);
        }


        Vector2[] uv = new Vector2[8] { Vector2.zero, Vector2.up, Vector2.one, Vector2.right, Vector2.zero, Vector2.up, Vector2.one, Vector2.right };




        mesh.vertices = verts;
        mesh.triangles = triangles;
        mesh.normals = normals;
        mesh.uv = uv;

        //mesh.vertices = cubeMesh.vertices;
        //mesh.triangles = cubeMesh.triangles;
        //mesh.normals = cubeMesh.normals;

        mf.mesh = mesh;
        mr.material = platformMaterial;
    }
}
