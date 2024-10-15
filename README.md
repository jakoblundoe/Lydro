# Lydro
*Lydro is an interactive soundscape experience utilizing the 3dof head tracking present in modern headphone devices.*

<div align="center">
    <img src="img/lydrogifv4.gif" width="92%" alt="Capturing Spaces Overview"/>
</div>

## Overview and Concept

**Lydro is an XR-based spatialized sound experience.**

*As part of a [large welfare technological research project](https://danishsoundcluster.dk/praksisnaere-loesningsmodeller-til-positiv-og-engagerede-brug-af-musik-og-lydinterventioner-paa-plejehjem/) on Aarhus University and in collaboration with the VR developer company [AATE VR](https://aatevr.com/), this interactive sound experience was created. The main focus was to research how interactive and spatial audio can emphasize the experience and the affective qualities that sound can provide.*

The design concept aimed to create an engaging and interactive XR sound experience. By incorporating head movement as an input, the listener is encouraged to actively and consciously engage with the sound, enhancing the sense of presence and thereby amplifying the affective qualities of the audio.

Utilizing the multi-sensory, enhanced experience provided by these new technologies is still a relatively new field of study. An experience in this XR medium distinguishes itself from static audio due to the kinaesthetic and auditory connection which was the main point of interest in the design of the experience.

<div align="center">
    <img src="img/lydro-img-2.jpeg" width="48%" alt="Capturing Spaces Overview"/>
    <img src="img/lydro-img-1.jpeg" width="48%" alt="Capturing Spaces Overview"/>
</div>

<br>

## How It Works
The system takes the form of an app that tracks the user's head movements (utilizing the head tracking available in most modern headphone devices). These head movements serve as inputs to the program, which then determines how to play and manipulate the program's audio output. The user's head movements control the sounds they hear.

In the UI, you can:
- Check if your headphones are connected properly to the application.
- Observe your head movements.
- Calibrate the head tracking (in case the offset is incorrect).
- Choose to activate or deactivate a narrator that guides you through the experience.
- Play, pause, and end the experience.
- Adjust master volume and narrator volume.

<br>

## Software and Dependencies
- **Unity** - Game engine.
- **Xcode** - For building to devices and notarizing the software.
- [**HeadphoneMotion unity plugin**](https://github.com/anastasiadevana/HeadphoneMotion.git) - Exposes Apple’s Headphone Motion API (CMHeadphoneManager) in Unity.
- **FMOD** - Audio middleware.
- **Reaper** - Digital audio workstation.