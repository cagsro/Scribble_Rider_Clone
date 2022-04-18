using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;

public class DrawLine : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    GameObject LineGO;
    [SerializeField] Material cubeMaterial;
    bool StartDrawing;

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

    public Transform LastInstantiated_Collider;

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
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        StartDrawing = false;

        Rigidbody rb = LineGO.AddComponent<Rigidbody>();
        rb.useGravity = false;
        LineGO.transform.rotation = Quaternion.Euler(0, 90, 0);
        LineGO.transform.position = new Vector3(RBWheel.position.x, RBWheel.position.y, RBWheel.position.z - 10);
        


        rb.constraints = RigidbodyConstraints.FreezeRotationX;

        LR.useWorldSpace = false;

        Destroy(LastInstantiated_Collider.gameObject);

        Start();

        CurrentIndex = 0;

        //for love balls

        //foreach(Rigidbody SphereRB in RB )
        //{
        //SphereRB.useGravity = true;
        //}
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
                LastInstantiated_Collider.GetComponent<Renderer>().material = cubeMaterial;
                LastInstantiated_Collider.gameObject.SetActive(false);

                MousePos = Input.mousePosition;

                CurrentIndex++;

                LR.positionCount = CurrentIndex + 1;

                LR.SetPosition(CurrentIndex, cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z + 10f)));
            }
        }
    }
}
