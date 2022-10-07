# Terrain_dataset
This is a simulated terrain dataset made by Unity3D used for self-supervised monocular depth estimation, which contains a set of monocular image sequences of Mars and Moon scenes. 

This dataset aims to modeling the monocular image sequences of planet's surface captured by aircrafts during plantary landing and exploration tasks.

It contains 29 monocular image sequences for a total of 12229 images(the Mars and Moon are 7685 and 4544, respectively) for training.

Validation and test set are consists of 1023 and 854 images with corresponding depth maps, respectively.

# How to make?
This dataset is made by Unity3D, which is a popular game engine and is widely used in game development, art, design, architecture and so on.

In specific, we export 3D model of Mars and Moon to Unity3D and write C sharp script to control the movement and shot of the game camera.

A set of monocular image sequences are captured during the movement of the camera.
