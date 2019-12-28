using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using System;

public class UIManager : MonoBehaviour {

    public Text PlanetNameText;
    public TextMeshProUGUI ClassificationText;
    public TextMeshProUGUI RadiusText;
    public TextMeshProUGUI MassText;
    public TextMeshProUGUI DensityText;
    public TextMeshProUGUI GravityText;
    public TextMeshProUGUI DistanceText;
    public TextMeshProUGUI OrbitalPeriodText;
    public TextMeshProUGUI DescriptionText;

    public GameObject PlanetNameDisplay;
    Animator planetNameAnimation;

    public GameObject DataUIContainer;
    Vector3 hiddenUIPosition;
    Vector3 originalUIPosition;

    public BodyData TEMP;

    // Use this for initialization
    void Awake () {
        planetNameAnimation = PlanetNameDisplay.GetComponent<Animator>();
        originalUIPosition = DataUIContainer.transform.localPosition;
        //hiddenUIPosition = new Vector3(DataUIContainer.transform.localPosition.x - 100, DataUIContainer.transform.localPosition.y, DataUIContainer.transform.localPosition.z);
        hiddenUIPosition = new Vector3(DataUIContainer.transform.position.x - 400, DataUIContainer.transform.position.y, DataUIContainer.transform.position.z);
        DataUIContainer.transform.position = hiddenUIPosition;
        PlanetNameDisplay.SetActive(false);

        //PopulateUI(TEMP);
    }

    public void PopulateUI(BodyData data)
    {
        PlanetNameText.text = data.Name.ToUpper();
        ClassificationText.text = data.Classification.ToString();
        RadiusText.text = data.Radius;
        MassText.text = data.Mass;
        DensityText.text = data.Density;
        GravityText.text = data.Gravity;
        DistanceText.text = data.DistanceFromSun;
        OrbitalPeriodText.text = data.OrbitalPeriod;
        DescriptionText.text = data.Description;

        DataUIContainer.transform.position = hiddenUIPosition;
        //DataUIContainer.transform.DOMove(originalUIPosition, 1.4f);
        DataUIContainer.transform.DOLocalMove(originalUIPosition, 0.4f);

        PlanetNameDisplay.SetActive(true);
        planetNameAnimation.speed = 0.6f;
        planetNameAnimation.Play("Expand");
    }

    public void HideUI()
    {
        DataUIContainer.transform.position = hiddenUIPosition;
        PlanetNameDisplay.SetActive(false);
    }
}
