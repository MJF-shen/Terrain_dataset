# Terrain dataset
This is a **simulated** terrain dataset made by [Unity3D](https://unity.com/) used for **self-supervised monocular depth estimation**, which contains a set of monocular image sequences of Mars and Moon scenes.

The images in this dataset are taken from the **top view**, aiming to simulate the monocular image sequences of planet surface captured by aircrafts during planetary landing or exploration misson.

It contains **29** monocular image sequences for a total of **12229** images(the Mars and Moon are 7685 and 4544, respectively) for training.

Validation and test set consists of **1023** and **854** images with corresponding depth maps, respectively.

The distribution of this dataset is shown in the following table:

| | Mars | Moon |
| :------: | :------: | :------: |
|__training set__|7685|4544|
|__validation set__|620|403|
|__test set__|301|453|

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

<table>
    <tr>
        <td ><center><img src="https://github.com/MJF-shen/Terrain_dataset/blob/main/image/mars1.jpg" ></center></td>
        <td ><center><img src="https://github.com/MJF-shen/Terrain_dataset/blob/main/image/mars2.jpg" ></center></td>
        <td ><center><img src="https://github.com/MJF-shen/Terrain_dataset/blob/main/image/mars3.jpg" ></center></td>
    </tr>
</table>

- Moon

<table>
    <tr>
        <td ><center><img src="https://github.com/MJF-shen/Terrain_dataset/blob/main/image/moon1.jpg" ></center></td>
        <td ><center><img src="https://github.com/MJF-shen/Terrain_dataset/blob/main/image/moon2.jpg" ></center></td>
        <td ><center><img src="https://github.com/MJF-shen/Terrain_dataset/blob/main/image/moon3.jpg" ></center></td>
    </tr>
</table>

# How to make the dataset?
## Unity3D

This dataset is made by Unity3D, which is a popular game engine and is widely used in game development, art, design, architecture and so on.

We adopt various functions integrated in Unity3D to achieve the production of this dataset. In Unity3D, the **game** is composed of different **scenes**. Different **objects** can be created in the scene, and each object is composed of different **components**, which represent the attributes of the object.

You can create a **game camera** in Unity3D. The camera is designed to observe the whole game scene from different views. The game graphic displayed on the screen is the field of view of the camera.

Unity3D also integrates the **C# library**, which includes lots of library funcitons. You can write **C# scripts** to achieve different functions. The script must **be attached to a certain game object** to execute corresponding function.

## Implement details

In specific, we export 3D model of Mars and Moon, as shown in the figure below, to Unity3D. 

- Mars

<table>
    <tr>
        <td ><center><img src="https://github.com/MJF-shen/Terrain_dataset/blob/main/image/Mars1.png" ></center></td>
        <td ><center><img src="https://github.com/MJF-shen/Terrain_dataset/blob/main/image/Mars2.png" ></center></td>
        <td ><center><img src="https://github.com/MJF-shen/Terrain_dataset/blob/main/image/Mars3.png" ></center></td>
    </tr>
</table>

- Moon

<table>
    <tr>
        <td ><center><img src="https://github.com/MJF-shen/Terrain_dataset/blob/main/image/Moon1.png" ></center></td>
        <td ><center><img src="https://github.com/MJF-shen/Terrain_dataset/blob/main/image/Moon2.png" ></center></td>
        <td ><center><img src="https://github.com/MJF-shen/Terrain_dataset/blob/main/image/Moon3.png" ></center></td>
    </tr>
</table>

And we write C# scripts to control the motion and shot of the game camera. 

**The [scripts](https://github.com/MJF-shen/Terrain_dataset/tree/main/C%23%20script) we write is available in this project.**

For training set, the camera is controlled to do landing and orbiting motion during which a set of monocular image sequences are captured.

For validation and test set, the collision detection function in Unity3D is adopted to obtain the dense depth value corresponding to color image.

The validation and test set are well designed to include as many scenes as possible, as well as some difficult samples, such as textureless and flat area, in order to comprehensively evaluate the monocluar depth estimation result.

The color images and corresponding depth maps of test set are shown in the figure below. Brighter area in depth map means the farther away from the camera.

<table>
    <tr>
        <td ><center><img src="https://github.com/MJF-shen/Terrain_dataset/blob/main/image/color1.jpg" ></center></td>
        <td ><center><img src="https://github.com/MJF-shen/Terrain_dataset/blob/main/image/depth1.png" ></center></td>
    </tr>
    <tr>
        <td ><center><img src="https://github.com/MJF-shen/Terrain_dataset/blob/main/image/color2.jpg" ></center></td>
        <td ><center><img src="https://github.com/MJF-shen/Terrain_dataset/blob/main/image/depth2.png" ></center></td>
    </tr>
</table>

### Motion control

The camera motion control is realized by using the **transform attribute** corresponding to the **Transform component**. 

The translation component along the coordinate axis is set through ***transform.position***, while the rotation component is set through ***transform.localEulerAngles***, both of them are three-dimensional vector, each component of which represents the component of translation or rotation on each coordinate aixs.

### Color images

Many types of camera are available in Unity3D. We select the **physical camera**, as shown in the figure below.

This camera is a **pinhole camera**, and its field of view is depend on the focal length, sensor size and offset.

Through these physical parameters and the width and height of the image, the camera **intrinsic matrix K** can be calculated as follows:

$$
K = 
\left[
\begin{matrix}
f_x & 0 & c_x \\
0 & f_y & c_y \\
0 & 0 & 1 \\
\end{matrix}
\right],
f_x=f\frac{w}{s_w}, 
f_y=f\frac{h}{s_h}, 
c_x=\frac{w}{2}, 
c_y=\frac{h}{2}
$$

Here, f represents the focal length, $s_w$ and $s_h$ represent the width and height of the sensor(mm) respectively, w and h represent the width and height of the image(pixel). The offset of the camera is set to zero, so that the main point of image is exactly in the image center, thus $c_x$ and $c_y$ are equal to half of the width and height of image respectively.

<div align=center>
<img src="https://github.com/MJF-shen/Terrain_dataset/blob/main/image/camera.png">
<br>Physical camera</div>

In order to acquire current scene data, the **Texture2D class** is adopted to capture the current scene observed by game camera. Then, the data is encoded into picture format(JPG, PNG) for output, thus obtaing color images of current scene.

### Per-pixel depth groundtruth

In order to quantitatively evaluate the depth estimation results, it is necessary to obtain the per-pixel depth groundtruth.

In Unity3D, we cannot directly obtain the distance from the object to the camera plane, but the **collision detection model** provided can indirectly achieve this goal.

Emit a ray from the camera to the specified position(pixel position) on the screen, the collision detection function is adopted to obtain the world coordinate of the collision point(point on the object), which is transformed to the camera coordinate system. Thus, the Z compoment of the transformed coordinate is the distance from the point to the camera plane, that is, the depth value.

After an RGB image is taken, per-pixel depth value of it was obtained through collision detcetion.
The depth data is first saved as txt file, and then is converted to npy file and corresponding depth map through python script.
Thus, we achieve the goal of obtaining per-pixel depth groundtruth of each color image.

# Experiment results

To demonstrate the effectiveness of our dataset to train a self-supervised monocular depth estimation model, we select SC-Depth(IJCV2021) as our baseline to train a monocular depth estimation model using the proposed dataset and the resulting model is used to test on our test set.

As with previous work, we apply a single median scaling to the whole test set due to monocular scale ambiguity and compute monocular depth estimation evaluation metrics.

The qualitative and quantitative results are as follows:

<table>
    <tr>
        <td ><center><img src="https://github.com/MJF-shen/Terrain_dataset/blob/main/image/result1.png" ></center></td>
        <td ><center><img src="https://github.com/MJF-shen/Terrain_dataset/blob/main/image/result2.png" ></center></td>
    </tr>
</table>

From left to right: **RGB image**, **GroundTruth**, **Predicted**, **GT 3D point cloud**, **Predicted 3D point cloud**

|  Model   | Abs Diff| Abs Rel | Sq Rel | Log10 | RMSE  | RMSE(log) | Acc.1 | Acc.2 | Acc.3 |
|:--------:|:-------:|:-------:|:------:|:-----:|:-----:|:---------:|:-----:|:-----:|:-----:|
| SC-Depth | 2.885   | 0.016   | 0.076  | 0.007 | 3.693 | 0.020     | 1.000 | 1.000 | 1.000 |

