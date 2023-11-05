using UnityEngine;

// Outside units: m, s, kg
// Inside units:  u, t, kg

static class Constants
{
    public static float G = 6.6743e-11f; // m³ kg² / s² -- Gravitation
    public static float ρ = 1; // kg / m³ -- Density
    public static float κ = 10e9f; // m / u -- Length Scale
    public static float λ = 10e5f; // m / u -- Size Scale
    public static float ζ = 1; // s / t -- Time Scale
    // public static float Ξ = G / Mathf.Pow(κ, 3) * Mathf.Pow(ζ, 2); // u³ kg² / t² -- Gravitation
}