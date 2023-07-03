# SwiprDemo

UI Toolkit swipe functionality

## Implementation

I had to learn about how VisualElement layouts worked and read a good chunk of the documentation in order to get started. Once I had a basic scroll view working with a few placeholder images I went about handling the customized scroll functionality. Knowing that it needed to snap to a given item I first worked out how tall the scroll view thought it was and how tall each item in it was, which Unity automatically scales things and the values required are sometimes inconveniently located. I used the ScrollView's content rect as that was guaranteed to fit the correct area of the screen.

A little bit of math figures out which content element is the nearest, how far away it is, and in what direction. Yadda yadda some update loop scroll position delta-v (and killing the base behavior's deceleration) and the application automatically snaps to the nearest element and stops when the target offset is reached.
 - This could be handled differently, allowing for less user scroll by jumping to the next-nearest if the scroll amount was small, however there are considerations to take into account when doing this and it is easier to handle by reading touch input directly instead of relying on the UI Element native behaviors.

After scrolling the scroll view's content is updated and moved around, along with a swift repositioning of the scroll position so that the user never actually hits the end of the scroll view.
 - In theory a user could potentially scroll more than one item at a time without triggering the auto-scroll behavior by not releasing their touches, but this edge case is not worth considering as the user would need to be particularly masochistic to push against the boundaries of the implementation. The simpler solution is to not try and fix it.
 
Characters were inserted into the demo scene from the POLYGON assets, chosing characters and positions that were interesting to look at. I'm not an artist.

Animations were assigned arbitrarily. They look delightfully silly.

## Other notes

I didn't create any fake UI for main navigation (home, profile, etc) or secondary navigation (like, comments, "dance it", etc) as these were not indicated as being required.

Stub method added to stand in for querying a server for more videos/whatever. The implementation details (like, passing a video ID) were not considered due to the placeholder nature of the content.

## Assets used

- POLYGON Dungeons - Low Poly 3D Art (Synty)
  - https://assetstore.unity.com/packages/3d/environments/dungeons/polygon-dungeons-low-poly-3d-art-by-synty-102677
- Dance Animations FREE (Kevin Iglesias)
  - https://assetstore.unity.com/packages/3d/animations/dance-animations-free-161313