# SixthSenseMagicLeap2

Schizophrenia simulation in AR using Unity and Magic Leap One. 

This project is born at XR Brain Jam 2022. 

# Installation:
- Download Unity version 2020.3.36f1 + Install Lumin OS Module 
- Set up a developer account on magic leap 
- Download [The Lab](https://developer.magicleap.com/en-us/learn/guides/lab)
- Download All files related to Unity under The Lab (The Lab -> Package Manager -> Bundles -> Unity)
![image](./Dcoumentations/Unity%20Bundle.png)
- Get SDK path by going to The Lab - Package Manager - My Tools - Common Packages - Lumin SDK - Open Folder 
![image](./Dcoumentations/SDK%20Path.png)
- You should also have .cert and .privkey in a folder for further setup in Unity ([Check here to get your own certificate](https://developer.magicleap.com/en-us/learn/guides/developer-certificates))
- Open the project 
- You will see errors as below ![image](./Dcoumentations/errors.png)
- Import the package of MLTK from [magic leap](https://github.com/magicleap/Magic-Leap-Toolkit-Unity)
- ONLY take the .package file and import it to Unity through Unity -> Assets -> Import Package -> Custom Package. More [details on how to import package](https://docs.unity3d.com/560/Documentation/Manual/AssetPackages.html)
- Switch your platform to Lumin OS for magic leap 
![image](./Dcoumentations//LuminOSBuild.png)
- Browse your Lumin SDK path 
![image](./Dcoumentations//LuminSDKinExternalTools.png)
- Edit your certificate path (This will be when you deploy your app)
![image](./Dcoumentations//cert%20path.png)
- Reimport all through Unity -> Assets -> Reimport All. This will reimport all the dependencies in Unity and restart it 




