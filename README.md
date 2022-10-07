# Terrain dataset
This is a simulated terrain dataset made by Unity3D used for self-supervised monocular depth estimation, which contains a set of monocular image sequences of Mars and Moon scenes. 

This dataset aims to modeling the monocular image sequences of planet's surface captured by aircrafts during plantary landing and exploration tasks.

It contains 29 monocular image sequences for a total of 12229 images(the Mars and Moon are 7685 and 4544, respectively) for training.

Validation and test set are consists of 1023 and 854 images with corresponding depth maps, respectively.

# How to make the dataset?
This dataset is made by Unity3D, which is a popular game engine and is widely used in game development, art, design, architecture and so on.

In specific, we export 3D model of Mars and Moon to Unity3D and write C sharp script to control the movement and shot of the game camera.

The camera is controlled to do landing and orbiting movement during which a set of monocular image sequences are captured.

The collision detection function in Unity3D is adopted to obtain the dense depth data of current scene.

The validation and test set are well designed to include as many scenes as possible, as well as some difficult samples, such as textureless and flat area, in order to comprehensively evaluate the monocluar depth estimation result.
