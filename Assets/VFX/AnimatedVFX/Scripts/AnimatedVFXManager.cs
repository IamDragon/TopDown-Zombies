using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AnimatedVFXManager : MonoBehaviour
{
    [SerializeField] private AnimatedVFX[] animatedVFXes;
    private static AnimatedVFXManager instance;
    public List<AnimatedVFX> vfxs;

    public enum VFXType
    {
        Blood,
        SmallExplosion,
        BigExplosion
    }

    public static AnimatedVFXManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        vfxs = new List<AnimatedVFX>();
    }

    public void PlayVFX(VFXType type, Vector3 position, Quaternion rotation)
    {
        AnimatedVFX vfx = GetVFX(type);
        if (vfx == null)
        {
            Debug.Log("vfx was null");
            return;
        }
        Debug.Log("playing vfx");
        vfx.PlayAnimation();
        vfx.transform.position = position;
        vfx.transform.rotation = rotation;
    }

    private AnimatedVFX GetVFX(VFXType type)
    {
        AnimatedVFX vfx = vfxs.Find(fx => !fx.Active && fx.type == type);
        Debug.Log(type);
        if (vfx == null)
        {
            Debug.Log("Couldnt find vfx trying to create new one");
            //causes problems if the type doesnt exist
            AnimatedVFX toInstantiate = FindVFXToInstantiate(type);
            if (toInstantiate == null)
            {
                Debug.LogWarning("Trying to instantiate something that doesnt exist in the context");
                return null;
            }

            vfx = Instantiate<AnimatedVFX>(FindVFXToInstantiate(type), transform.position, transform.rotation, this.transform);
            vfxs.Add(vfx);
        }

        return vfx;
    }

    private AnimatedVFX FindVFXToInstantiate(VFXType type)
    {
        for (int i = 0; i < animatedVFXes.Length; i++)
        {
            if (animatedVFXes[i].type == type)
                return animatedVFXes[i];
        }
        return null;
    }
}
