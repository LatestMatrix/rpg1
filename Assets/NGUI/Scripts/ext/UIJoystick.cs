//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2014 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;
using System.Collections;

/// <summary>
/// UIJoystick
/// </summary>

public class UIJoystick : MonoBehaviour
{
    /// <summary>
    /// How long the user has to press on an item before the drag action activates.
    /// </summary>

    public delegate void EventHandler(Vector2 dir);

    [HideInInspector]
    public float pressAndHoldDelay = 1f;

    public int range = 50;
    public float updateFrame = 0.2f;
    public event EventHandler dragMove = null;

    #region Common functionality

    protected Transform mTrans;
    protected Collider mCollider;
    protected Collider2D mCollider2D;
    protected UIRoot mRoot;
    protected int mTouchID = int.MinValue;
    protected float mDragStartTime = 0f;
    protected bool mPressed = false;
    protected bool mDragging = false;
    private Vector2 local_pos;
    private float passTime = 0f;

    /// <summary>
    /// Cache the transform.
    /// </summary>

    protected virtual void Start()
    {
        mTrans = transform;
        mCollider = collider;
        mCollider2D = collider2D;
        local_pos = new Vector2();
    }

    /// <summary>
    /// Record the time the item was pressed on.
    /// </summary>

    protected void OnPress(bool isPressed)
    {
        if (isPressed)
        {
            mDragStartTime = RealTime.time + pressAndHoldDelay;
            mPressed = true;
        }
        else mPressed = false;
    }

    /// <summary>
    /// Start the dragging operation after the item was held for a while.
    /// </summary>

    protected virtual void Update()
    {
        passTime += Time.deltaTime;
        if (passTime > updateFrame)
        {
            passTime -= updateFrame;
            if (dragMove != null)
            {
                if (local_pos.sqrMagnitude > 0)
                {
                    dragMove(local_pos.normalized);
                }
                else
                {
                    dragMove(Vector2.zero);
                }
            }
        }
    }

    /// <summary>
    /// Start the dragging operation.
    /// </summary>

    protected void OnDragStart()
    {
        if (!enabled || mTouchID != int.MinValue) return;

        StartDragging();
    }

    /// <summary>
    /// Start the dragging operation.
    /// </summary>

    protected virtual void StartDragging()
    {
        if (!mDragging)
        {
            mDragging = true;
            OnDragDropStart();
        }
    }

    /// <summary>
    /// Perform the dragging.
    /// </summary>

    protected void OnDrag(Vector2 delta)
    {
        if (!mDragging || !enabled || mTouchID != UICamera.currentTouchID) return;
        OnDragDropMove(delta * mRoot.pixelSizeAdjustment);
    }

    /// <summary>
    /// Notification sent when the drag event has ended.
    /// </summary>

    protected void OnDragEnd()
    {
        if (!enabled || mTouchID != UICamera.currentTouchID) return;
        StopDragging(UICamera.hoveredObject);
    }

    /// <summary>
    /// Drop the dragged item.
    /// </summary>

    public void StopDragging(GameObject go)
    {
        if (mDragging)
        {
            mDragging = false;
            OnDragDropRelease(go);
        }
    }

    #endregion

    /// <summary>
    /// Perform any logic related to starting the drag & drop operation.
    /// </summary>

    protected virtual void OnDragDropStart()
    {
        // Disable the collider so that it doesn't intercept events
        if (mCollider != null) mCollider.enabled = false;
        else if (mCollider2D != null) mCollider2D.enabled = false;

        mTouchID = UICamera.currentTouchID;
        mRoot = NGUITools.FindInParents<UIRoot>(mTrans.parent);

        mTrans.localPosition = Vector3.zero;
    }

    /// <summary>
    /// Adjust the dragged object's position.
    /// </summary>

    protected virtual void OnDragDropMove(Vector2 delta)
    {
        local_pos += delta;
        Vector3 pos = (Vector3)local_pos;
        if (pos.magnitude > range)
        {
            pos *= (range / pos.magnitude);
        }
        mTrans.localPosition = pos;
    }

    /// <summary>
    /// Drop the item onto the specified object.
    /// </summary>

    protected virtual void OnDragDropRelease(GameObject surface)
    {
        mTouchID = int.MinValue;

        // Re-enable the collider
        if (mCollider != null) mCollider.enabled = true;
        else if (mCollider2D != null) mCollider2D.enabled = true;

        mTrans.localPosition = Vector3.zero;
        local_pos = Vector2.zero;

        // Notify the widgets that the parent has changed
        NGUITools.MarkParentAsChanged(gameObject);

        // We're now done
        OnDragDropEnd();
    }

    /// <summary>
    /// Function called when the object gets reparented after the drop operation finishes.
    /// </summary>

    protected virtual void OnDragDropEnd() { }

}
