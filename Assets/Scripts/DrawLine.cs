using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;

public class DrawLine : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public GameObject CloneCenter;
    GameObject R1;
    GameObject R2;
    GameObject L1;
    GameObject L2;
    GameObject LineGO;
    public GameObject Center;
    [SerializeField] Material cubeMaterial;
    bool StartDrawing;
    bool isDraw=false;

    Vector3 MousePos;

    LineRenderer LR;

    [SerializeField]
    Material LineMat;

    int CurrentIndex;

    [SerializeField]
    Camera cam;

    [SerializeField]
    Transform Collider_Prefab;

    [SerializeField]
    Transform RBWheel;
    [SerializeField]
    Transform RFWheel;
    [SerializeField]
    Transform LBWheel;
    [SerializeField]
    Transform LFWheel;

    public Transform LastInstantiated_Collider;
    public List<Transform> Cubes = new List<Transform>();

    public float maxX, maxY, minX, minY, avgX, avgY;

    //for Love Balls

    //[SerializeField]
    //List<Rigidbody> RB = new List<Rigidbody>();

    public void OnPointerDown(PointerEventData eventData)
    {
        StartDrawing = true;
        MousePos = Input.mousePosition;

        LR = LineGO.AddComponent<LineRenderer>();

        LR.startWidth = 0f;

        LR.material = LineMat;
        Cubes.Clear();

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        StartDrawing = false;

        Rigidbody rb = LineGO.AddComponent<Rigidbody>();
        rb.useGravity = false;
        Destroy(LineGO);
        rb.constraints = RigidbodyConstraints.FreezeRotationX;
        LR.useWorldSpace = false;
        Destroy(LastInstantiated_Collider.gameObject);
        Start();
        CurrentIndex = 0;
        SortIndex();
        if(isDraw)
        {
            ClearObject();
        }
        CloneObject();
        
    }

    void Start()
    {
        LineGO = new GameObject();
    }

    void FixedUpdate()
    {
        if (StartDrawing)
        {
            Vector3 Dist = MousePos - Input.mousePosition;

            float Distance_SqrMag = Dist.sqrMagnitude;

            if (Distance_SqrMag > 1000f)
            {
                // Set this Position for our line
                
                LR.SetPosition(CurrentIndex, cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z + 10f)));
                if (LastInstantiated_Collider != null)
                {
                    Vector3 CurLinePos = LR.GetPosition(CurrentIndex);
                    LastInstantiated_Collider.gameObject.SetActive(true);

                    LastInstantiated_Collider.LookAt(CurLinePos);

                    if (LastInstantiated_Collider.rotation.y == 0)
                    {
                        //Debug.Log(LastInstantiated_Collider);
                        LastInstantiated_Collider.eulerAngles = new Vector3(LastInstantiated_Collider.rotation.eulerAngles.x, 90, LastInstantiated_Collider.rotation.eulerAngles.z);
                    }

                    //LastInstantiated_Collider.localScale = new Vector3(LastInstantiated_Collider.localScale.x, LastInstantiated_Collider.localScale.y, Vector3.Distance(LastInstantiated_Collider.position, CurLinePos) * 0.5f);
                }

                //LastInstantiated_Collider = Instantiate(Collider_Prefab, LR.GetPosition(CurrentIndex), Quaternion.identity, LineGO.transform);
                LastInstantiated_Collider = Instantiate(Collider_Prefab, LR.GetPosition(CurrentIndex), Quaternion.identity, LineGO.transform);
                Cubes.Add(LastInstantiated_Collider);
                LastInstantiated_Collider.GetComponent<Renderer>().material = cubeMaterial;
                LastInstantiated_Collider.gameObject.SetActive(false);

                MousePos = Input.mousePosition;

                CurrentIndex++;

                LR.positionCount = CurrentIndex + 1;

                LR.SetPosition(CurrentIndex, cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z + 10f)));
            }
        }
    }
    void SortIndex()
    {
        maxY = Cubes[0].transform.position.y;
        foreach(Transform childTransform in Cubes)
        {
            childTransform.transform.parent = null;
            childTransform.position = new Vector3(childTransform.position.x, childTransform.position.y, 0);
            if(childTransform.position.x>maxX)
            {
                maxX = childTransform.position.x;
            }
            if (childTransform.position.x < minX)
            {
                minX = childTransform.position.x;
            }
            if (childTransform.position.y > maxY)
            {
                maxY = childTransform.position.y;
            }
            if (childTransform.position.y < minY)
            {
                minY = childTransform.position.y;
            }
        }
        avgX = (maxX + minX)/2;
        avgY = (maxY + minY)/2;
        CloneCenter = Instantiate(Center, new Vector3(avgX, avgY, 0), Quaternion.identity);
        foreach(Transform child in Cubes)
        {
            child.transform.parent = CloneCenter.transform;
        }
    }
    void CloneObject()
    {
        isDraw = true;
        //CloneCenter.transform.rotation = Quaternion.Euler(0, 90, 0);
        //CloneCenter.transform.position = RBWheel.transform.position;
        R1 = Instantiate(CloneCenter, RBWheel.transform.position, Quaternion.Euler(0, 90, 0), RBWheel.transform);
        R2 = Instantiate(CloneCenter, RFWheel.transform.position, Quaternion.Euler(0, 90, 0), RFWheel.transform);
        L1 = Instantiate(CloneCenter, LBWheel.transform.position, Quaternion.Euler(0, 90, 0), LBWheel.transform);
        L2 = Instantiate(CloneCenter, LFWheel.transform.position, Quaternion.Euler(0, 90, 0), LFWheel.transform);
    }
    void ClearObject()
    {
        Destroy(R1);
        Destroy(R2);
        Destroy(L1);
        Destroy(L2);
        Destroy(CloneCenter);
    }
}
