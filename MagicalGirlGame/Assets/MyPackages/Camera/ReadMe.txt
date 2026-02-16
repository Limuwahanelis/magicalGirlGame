For 3D camera:
1. Set actions in Player Input for camera control
2. Use prefab in 3D folder
3. Set Transform to follow  in Player Camera.
4. Disable Player Camera Mouse if camera should not rotate.
   Otherwise set focal point around which camera should rotate.

For 2D camera:

1. Set actions in Player Input for camera control
2. use prefab in 2D folder
3. if should camera follow smth: assign transform to follow in PlayerCamera 2D
   if camera should be mvoed by mouse or other means set Move camera to true

For raycast:

1. Use prefab with selection sprite
2. Attach Camera Input handler 2D script, set values.
3. Attach raycast From mouse 2D.
4. In input handler assign Raycast and LMB action after setting "Mouse raycast" to true