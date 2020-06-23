using UnityEngine;

namespace Lean.Touch
{
	/// <summary>This component allows you to translate the current GameObject relative to the camera using the finger drag gesture.</summary>
	[HelpURL(LeanTouch.HelpUrlPrefix + "LeanDragTranslate")]
	[AddComponentMenu(LeanTouch.ComponentPathPrefix + "Drag Translate")]
	public class DragDropAndPositionChecking : MonoBehaviour
	{
		/// <summary>The method used to find fingers to use with this component. See LeanFingerFilter documentation for more information.</summary>
		public LeanFingerFilter Use = new LeanFingerFilter(true);

		[SerializeField] private SpriteRenderer workstation;
    	private Vector2 inventoryPos;
    	private Vector2 lastPos;
		private bool isDragging;
		private Vector2 oldScale;
		private Quaternion oldRotation;
		
		/// <summary>The camera the translation will be calculated using.\n\nNone = MainCamera.</summary>
		[Tooltip("The camera the translation will be calculated using.\n\nNone = MainCamera.")]
		public Camera Camera;

		/// <summary>If you want this component to change smoothly over time, then this allows you to control how quick the changes reach their target value.
		/// -1 = Instantly change.
		/// 1 = Slowly change.
		/// 10 = Quickly change.</summary>
		[Tooltip("If you want this component to change smoothly over time, then this allows you to control how quick the changes reach their target value.\n\n-1 = Instantly change.\n\n1 = Slowly change.\n\n10 = Quickly change.")]
		public float Dampening = -1.0f;

		/// <summary>This allows you to control how much momenum is retained when the dragging fingers are all released.
		/// NOTE: This requires <b>Dampening</b> to be above 0.</summary>
		[Tooltip("This allows you to control how much momenum is retained when the dragging fingers are all released.\n\nNOTE: This requires <b>Dampening</b> to be above 0.")]
		[Range(0.0f, 1.0f)]
		public float Inertia;

		[HideInInspector]
		[SerializeField]
		private Vector3 remainingTranslation;

		/// <summary>If you've set Use to ManuallyAddedFingers, then you can call this method to manually add a finger.</summary>
		public void AddFinger(LeanFinger finger)
		{
			Use.AddFinger(finger);
		}

		/// <summary>If you've set Use to ManuallyAddedFingers, then you can call this method to manually remove a finger.</summary>
		public void RemoveFinger(LeanFinger finger)
		{
			Use.RemoveFinger(finger);
		}

		/// <summary>If you've set Use to ManuallyAddedFingers, then you can call this method to manually remove all fingers.</summary>
		public void RemoveAllFingers()
		{
			Use.RemoveAllFingers();
		}
#if UNITY_EDITOR
		protected virtual void Reset()
		{
			Use.UpdateRequiredSelectable(gameObject);
		}
#endif
		protected virtual void Awake()
		{
			Use.UpdateRequiredSelectable(gameObject);
			inventoryPos = transform.position;
        	lastPos = transform.position;
            GetComponent<LeanPinchScale>().enabled = false;
            GetComponent<LeanTwistRotate>().enabled = false;
            oldScale = transform.localScale;
            oldRotation = transform.rotation;
		}
		
		public void OnMouseDown()
    	{
        	isDragging = true;
    	}

    	public void OnMouseUp()
    	{
        	isDragging = false;
    	}

