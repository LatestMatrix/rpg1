//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2014 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;

/// <summary>
/// Tween the object's alpha. Works with both UI widgets as well as renderers.
/// </summary>

[AddComponentMenu("NGUI/Tween/Tween Float")]
public class TweenFloat : UITweener
{
	[Range(0f, 1f)] public float from = 1f;
	[Range(0f, 1f)] public float to = 1f;

    PropertyReference prf;

	/// <summary>
	/// Tween's current value.
	/// </summary>

	public float value
	{
		get
		{
            return (float)prf.Get();
		}
		set
		{
            prf.Set(value);
		}
	}

	/// <summary>
	/// Tween the value.
	/// </summary>

	protected override void OnUpdate (float factor, bool isFinished) { value = Mathf.Lerp(from, to, factor); }

	/// <summary>
	/// Start the tweening operation.
	/// </summary>

    static public TweenFloat Begin(GameObject go, Component com, string cName, float duration, float target)
	{
        TweenFloat comp = UITweener.Begin<TweenFloat>(go, duration);
        comp.prf = new PropertyReference();
        comp.prf.Set(com, cName);
		comp.from = comp.value;
        comp.to = target;
        

		if (duration <= 0f)
		{
			comp.Sample(1f, true);
			comp.enabled = false;
		}
		return comp;
	}

	public override void SetStartToCurrentValue () { from = value; }
	public override void SetEndToCurrentValue () { to = value; }
}
