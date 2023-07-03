using System;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UIElements;
using static System.Collections.Specialized.BitVector32;

public class RuntimeControls : MonoBehaviour
{
	public Texture[] images;
	private ScrollView scrollview;
	private Vector2 scaledScreenSize;
	private float autoSpeed = 100;
	private float sectionHeight;

	[UsedImplicitly]
	private void OnEnable()
	{
		// The UXML is already instantiated by the UIDocument component
		UIDocument uiDocument = GetComponent<UIDocument>();
		VisualElement root = uiDocument.rootVisualElement;
		scrollview = root.Query<ScrollView>("scroll-view-wrap");
		scrollview.verticalScrollerVisibility = ScrollerVisibility.Hidden;
		scrollview.touchScrollBehavior = ScrollView.TouchScrollBehavior.Clamped;
		scrollview.mode = ScrollViewMode.Vertical;
		scrollview.scrollDecelerationRate = 0;
		PopulateVideos(scrollview);
	}

	private bool fullInit = false;
	private bool autoscroll = false;
	private int lastIndex = 1;

	[UsedImplicitly]
	private void Update()
	{
		if (!fullInit && !float.IsNaN(scrollview.verticalScroller.highValue))
		{
			scaledScreenSize = new Vector2(scrollview.contentRect.width, scrollview.contentRect.height);
			float sections = (scrollview.verticalScroller.highValue / scaledScreenSize.y);
			sectionHeight = scrollview.verticalScroller.highValue / sections;
			autoSpeed = sectionHeight;
			scrollview.scrollOffset = new Vector2(0, sectionHeight);

			fullInit = true;
		}

		if (fullInit && Input.touchCount == 0)
		{
			float a = scrollview.scrollOffset.y / sectionHeight;
			int nearestFull = Mathf.RoundToInt(a); 
			float goal = nearestFull * sectionHeight;
			int dir = (int)Mathf.Sign(goal - scrollview.scrollOffset.y);

			if (Mathf.Abs(goal - scrollview.scrollOffset.y) > 0.05f)
			{
				float dv = Mathf.Min(autoSpeed * Time.deltaTime, Mathf.Abs(goal - scrollview.scrollOffset.y)) * dir;
				
				scrollview.scrollOffset = new Vector2(0, scrollview.scrollOffset.y + dv);
				if (!autoscroll)
				{
					autoscroll = lastIndex != nearestFull;
					lastIndex = nearestFull;
				}
			}
			else if(autoscroll)
			{
				autoscroll = false;
				if (lastIndex == 0)
				{
					scrollview[scrollview.childCount - 1].SendToBack();
					scrollview.scrollOffset = new Vector2(0, scrollview.scrollOffset.y + sectionHeight);
					lastIndex++;
					LoadMore(-1);
				}
				else if(lastIndex == scrollview.childCount - 1)
				{
					scrollview[0].BringToFront();
					scrollview.scrollOffset = new Vector2(0, scrollview.scrollOffset.y - sectionHeight);
					lastIndex--;
					LoadMore(1);
				}
			}
		}
	}

	private void LoadMore(int dir)
	{
		//fetch more videos in the indicated direction
	}

	private void PopulateVideos(VisualElement root)
	{
		foreach (Texture t in images)
		{
			Image image = new Image();
			image.image = t;
			root.Add(image);
		}
	}
}
