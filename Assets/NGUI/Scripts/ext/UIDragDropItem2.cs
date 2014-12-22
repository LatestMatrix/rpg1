//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2014 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;
using System.Collections;

/// <summary>
/// UIDragDropItem is a base script for your own Drag & Drop operations.
/// </summary>

public class UIDragDropItem2 : MonoBehaviour
{
	/// <summary>
	/// How long the user has to press on an item before the drag action activates.
	/// </summary>

	[HideInInspector]
	public float pressAndHoldDelay = 1f;

#region Common functionality

	protected Transform mTrans;
	protected Transform mParent;
	protected Collider mCollider;
	protected Collider2D mCollider2D;
	protected UIButton mButton;
	protected UIRoot mRoot;
	protected int mTouchID = int.MinValue;
	protected float mDragStartTime = 0f;
	protected bool mPressed = false;
	protected bool mDragging = false;

	/// <summary>
	/// Cache the transform.
	/// </summary>

	protected virtual void Start ()
	{
		mTrans = transform;
		mCollider = collider;
		mCollider2D = collider2D;
		mButton = GetComponent<UIButton>();
	}

	/// <summary>
	/// Record the time the item was pressed on.
	/// </summary>

	protected void OnPress (bool isPressed)
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

	protected virtual void Update ()
	{
	
	}

	/// <summary>
	/// Start the dragging operation.
	/// </summary>

	protected void OnDragStart ()
	{
		if (!enabled || mTouchID != int.MinValue) return;

		StartDragging();
	}

	/// <summary>
	/// Start the dragging operation.
	/// </summary>

	protected virtual void StartDragging ()
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

	protected void OnDrag (Vector2 delta)
	{
		if (!mDragging || !enabled || mTouchID != UICamera.currentTouchID) return;
		OnDragDropMove(delta * mRoot.pixelSizeAdjustment);
	}

	/// <summary>
	/// Notification sent when the drag event has ended.
	/// </summary>

	protected void OnDragEnd ()
	{
		if (!enabled || mTouchID != UICamera.currentTouchID) return;
		StopDragging(UICamera.hoveredObject);
	}

	/// <summary>
	/// Drop the dragged item.
	/// </summary>

	public void StopDragging (GameObject go)
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

	protected virtual void OnDragDropStart ()
	{
		// Disable the collider so that it doesn't intercept events
		if (mButton != null) mButton.isEnabled = false;
		else if (mCollider != null) mCollider.enabled = false;
		else if (mCollider2D != null) mCollider2D.enabled = false;

		mTouchID = UICamera.currentTouchID;
		mParent = mTrans.parent;
		mRoot = NGUITools.FindInParents<UIRoot>(mParent);

		// Re-parent the item
		if (UIDragDropRoot.root != null)
			mTrans.parent = UIDragDropRoot.root;

		Vector3 pos = mTrans.localPosition;
		pos.z = 0f;
		mTrans.localPosition = pos;

		TweenPosition tp = GetComponent<TweenPosition>();
		if (tp != null) tp.enabled = false;

		SpringPosition sp = GetComponent<SpringPosition>();
		if (sp != null) sp.enabled = false;

		// Notify the widgets that the parent has changed
		NGUITools.MarkParentAsChanged(gameObject);
	}

	/// <summary>
	/// Adjust the dragged object's position.
	/// </summary>

	protected virtual void OnDragDropMove (Vector2 delta)
	{
		mTrans.localPosition += (Vector3)delta;
	}

	/// <summary>
	/// Drop the item onto the specified object.
	/// </summary>

	protected virtual void OnDragDropRelease (GameObject surface)
	{
		mTouchID = int.MinValue;

		// Re-enable the collider
		if (mButton != null) mButton.isEnabled = true;
		else if (mCollider != null) mCollider.enabled = true;
		else if (mCollider2D != null) mCollider2D.enabled = true;

		// Is there a droppable container?
		UIDragDropContainer container = surface ? NGUITools.FindInParents<UIDragDropContainer>(surface) : null;

		if (container != null)
		{
			// Container found -- parent this object to the container
			mTrans.parent = (container.reparentTarget != null) ? container.reparentTarget : container.transform;

			Vector3 pos = mTrans.localPosition;
            pos.z = 0f;
			mTrans.localPosition = pos;
		}
		else
		{
			// No valid container under the mouse -- revert the item's parent
			mTrans.parent = mParent;
            Vector3 pos = mTrans.localPosition;
            pos.x = 0f;
            pos.y = 0f;
            pos.z = 0f;
            mTrans.localPosition = pos;
		}

		// Update the grid and table references
		mParent = mTrans.parent;

		// Notify the widgets that the parent has changed
		NGUITools.MarkParentAsChanged(gameObject);

		// We're now done
		OnDragDropEnd();
	}

	/// <summary>
	/// Function called when the object gets reparented after the drop operation finishes.
	/// </summary>

	protected virtual void OnDragDropEnd () { }

}