		protected virtual void Update()
		{
			RectTransform workstationBorders = workstation.GetComponent<RectTransform>();
			Rect rectWorkstation = new Rect(getLeftWorldCorner(workstationBorders).x, getLeftWorldCorner(workstationBorders).y, 300, 600);
			Vector2 workstationCenter = new Vector2(rectWorkstation.center.x, rectWorkstation.center.y);
			Rect rectEmoji = new Rect(-1810, -282, 600, 600);
			if(isDragging){
				// Store
				var oldPosition = transform.localPosition;

				// Get the fingers we want to use
				var fingers = Use.GetFingers();

				// Calculate the screenDelta value based on these fingers
				var screenDelta = LeanGesture.GetScreenDelta(fingers);

				if (screenDelta != Vector2.zero)
				{
					// Perform the translation
					if (transform is RectTransform)
					{
						TranslateUI(screenDelta);
					}
					else
					{
						Translate(screenDelta);
					}
				}

				// Increment
				remainingTranslation += transform.localPosition - oldPosition;

				// Get t value
				var factor = LeanTouch.GetDampenFactor(Dampening, Time.deltaTime);

				// Dampen remainingDelta
				var newRemainingTranslation = Vector3.Lerp(remainingTranslation, Vector3.zero, factor);

				// Shift this transform by the change in delta
				transform.localPosition = oldPosition + remainingTranslation - newRemainingTranslation;

				if (fingers.Count == 0 && Inertia > 0.0f && Dampening > 0.0f)
				{
					newRemainingTranslation = Vector3.Lerp(newRemainingTranslation, remainingTranslation, Inertia);
				}

				// Update remainingDelta with the dampened value
				remainingTranslation = newRemainingTranslation;


			}else if(rectOverlaps(rectWorkstation))
			{
				GetComponent<LeanPinchScale>().enabled = true;
				GetComponent<LeanTwistRotate>().enabled = true;
				transform.position = workstationCenter;
				lastPos = workstationCenter;

			}else if (rectOverlaps(rectEmoji) && lastPos==workstationCenter)
			{
				GetComponent<LeanPinchScale>().enabled = false;
				GetComponent<LeanTwistRotate>().enabled = false;
			
				Debug.Log(lastPos);
			}
			else
			{ 
				GetComponent<LeanPinchScale>().enabled = false;
				GetComponent<LeanTwistRotate>().enabled = false;
				transform.position = inventoryPos;
				lastPos = transform.position;
				transform.localScale = oldScale;
				transform.rotation = oldRotation;
			}
		}

		private void TranslateUI(Vector2 screenDelta)
		{
			var camera = Camera;

			if (camera == null)
			{
				var canvas = transform.GetComponentInParent<Canvas>();

				if (canvas != null && canvas.renderMode != RenderMode.ScreenSpaceOverlay)
				{
					camera = canvas.worldCamera;
				}
			}

			// Screen position of the transform
			var screenPoint = RectTransformUtility.WorldToScreenPoint(camera, transform.position);

			// Add the deltaPosition
			screenPoint += screenDelta;

			// Convert back to world space
			var worldPoint = default(Vector3);

			if (RectTransformUtility.ScreenPointToWorldPointInRectangle(transform.parent as RectTransform, screenPoint, camera, out worldPoint) == true)
			{
				transform.position = worldPoint;
			}

		
		}

		private void Translate(Vector2 screenDelta)
		{
			// Make sure the camera exists
			var camera = LeanTouch.GetCamera(Camera, gameObject);

			if (camera != null)
			{
				// Screen position of the transform
				var screenPoint = camera.WorldToScreenPoint(transform.position);

				// Add the deltaPosition
				screenPoint += (Vector3)screenDelta;

				// Convert back to world space
				transform.position = camera.ScreenToWorldPoint(screenPoint);
			}
			else
			{
				Debug.LogError("Failed to find camera. Either tag your camera as MainCamera, or set one in this component.", this);
			}
		}

		bool rectOverlaps(Rect rect2)
		{
			if (transform.position.x > rect2.xMin && transform.position.x < rect2.xMax)
			{
				if (transform.position.y > rect2.yMin && transform.position.y < rect2.yMax)
				{
				   return true;
				}
			}
			return false;
    
		}
		Vector2 getLeftWorldCorner(RectTransform rt)
		{
			Vector3[] v = new Vector3[4];
			rt.GetWorldCorners(v);
			return v[0];
		}
	}
}