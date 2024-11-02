using UnityEngine;
using UnityEngine.Serialization;

[ExecuteInEditMode]
public class UnitConfig : ScriptableObject
{
	public string UnitName;

	public UnitType UnitType;

	[Header("Projectile")]
	public GameObject ProjectilePrefab;

	public bool IsBallisticProjectile;

	public bool ProjectileIgnoreGravity;

	public float ProjectileSpeed;

	public bool IsProjectileRotating;

	public float ProjectileRotationSpeed;

	[Header("Explosions")]
	[FormerlySerializedAs("ProjectileExplodeOnHit")]
	public bool ExplodeOnHit;

	[FormerlySerializedAs("ProjectileExplosionRadius")]
	public float ImpactExplosionRadius;

	[FormerlySerializedAs("ProjectileExplosionDamageMulti")]
	public float ImpactExplosionDamageMulti;

	[FormerlySerializedAs("ProjectileExplosionPrefab")]
	public GameObject ImpactExplosionPrefab;

	[Header("Stats")]
	public float MoveSpeed;

	public float Damage;

	public float Dps;

	public float Hp;

	public float AttackRange;

	public int FrameCount;

	public float AttackDuration;

	public bool HasLoopedAttack;

	[Header("Visuals")]
	//public AttackSfx AttackSfx;

	//public ImpactSfx ImpactSfx;

	public GameObject UnitPrefab;

	public UnitBoundingBox BoundingBox;

	private bool _showSpeed => false;

	private bool _showRotationSpeed => false;

	private void OnValidate()
	{
	}

	public void UpdateValues()
	{
	}

	private float GetBallisticAngle(Vector2 startPoint, Vector2 endPoint, float projectileSpeed)
	{
		return 0f;
	}
}
