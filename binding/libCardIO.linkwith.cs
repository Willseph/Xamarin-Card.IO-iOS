using System;
using MonoTouch.ObjCRuntime;

[assembly: LinkWith ("libCardIO.a", IsCxx=true, LinkTarget= LinkTarget.ArmV7 | LinkTarget.ArmV7s | LinkTarget.Simulator, ForceLoad = true
	,Frameworks = "AVFoundation AudioToolbox CoreMedia CoreVideo OpenGLES MobileCoreServices"
	,LinkerFlags = "-ObjC -lc++")]
