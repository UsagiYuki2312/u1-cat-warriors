using UnityEngine;

public class AgeConfig : ScriptableObject
{
	[Header("Age")]
	public string Name;

	public string TimeText;

	[Multiline(6)]
	public string Description;

	public Sprite Icon;

	[Header("Units")]
	public UnitConfig[] Units;

	[Header("Unit Visuals")]
	public Color PlayerUnitColor;

	public Color EnemyUnitColor;

	public Color EnemyWeaponColor;

	[Header("Visuals")]
	public GameObject MapPrefab;

	public GameObject BasePrefab;

	public BiomeSfx BiomeSfx;
}
