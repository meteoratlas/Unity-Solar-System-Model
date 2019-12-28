using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Body Data", menuName ="Custom/Body Data")]
public class BodyData : ScriptableObject {

    public string Name;
    public Classifications Classification;
    public string Radius; // in km
    public string Mass; // kg
    public string Density; // g / cm3
    public string Gravity; // earth = 1
    public string DistanceFromSun; // au
    public string OrbitalPeriod; // in earth years
    [TextArea]
    public string Description;
}

public enum Classifications
{
    Planet,
    DwarfPlanet,
    Star
}