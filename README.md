# 3D-in-console
A program which can create 3D objects in console.

*video:*
![Console3D](3D-in-console.gif)

## sample usage
```c#
Window win = new Window(35, 36);
World3D world = new World3D(1/*total objects*/, new Vector3(35, 36, 36)/*world size*/);

/*backgRound*/
E.empty = " ";

//create object

// "cube" , "line" , "custom"
Object o1 = new Object("cube");
o1.rotationPoint = new Vector3(10,10,10);
o1.fill = "#";
o1.sortRotate[0] = "Y";//first 
o1.sortRotate[1] = "X";//second
o1.sortRotate[2] = "Z";//third
o1.position = new Vector3(7, 8, 8);
o1.rotation = new Vector3(0, 0, 0);
o1.scale.x = 21;
o1.scale.y = 21;
o1.scale.z = 21;


world.add(o1, 0);
OrthographicCamera cam = new OrthographicCamera(world, win);
cam.position = new Vector3(0, 0, 0);

while (true)
{

    world.update();
    cam.update();
    win.show(cam);
    Thread.Sleep(50);
    System.Console.Clear();
    o1.rotation.y += 5;

}
```


## Description of classes

```c#

class window
{
  Gets 2d array from class OrthographicCamera{}.
  Prints the 2d array on screen.
}

class OrthographicCamera
{
    Pass objects and some information to class Converter{}.
    Then returns 2d array.
}

class World3D
{
    Creates a 3d array.
    Objects will be created by calling methods which are in class Object{}.
    Adds objects to 3d array.
}

class Object
{
    Creates objects and rotates them by class Math2{}.
    Then adds objects to 3d array.
}

class Converter
{
    Converts 3d array to 2d array.
    Connverter does not support perspective.
}

class Math2
{
    Rotates positions of objects one by one.
}
```

