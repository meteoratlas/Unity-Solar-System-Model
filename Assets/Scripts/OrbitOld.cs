using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(MeshRenderer))]
public class OrbitOld : MonoBehaviour {

    // Calculates an orbit to a specified resolution and passes those values to rendering objects
    [Range(6, 50)]
    public int Resolution = 12;
    public Vector2 Axis = new Vector2(2f, 2f);
    public BodyData PlanetData;

    public bool CameraTarget;

    private LineRenderer lineRenderer;

    private Orbit orbit;// = new Orbit(4, 4);
    public GameObject blah;

    SphereCollider sphereCollider;

    private Vector3[] points;
    //private bool tweenActive;
    //float tweenVal = 0f;
    float rotationSpeed = 17.6f;

    readonly int outlineExtrude = Shader.PropertyToID("_OutlineExtrusion");
    Material material;

    float orbitProgress;
    public float orbitPeriod = 19f;
    bool orbitActive = true;

    private Color HighliteColour = new Color(1, 1, 0);
    readonly int _ColourRef = Shader.PropertyToID("_Color");

    UIManager uIManager;
    GameManager gameManager;

    AudioSource audioSource;

    public int Index;

    // Use this for initialization
    void Awake () {

        gameManager = GameObject.Find("Manager").GetComponent<GameManager>();
        uIManager = GameObject.Find("Info Canvas").GetComponent<UIManager>();
        audioSource = gameManager.GetComponent<AudioSource>();
        sphereCollider = GetComponent<SphereCollider> ();
        DOTween.Init();
        orbit = new Orbit(Axis.x, Axis.y);
        lineRenderer = GetComponent<LineRenderer>();
        material = GetComponent<MeshRenderer>().material;
        CalculateOrbit();
        DrawOrbitLine();

        orbitProgress = Random.Range(0f, 1f);

        //ZoomInOnPlanet();
        //
        SetOrbitingPosition();
        StartCoroutine(Run());
	}

    void CalculateOrbit()
    {
        points = new Vector3[Resolution + 1];

        for (int i = 0; i < points.Length; i++)
        {
            float angle = ((float)i / (float)Resolution) * 360 * Mathf.Deg2Rad;
            float x = Mathf.Sin(angle) * Axis.x;
            float y = Mathf.Cos(angle) * Axis.y;
            points[i] = new Vector3(x, 0, y);
        }

        points[Resolution] = points[0];
    }

    void Update()
    {

        transform.Rotate(0, Time.deltaTime * rotationSpeed, 0);

        
    }

    IEnumerator Run()
    {
        if (orbitPeriod < 0.1f)
            orbitPeriod = 0.1f;

        float orbitSpeed = 1f / orbitPeriod;

        while (orbitActive)
        {
            orbitProgress += Time.deltaTime * orbitSpeed;
            orbitProgress %= 1f;
            SetOrbitingPosition();


            //Debug.Log("egg");
            yield return null;
        }
    }

    void DrawOrbitLine()
    {
        lineRenderer.positionCount = Resolution + 1;
        lineRenderer.SetPositions(points);
    }

    void SetOrbitingPosition()
    {
        Vector2 pos = orbit.Evaluate(orbitProgress);
        transform.position = new Vector3(pos.x, 0, pos.y);
    }

    
    void OnMouseOver()
    {
        if (gameManager.CameraTarget == this.gameObject)
        {
            sphereCollider.enabled = false;
            return;
        }
        else
        {
            sphereCollider.enabled = true;
        }

        material.SetColor(_ColourRef, HighliteColour);

        if (Input.GetMouseButtonDown(0))
        {
            uIManager.PopulateUI(PlanetData);
            gameManager.FollowPlanet(this.gameObject);
            gameManager.CameraTarget = this.gameObject;
            gameManager.PlanetSelected = this.Index;
            audioSource.Play();
            sphereCollider.enabled = true;
        }
        /*
        if (!tweenActive)
        {
            tweenActive = true;
            DOTween.To(()=> tweenVal, x => tweenVal = x, 0.3f, 0.3f);
        }
        */
    }

    void OnMouseExit()
    {
        material.SetColor(_ColourRef, Color.white);
        sphereCollider.enabled = true;
        /*
        if (tweenActive)
        {
            tweenActive = false;
            DOTween.To(() => tweenVal, x => tweenVal = x, 0.0f, 0.15f);
        }
        */
    }
    
    void ZoomInOnPlanet()
    {
        if (!CameraTarget)
            return;
        var trans = GameObject.Find("CameraZoomPoint").transform;
        Camera.main.transform.DOMove(trans.position, 0.5f).OnComplete(ZoomComplete);

        Camera.main.transform.parent = trans;
        Camera.main.transform.position = Vector3.zero;
        //Camera.main.transform.LookAt(GameObject.Find("Sphere").transform, Vector3.up);

    }

    void ZoomComplete()
    {
        //Camera.main.transform.DOLookAt(GameObject.Find("Sphere").transform, 0.2f);
        // Camera.main.transform.LookAt(GameObject.Find("Sphere").transform, Vector3.up);

        // ?
        //Transform target = GameObject.Find("blah").transform;
        //Camera.main.transform.DOLookAt(target.position, 0.2f);
    }

}
