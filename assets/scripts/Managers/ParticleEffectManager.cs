using UnityEngine;
using System.Collections;

public class ParticleEffectManager : MonoBehaviour 
{
	public static ParticleEffectManager instance;
	public Transform[] particlePrefabs;

	void Awake()
	{
		if(instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(this.gameObject);
			return;
		}
	}

	public void PlayParticle(string prefabName, Vector3 position)
	{
		for(int i = 0; i < particlePrefabs.Length; i++)
		{
			if(particlePrefabs[i].name == prefabName)
			{
				//Play Particle Effect
				Instantiate(particlePrefabs[i], position, Quaternion.identity);
				break;
			}
		}
	}
}
