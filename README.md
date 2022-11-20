# Custom Drawers

<p align="center">
	<img alt="GitHub package.json version" src ="https://img.shields.io/github/package-json/v/Thundernerd/Unity3D-CustomDrawers" />
	<a href="https://github.com/Thundernerd/Unity3D-CustomDrawers/issues">
		<img alt="GitHub issues" src ="https://img.shields.io/github/issues/Thundernerd/Unity3D-CustomDrawers" />
	</a>
	<a href="https://github.com/Thundernerd/Unity3D-CustomDrawers/pulls">
		<img alt="GitHub pull requests" src ="https://img.shields.io/github/issues-pr/Thundernerd/Unity3D-CustomDrawers" />
	</a>
	<a href="https://github.com/Thundernerd/Unity3D-CustomDrawers/blob/main/LICENSE.md">
		<img alt="GitHub license" src ="https://img.shields.io/github/license/Thundernerd/Unity3D-CustomDrawers" />
	</a>
	<img alt="GitHub last commit" src ="https://img.shields.io/github/last-commit/Thundernerd/Unity3D-CustomDrawers" />
</p>

Custom drawers that can be used with regular objects/types instead of SerializedObjects.

## Installation
1. The package is available on the [openupm registry](https://openupm.com). You can install it via [openupm-cli](https://github.com/openupm/openupm-cli).
```
openupm add net.tnrd.customdrawers
```

2. Installing through a [Unity Package](http://package-installer.glitch.me/v1/installer/package.openupm.com/net.tnrd.customdrawers?registry=https://package.openupm.com) created by the [Package Installer Creator](https://package-installer.glitch.me) from [Needle](https://needle.tools)

[<img src="https://img.shields.io/badge/-Download-success?style=for-the-badge"/>](http://package-installer.glitch.me/v1/installer/package.openupm.com/net.tnrd.customdrawers?registry=https://package.openupm.com)

3. Installing through Package Manager - Git URL
	- Open the package manager
	- Click the plus button
	- Click `Add package from GIT url...`
	- Paste this link `https://github.com/Thundernerd/Unity3D-CustomDrawers`
	- Click `Add`

## Usage

### Getting a drawer

Below you can find a simple example on how to get and use a drawer. 

```csharp
using TNRD.CustomDrawers;
using UnityEditor;
using UnityEngine;

public class MyEditorWindow : EditorWindow
{
    private IDrawer positionDrawer;

    private void OnEnable()
    {
        positionDrawer = DrawerFactory.CreateDrawer(position);
    }

    private void OnGUI()
    {
        position = (Rect)positionDrawer.OnGUI("Position", position, false);
    }
}
```


### Registering your own drawer

You can also register your own drawers. This can be useful if you have a custom type that you would like to write a custom drawer for.

Your custom drawer needs to implement the `IDrawer` interface which can be found in the `TNRD.CustomDrawers` namespace.

To get your custom drawer into the system, you'll have to register it with the `DrawerFactory`. You can do this by calling `DrawerFactory.RegisterDrawer`.
You need to pass the type of the value the drawer is for, the type of the drawer, and if this drawer should be used for types that inherit from the value type as well.


### Overriding an existing drawer

It can happen that a drawer already exists and but you want it to be different. You can override drawers yourself, giving them precedence over the ones that are registered normally using the method above.

To override an existing drawer, first make sure you have a custom drawer. You can then call `DrawerFactory.OverrideDrawer`.
You need to pass the type of the value the drawer is for, the type of the drawer, and if this drawer should be used for types that inherit from the value type as well.


## Support
**Custom Drawers** is a small and open-source utility that I hope helps other people. It is by no means necessary but if you feel generous you can support me by donating.

[![ko-fi](https://www.ko-fi.com/img/githubbutton_sm.svg)](https://ko-fi.com/J3J11GEYY)

## Contributions
Pull requests are welcomed. Please feel free to fix any issues you find, or add new features.

