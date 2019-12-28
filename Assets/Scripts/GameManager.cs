using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityStandardAssets.Cameras;

public class GameManager : MonoBehaviour {

    public GameObject StaticCamera;
    public GameObject FollowCamera;
    AutoCam autoCam;
    GameObject mainCam;

    public Transform[] StaticCameraNodes;
    public GameObject[] AllPlanets;

    int CameraSelection = -1;

    public GameObject CameraTarget;

    [HideInInspector]
    public int PlanetSelected = 3;


    GameObject AboutPage;

    UIManager uIManager;

	void Start () {
        AboutPage = GameObject.Find("AboutPage");
        AboutPage.SetActive(false);

        autoCam = FollowCamera.GetComponent<AutoCam>();
        mainCam = GameObject.Find("MainCamera");

        StaticCamera.transform.position = StaticCameraNodes[1].position;
        StaticCamera.transform.rotation = StaticCameraNodes[1].rotation;

        uIManager = GameObject.Find("Info Canvas").GetComponent<UIManager>();
    }
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (CameraTarget != null)
            {
                ResetCamera();
            }
        }

        //StaticCamera.transform.LookAt(GameObject.Find("Sol").transform.position);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CameraSelection++;
            if (CameraSelection >= StaticCameraNodes.Length)
            {
                CameraSelection = 0;
            }
            ResetCamera();
        }
        /*
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (PlanetSelected == 0)
            {
                PlanetSelected = AllPlanets.Length;
            }
            else
            {
                PlanetSelected--;
            }
            uIManager.PopulateUI(AllPlanets[PlanetSelected].GetComponent<OrbitOld>().PlanetData);
            ResetCamera();
            FollowPlanet(this.gameObject);
            CameraTarget = this.gameObject;
        }*/
	}

    void ResetCamera()
    {
        FollowCamera.SetActive(false);
        StaticCamera.SetActive(true);
        StaticCamera.transform.position = StaticCameraNodes[CameraSelection].position;
        StaticCamera.transform.rotation = StaticCameraNodes[CameraSelection].rotation;

        CameraTarget = null;
        uIManager.HideUI();
    }

    public void OpenAboutPage()
    {
        AboutPage.SetActive(true);
    }

    public void CloseAboutPage()
    {
        AboutPage.SetActive(false);
    }

    public void FollowPlanet(GameObject target)
    {
        StaticCamera.SetActive(false);
        FollowCamera.SetActive(true);
        autoCam.SetTarget(target.transform);
        SetCameraDistance(target);
        //target.GetComponent<SphereCollider>().
    }

    void SetCameraDistance(GameObject planet)
    {
        Vector3 pos = mainCam.transform.position;

        if (planet.transform.lossyScale.x > 1.5f)
        {
            mainCam.transform.localPosition = new Vector3(0, -0.5f, - 10);
        }
        else
        {
            mainCam.transform.localPosition = new Vector3(0, -1.5f, -0.8f);
        }
    }
}
