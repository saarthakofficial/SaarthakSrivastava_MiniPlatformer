using UnityEngine;

[CreateAssetMenu]
public class ScriptableStats : ScriptableObject
{
    public LayerMask PlayerLayer;

    public float MaxSpeed = 14;

    public float Acceleration = 120;

    public float GroundDeceleration = 60;

    public float AirDeceleration = 30;

    [Range(0f, -10f)]
    public float GroundingForce = -1.5f;

    [Range(0f, 1f)]
    public float GrounderDistance = 0.05f;

    public float JumpPower = 36;
    public int MaxJumps = 2;
    public float MaxFallSpeed = 40;
    [Range(0f, 5f)]
    public float DashCooldown = 3f;
    public float DashSpeed = 5f;
    public float DashDuration = 0.5f;
}
