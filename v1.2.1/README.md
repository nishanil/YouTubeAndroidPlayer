# Version : 1.2.1 

Supports:

* YouTube Player version 1.2.1. 
* Target Framework: Android 4.2 (JellyBean)

Please note: YouTubeDemo is targetted for Android 5.0 (Lollipop) however it will work from Android 4.2 onwards.


YouTube Android Player for Xamarin.Android
=======================

The YouTube Android Player API enables you to incorporate video playback functionality into your Android applications. The API defines methods for loading and playing YouTube videos (and playlists) and for customizing and controlling the video playback experience.

Using the API, you can load or cue videos into a player view embedded in your application's UI. You can then control playback programmatically. For example, you can play, pause, or seek to a specific point in the currently loaded video.

You can also register event listeners to get callbacks for certain events, such as the player loading a video or the player state changing. Finally, the API has helper functionality to support orientation changes as well as transitions to fullscreen playback.

![YouTube Screen1 ](https://raw.githubusercontent.com/nishanil/YouTubeAndroidPlayer/master/ScreenShots/ScreenShot1.png)
![YouTube Screen2 ](https://raw.githubusercontent.com/nishanil/YouTubeAndroidPlayer/master/ScreenShots/ScreenShot2.png)
![YouTube Screen4 ](https://raw.githubusercontent.com/nishanil/YouTubeAndroidPlayer/master/ScreenShots/ScreenShot4.png)
![YouTube Screen4 ](https://raw.githubusercontent.com/nishanil/YouTubeAndroidPlayer/master/ScreenShots/ScreenShot5.png)
![YouTube Screen3 ](https://raw.githubusercontent.com/nishanil/YouTubeAndroidPlayer/master/ScreenShots/ScreenShot3.png)

# Samples:

### Simple PlayerView

This app shows how to use a YouTubePlayerView to play a video.

### Simple PlayerFragment

This app shows how to use a YouTubePlayerFragment to play a video.

### Custom Player Controls

This app displays several custom controls to demonstrate the use of YouTubePlayer programmatic controls. The app shows a dropdown menu that cues a video or a playlist, a play button, a pause button, and an input field that lets you specify a point to skip to in the video. It also shows an event log that lists player state changes as they happen.

### Custom Fullscreen Handling

This app demonstrates the best practice for handling fullscreen video playback. This custom fullscreen handling method is preferred because the YouTube player's default fullscreen implementation causes rebuffering of the video.

### Overlay ActionBar Demo

This app shows how you can overlay an action bar on the player when it is in fullscreen mode.

### Standalone Player

This app shows how to use a YouTubeStandalonePlayer intent to start a standalone YouTube player in a separate activity. This player can either be fullscreen or it can appear as a dialog above the current activity.

### YouTube App Launcher Intents

This app uses the static methods in the YouTubeIntents class to create Intents that navigate the user to Activities within the main YouTube application on the device.


> Read more about Samples here: https://developers.google.com/youtube/android/player/sample-applications

# Ported to C# 

This project contains YouTube Android Player API oringinally written in Java by Google which is projected to C# using [Bindings](http://docs.xamarin.com/guides/android/advanced_topics/java_integration_overview/binding_a_java_library_(.jar)/)

Solution contains two projects:

- YouTube Android Player API for Xamarin.Android. (YouTubeApi)
- Samples (again a port from Java) (YouTubeDemo)

Original Java Samples and YouTube Android Player API can be found [here](https://developers.google.com/youtube/android/player/downloads/)

## Running the project

- Make sure you get a developer key from Google: https://developers.google.com/youtube/android/player/register
- Add the Public Access API key to the class DeveloperKey.cs class
- Connect an Android device running version 4.2.16 or later of the YouTube app.

### TODO:

VideoWall Sample is still in the TODO list.

### Stuck Somewhere?
Let me know! [@nishanil](http://twitter.com/NishAnil)
