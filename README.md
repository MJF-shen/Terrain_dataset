# Terrain dataset
This is a **simulated** terrain dataset made by **Unity3D** used for **self-supervised monocular depth estimation**, which contains a set of monocular image sequences of Mars and Moon scenes.

This dataset aims to simulate the monocular image sequences of planet surface captured by aircrafts during planetary landing or exploration misson.

It contains **29** monocular image sequences for a total of **12229** images(the Mars and Moon are 7685 and 4544, respectively) for training.

Validation and test set consists of **1023** and **854** images with corresponding depth maps, respectively.

The dataset is organized as follows:

    terrain
      -Training
        --scene1(Mars)
          ---*.jpg(list of color(rgb) images)
          ---cam.txt(3x3 camera intrinsic matrix)
          ---depth(only in validation set)
            ----*.npy(dense depth data)
          ---depth_img(only in validation set)
            ----*.png(depth map, visulization for depth data)
        --scene2(Mars)
        ....
        --moon1(Moon)
        --moon2(Moon)
        ....
        train.txt(containing scene names for training)
        val.txt(containing scene names for validation)
      -Testing
        --mars(Mars)
          ---color
            ----*.jpg(list of color(rgb) images)
          ---depth
            ----*.npy(dense depth data)
          ---depth_img
            ----*.png(depth map, visulization for depth data)
          ---cam.txt(3x3 camera intrinsic matrix)
        --moon(Moon)
        --both(Mars and Moon)
        
Some examples of this dataset are as follows:

- Mars

![Mars1](https://github.com/MJF-shen/Terrain_dataset/blob/main/image/Mars1.png "Mars1")
![Mars2](https://github.com/MJF-shen/Terrain_dataset/blob/main/image/Mars2.png "Mars2")

- Moon

![Moon1](https://github.com/MJF-shen/Terrain_dataset/blob/main/image/Moon1.png "Moon1")
![Moon2](https://github.com/MJF-shen/Terrain_dataset/blob/main/image/Moon2.png "Moon2")

# How to make the dataset?
## Unity3D

This dataset is made by Unity3D, which is a popular game engine and is widely used in game development, art, design, architecture and so on.

We adopt various functions integrated in Unity3D to achieve the production of this dataset. In Unity3D, the game is composed of different **scenes**. Different **objects** can be created in the scene, and each object is composed of different **components**, which represent the attributes of the object.

You can create a **game camera** in Unity. The camera is designed to imitate the whole game scene observed from different views. The game picture displayed on the screen is the field of view of the camera.

Unity3D also integrates the **C# library**, which includes lots of library funcitons. You can write **C# scripts** to achieve different functions. The script must **be bound to a certain game object** to execute corresponding function.

## Process

In specific, we export 3D model of Mars and Moon to Unity3D and write C# scripts to control the movement and shot of the game camera.

### Motion control

The camera motion control is realized by using the **transform attribute** corresponding to the **Transform component**. 

The translation component along the coordinate axis is set through ***transform.position***, while the rotation component is set through ***transform.localEulerAngles***, both values are three-dimensional vector, each component of which represents the component of translation or rotation on each coordinate aixs.

### Shotting control

Many types of camera are available in Unity3D. We select the **physical camera**, as shown in the figure below.

![Moon1](https://github.com/MJF-shen/Terrain_dataset/blob/main/image/Moon1.png "Moon1")

The camera is controlled to do landing and orbiting movement during which a set of monocular image sequences are captured.

The collision detection function in Unity3D is adopted to obtain the dense depth data of current scene.

The validation and test set are well designed to include as many scenes as possible, as well as some difficult samples, such as textureless and flat area, in order to comprehensively evaluate the monocluar depth estimation result.
