# 2DAssetSpriteSorter

This project uses a tool that I have coded trying to reproduce the light tool/SpriteRenderer sorter of Odd Bug studio for their game **Tails of Iron** on Unity 2D.

   [![IMAGE ALT TEXT HERE](https://img.youtube.com/vi/0iKhPM-wrpQ/0.jpg)](https://www.youtube.com/watch?v=0iKhPM-wrpQ)

**Sweet Nightmare/Michael TPS** <https://yemabwoy.itch.io/sweet-nightmare>

    For this project I only used the asset from a 2D demo platformer used during the Unite Copenhagen keynote. 
    I however only used the sprites, I created a totally new scene without using any post processing, volumes or shaders.
    
**Used sprites :** <https://assetstore.unity.com/packages/essentials/tutorial-projects/lost-crypt-2d-sample-project-158673>

This project only uses the light tool i created (no post-processing, volumes or shaders/materials.
Here is what the result looks like and how the tool can change an entire scene mood just by modifying SpriteRenderers and the global light.


https://github.com/Sh6pa/2DAssetSpriteSorter/assets/71717569/43800e2e-842d-4e09-9242-4e7108373d99

![image_002_0000](https://github.com/Sh6pa/2DAssetSpriteSorter/assets/71717569/b94bdacb-2c9c-4d54-b741-92c7f1fa5065)
![image_004_0000](https://github.com/Sh6pa/2DAssetSpriteSorter/assets/71717569/065abe03-b883-4903-8ea6-1c196da9c671)

<hr/>

# Explanation of the system
## Basics
The 2D **level assets have a unique parent**.
2D assets are place in 3D and the camera is set to **perspective** (not orthographic) to get a natural paralax effect.
This also has the effect that you don't have to care about the scale of background objects, the distance from the perspective camera does it naturally. 

![image](https://github.com/Sh6pa/2DAssetSpriteSorter/assets/71717569/31b397e9-26af-45dc-8f79-549f61099b86)![image](https://github.com/Sh6pa/2DAssetSpriteSorter/assets/71717569/c3a343e0-4972-4a7f-89e0-4426c3df78b2)


This system allows several level segments inside a single scene without interfearing with each other but moreover give access all level SpriteRenderers throught the scene hierarchy.
The light tool has that level parent as an argument.
So I recreated the light tool that we can see in the video, here is my version :

![image](https://github.com/Sh6pa/2DAssetSpriteSorter/assets/71717569/7ce0e598-f6c8-429c-a184-ba949887f0b7)

There isn't real code shown in the video so i tried to use my knowledge.
I directly created a scriptable object called LayerData to store the most important datas :
* a list of the project's sorting layer with the OffsetValue that will determine depending the deapth
* Sorting parameters that can be helpful to setup correctly
* Background reset params so that you don't have to give the offset value by hand but just tweek the value to give a directive to the distance each layer should have between each other.
* The prefabs of different lights that can be created directly by pressing the buttons in the light tool inspector.
  
![image](https://github.com/Sh6pa/2DAssetSpriteSorter/assets/71717569/6cfead91-1fae-4b2e-8763-a236245c843e)

To access all those assets I created functions to search and return easy usable variables :

![image](https://github.com/Sh6pa/2DAssetSpriteSorter/assets/71717569/2e00432a-dd7a-495f-89c1-ec794781e58e)

(see : Assets\Scripts\Utils\Helper.cs or Assets\Scripts\Utils\AdvancedAssetSearch.cs)

The tool is updated on the list of sorting order you create, so you can choose to have even more backgrounds layers if you want even more precise/bigger/deeper levels.

![image](https://github.com/Sh6pa/2DAssetSpriteSorter/assets/71717569/a24e7320-90cf-4d7f-8573-8e5e63076990)

<hr/>

## Sorting explanation
The level object parent serves as origin for the SpriteRenderer (SR) filtering.
Then, foreach SR in the hierearchy the sorting layer list is browsed to see which offset is superior to the offset the SR has to the origin object.
This way we can give the correct sorting layer.
Then depending on how much he is close to the next layer, he gets attributed a sortingOrder so that every SR gets rendered correctly based on his position towards camera during rendering.

https://github.com/Sh6pa/2DAssetSpriteSorter/assets/71717569/6c82e8ac-397d-47a2-a962-cf6a244085d1

The Normalize Layer button sorts all asset and then change their position to the edge of their current sorting layer.
The Fade Background Color button changes the level global light color. Le global light being a 2D freeform light placed just before the foreground layer. This allows to have a different global light foreach level section and create even better light/section transition using the light falloff.
The Fade Volume Opacity for now only changes the global light intensity to what the Volume Opacity Multiplier is set.
This projet does not implement for now the Volume provided by Unity URP. However the result is really convincing by only using SR properties and Unity specificities.

<hr/>

   ## Hope you enjoyed!
![image_003_0000](https://github.com/Sh6pa/2DAssetSpriteSorter/assets/71717569/188eb1c4-67e9-4401-b558-7ddee2f0712b)
![image_001_0000](https://github.com/Sh6pa/2DAssetSpriteSorter/assets/71717569/7fa6c629-7522-40b8-9ec8-9299516641d8)


