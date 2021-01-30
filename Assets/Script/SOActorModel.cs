using UnityEngine; 
[CreateAssetMenu(fileName = "Create Actor", menuName = "Create Actor")]
public class SOActorModel : ScriptableObject 

{ 
    public string actorName;
    public AttackType attackType;
	public enum AttackType { enemy, player }
	public string description;
    public int health;
    public float speed;
    public int hitPower;
    public GameObject actor;
    public GameObject actorsBullets;
	public int score;
}