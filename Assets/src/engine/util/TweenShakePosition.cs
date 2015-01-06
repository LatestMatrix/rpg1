//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2014 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;

/// <summary>
/// Tween the object's position.
/// </summary>

[AddComponentMenu("NGUI/Tween/Tween shake Position")]
public class TweenShakePosition : UITweener
{

    public Vector3 range = new Vector3(0.5f, 0.5f, 0.5f);
    private Vector3 pos = Vector3.zero;

	[System.Obsolete("Use 'value' instead")]
	public Vector3 position { get { return this.value; } set { this.value = value; } }

	/// <summary>
	/// Tween's current value.
	/// </summary>

	public Vector3 value
	{
		get
		{
            return pos;
		}
        set
        {
            pos = value;
        }
	}

    override protected void Start()
    {
        animationCurve = new AnimationCurve(new Keyframe(0f, 0f, 0f, 1f), new Keyframe(0.5f, 0.5f, 5.67f, 5.67f), new Keyframe(1f, 1f, 1f, 0f));
        base.Start();
    }

	/// <summary>
	/// Tween the value.
	/// </summary>

    protected override void OnUpdate(float factor, bool isFinished) { value = range * (factor * 2 - 1f); }

	/// <summary>
	/// Start the tweening operation.
	/// </summary>

    static public TweenShakePosition Begin(GameObject go, float duration, Vector3 range)
	{
        TweenShakePosition comp = UITweener.Begin<TweenShakePosition>(go, duration);
        comp.range = range;

		if (duration <= 0f)
		{
			comp.Sample(1f, true);
			comp.enabled = false;
		}
        comp.style = UITweener.Style.PingPong;
        comp.method = UITweener.Method.BounceInOut;
		return comp;
	}

	[ContextMenu("Set 'From' to current value")]
    public override void SetStartToCurrentValue() { value = Vector3.zero; }

	[ContextMenu("Set 'To' to current value")]
    public override void SetEndToCurrentValue() { value = Vector3.zero; }

	[ContextMenu("Assume value of 'From'")]
    void SetCurrentValueToStart() { value = Vector3.zero; }

	[ContextMenu("Assume value of 'To'")]
    void SetCurrentValueToEnd() { value = Vector3.zero; }
}
