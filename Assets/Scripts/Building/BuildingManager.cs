using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{


    public float tileSize;

    public Camera camera;

    [SerializeField]
    GameObject objectToBuild;

    GameObject objectPosPreview;

    [SerializeField]
    Material goodPosMaterial;
    [SerializeField]
    Material badPosMaterial;


    private void Start()
    {
        objectPosPreview = Instantiate(objectToBuild);
        objectPosPreview.name = "objectPosPreview";
        objectPosPreview.GetComponent<Collider>().enabled = false;
    }


    // Update is called once per frame
    void Update()
    {

        PosPreview();

       



    }

    private void PosPreview()
    {
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {


            if (hit.collider.tag == "Ground")
            {
                Vector3 hitPoint = hit.point;

                Vector3 buildingPosition;

                buildingPosition.x = Mathf.Floor(hitPoint.x / tileSize);
                buildingPosition.y = Mathf.Floor(hitPoint.y / tileSize);
                buildingPosition.z = Mathf.Floor(hitPoint.z / tileSize);

                objectPosPreview.transform.position = buildingPosition;

                if(hit.collider.tag != "Building")
                {
                    objectPosPreview.GetComponentInChildren<MeshRenderer>().material = goodPosMaterial;

                    if (Input.GetMouseButtonDown(0))
                    {

                        BuildObject(buildingPosition);

                    }

                }
                else
                {
                    objectPosPreview.GetComponent<MeshRenderer>().material = badPosMaterial;

                }
                
            }
        }
    }

    void BuildObject(Vector3 buildingPosition)
    {


        GameObject instanceObjectToBuild = Instantiate(objectToBuild, buildingPosition, Quaternion.identity);
        
    }

    void setObjectToBuild(GameObject objectToBuild)
    {
        this.objectToBuild = objectToBuild;
    }
}
