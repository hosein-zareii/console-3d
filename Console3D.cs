using System;


// in the name of god 
// created by hosein zarei

public class Console3D
{

    public static void Main(String[] args)
    {
        Console.Title = "3D in console";
        Console.ForegroundColor = ConsoleColor.Green;
        Console.CursorVisible = false;
        Console.BufferHeight = 35;
        Console.WindowHeight = 35;

        // (window & world)***
        Window win = new Window(35, 36);
        World3D world = new World3D(1/*total objects*/, new Vector3(35, 36, 36)/*world size*/);
        

        /*backgRound*/
        E.empty = " ";



        //(objects)***

        // "cube" , "line" , "custom"
        Object o1 = new Object("cube");
        o1.rotationPoint = new Vector3(10,10,10);
        o1.fill = "#";
        //(sortRotate) 
        o1.sortRotate[0] = "Y";//first 
        o1.sortRotate[1] = "X";//second
        o1.sortRotate[2] = "Z";//third
        o1.position = new Vector3(7, 8, 8);
        o1.rotation = new Vector3(0, 0, 0);
        o1.scale.x = 21;
        o1.scale.y = 21;
        o1.scale.z = 21;
        

        /*o1.customPoints = new Vector3[1];
        o1.customPoints[0] = new Vector3(0, 0, 0);*/



        //(add objects to world)***

        world.add(o1, 0);


        //(Orthographic Camera)***

        OrthographicCamera cam = new OrthographicCamera(world, win);
        cam.position = new Vector3(0, 0, 0);



        //(show in window)***
        

        while (true)
        {

            world.update();
            cam.update();
            win.show(cam);
            

            try
            {
                //Thread.Sleep(50);
                //System.Console.Clear();
                //o1.rotation.y += 5;
                //o1.rotation.x += 5;
                
            }
            catch (Exception e) { Console.WriteLine("error line 79  " + e.Message); }

            switch ( Console.ReadKey().Key )
            {
                case ConsoleKey.LeftArrow:
                    //System.Console.Clear();
                    o1.rotation.y += 5;
                break;
                case ConsoleKey.RightArrow:
                    //System.Console.Clear();
                    o1.rotation.y -= 5;
                break;

                case ConsoleKey.UpArrow:
                    //System.Console.Clear();
                    o1.rotation.x -= 5;
                    break;
                case ConsoleKey.DownArrow:
                    //System.Console.Clear();
                    o1.rotation.x += 5;
                break;

                case ConsoleKey.D:
                    //System.Console.Clear();
                    o1.position.x += 1;
                break;
                case ConsoleKey.A:
                    //System.Console.Clear();
                    o1.position.x -= 1;
                break;
            }

        }

    }

    
}



// version 1.4


class Window
{
    public String[,] window;
    public int x;
    public int y;
    public Window(int x, int y)
    {
        this.x = x;
        this.y = y;
        window = new String[y,x];

    }



    public void show(OrthographicCamera cam)
    {
        int lx = window.GetLength(1);
        int ly = window.GetLength(0);

        int ix = 0;
        int iy = 0;
        string[] wa = new string[(lx*ly)+ly];
        int i = 0;

        while (iy < ly)
        {
        
            while (ix < lx)
            {
                try
                {
                    if(cam.window[iy,ix] == null)
                    {
                        if(ix+1 == lx)
                        {
                            wa[i] = E.empty + " \n";
                        }else
                        {
                            wa[i] = E.empty + " ";
                        }
                    }else
                    {
                        if (ix + 1 == lx)
                        {
                            wa[i] = cam.window[iy, ix] + " \n";
                        }
                        else
                        {
                            wa[i] = cam.window[iy, ix] + " ";
                        }
                    }
                    
                }
                catch (Exception e) { Console.WriteLine("error line 121  " + e.Message); }
                ix++;
                i++;
            }
            ix = 0;
            iy++;
            i++;
        }
        Console.Write( String.Join("",wa) );

    }





}



class OrthographicCamera
{
    public Vector3 position = new Vector3();
    public Vector3 rotation = new Vector3();
    public Object[] objects;
    public String[,] window;
    public World3D world;
    Window win;


    public OrthographicCamera(World3D world, Window win)
    {
        this.win = win;
        this.world = world;
        objects = world.objs;
        this.window = new String[win.y,win.x];
    }

    public void update()
    {
        foreach (Object obj in objects)
        {
            window = new Converter(obj, world, win, position, rotation).To2D();
            // convert 3D arr to 2D arr
        }
    }


}



class World3D
{
    public Object[] objs;
    public String[,,] positions;
    public Vector3 size;

    public World3D(int count_objects, Vector3 size)
    {
        objs = new Object[count_objects];
        positions = new String[size.y,size.x,size.z];
        this.size = size;
    }

    Object[] objects()
    {
        return objs;
    }

    public void add(Object obj, int id)
    {
        objs[id] = obj;
        switch (obj.type)
        {
            case "cube":
                obj.cube(this);
                break;
            case "line":
                obj.line_in3D(this);
                break;
            case "custom":
                obj.custom_obj(this);
                break;
        }
    }
    public void update()
    {
        
        this.positions = new string[this.size.y, this.size.x, this.size.z];

        foreach (Object obj in this.objs)
        {
            switch (obj.type)
            {
                case "cube":
                    obj.cube(this);
                    break;
                case "line":
                    obj.line_in3D(this);
                    break;
                case "custom":
                    obj.custom_obj(this);
                    break;
            }
        }
    }

}



class Vector3
{
    public int x;
    public int y;
    public int z;
    public Vector3()
    {
        this.x = 0;
        this.y = 0;
        this.z = 0;
    }

    public Vector3(int x, int y, int z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

}



class Object
{
    public Vector3 position = new Vector3();
    public Vector3 rotation = new Vector3();
    public Vector3 rotationPoint = new Vector3();
    public String[] sortRotate = new String[3];
    public Vector3[] customPoints;
    public String fill = "o";

    public Vector3 scale = new Vector3();
    public String type;

    public Object(String type)
    {
        this.type = type;
    }



    public void cube(World3D world)
    {

        int sx = scale.x;
        int sy = scale.y;
        int sz = scale.z;

        Vector3[] cube_points = new Vector3[sx * sy * sz];


        int n = 0;
        for (int isx = 0; isx < sx; isx++)
        {
            for (int isy = 0; isy < sy; isy++)
            {
                for (int isz = 0; isz < sz; isz++)
                {


                    if ((isx > 0 && isx < sx - 1) && (isy > 0 && isy < sy - 1))
                    {
                        cube_points[n] = new Vector3(0, 0, 0);
                        n++;
                    }
                    else
                    {
                        if ((isz > 0 && isz < sz - 1) && (isx > 0 && isx < sx - 1))
                        {

                            cube_points[n] = new Vector3(0, 0, 0);
                            n++;

                        }
                        else
                        {
                            if ((isy > 0 && isy < sy - 1) && (isz > 0 && isz < sz - 1))
                            {
                                cube_points[n] = new Vector3(0, 0, 0);
                                n++;
                            }
                            else
                            {
                                cube_points[n] = new Vector3(isx, isy, isz);
                                n++;
                            }
                        }
                    }


                }
            }
        }






        Vector3[] new_point = Math2.rotatePoint(cube_points, this,this.rotationPoint);

        int c = 0;
        while (c < cube_points.Length)
        {


            int x = new_point[c].x;
            int y = new_point[c].y;
            int z = new_point[c].z;



            try
            {
                world.positions[y + position.y,x + position.x,z + position.z] = fill;
            }
            catch (Exception e) { /*Console.WriteLine("error line 419  " + e.Message);*/ }

            c++;
        }



    }



    public void line_in3D(World3D world)
    {


        int sx = scale.x;

        Vector3[] line_points = new Vector3[sx];


        //x
        int i = 0;
        while (i < sx)
        {
            line_points[i] = new Vector3(i, 0, 0);
            i++;
        }



        Vector3[] new_point = Math2.rotatePoint(line_points, this,this.rotationPoint);
        int c = 0;
        while (c < line_points.Length)
        {


            int x = new_point[c].x;
            int y = new_point[c].y;
            int z = new_point[c].z;

            try
            {
                world.positions[y + position.y,x + position.x,z + position.z] = fill;
            }
            catch (Exception e) { /*Console.WriteLine("error line 450  " + e.Message);*/ }

            c++;

        }



    }



    public void custom_obj(World3D world)
    {

        
        Vector3[] new_point = Math2.rotatePoint(this.customPoints, this,this.rotationPoint);
        


        int i = 0;
        while (i < customPoints.Length)
        {


            int x = new_point[i].x;
            int y = new_point[i].y;
            int z = new_point[i].z;

            try
            {
                world.positions[y + position.y,x + position.x,z + position.z] = fill;
            }
            catch (Exception e) { Console.WriteLine("error line 494  " + e.Message); }

            i++;

        }

    }



}



class Converter
{
    public String[,] window;
    public Vector3 camPos;
    public Vector3 camRot;
    public World3D world;

    public Converter(Object obj, World3D world, Window win, Vector3 camPos, Vector3 camRot)
    {
        this.camPos = camPos;
        this.camRot = camRot;
        this.world = world;
        window = new String[win.y,win.x];
    }

    public String[,] To2D()
    {



        int ix = 0;
        int iy = 0;
        int iz = 0;
        int ixc = window.GetLength(1);
        int iyc = window.GetLength(0);
        int izc = world.positions.GetLength(2);

        while (iz < izc)
        {
            while (iy < iyc)
            {
                while (ix < ixc)
                {
                    try
                    {
                        if (window[iy,ix] == E.empty || window[iy,ix] == null)
                        {
                            window[iy,ix] = world.positions[iy + camPos.y,ix + camPos.x,iz + camPos.z];
                        }
                    }
                    catch (Exception e)
                    { Console.WriteLine("error line 538  " + e.Message); }
                    ix++;
                }
                ix = 0;
                iy++;

            }
            iy = 0;
            iz++;
        }




        return window;
    }







}



class Math2
{

    public static int divide(int a, int b)
    {

        if (b == 0)
        {
            return 0;
        }
        else
        {
            return a / b;
        }

    }
    public static double divide(double a, double b)
    {

        if (b == 0)
        {
            return 0;
        }
        else
        {
            return a / b;
        }

    }


    public static double ToRadian(double deg)
    {
        return Math.PI / 180 * deg;
    }






    public static Vector3[] rotatePoint(Vector3[] points, Object obj,Vector3 rp)
    {


        double ry = Math2.ToRadian(obj.rotation.y);


        double rx = Math2.ToRadian(obj.rotation.x);


        double rz = Math2.ToRadian(obj.rotation.z);




        int i1 = 0;
        int i1c = points.Length;

        while (i1 < i1c)
        {

            int sri = 0;
            while (sri < 3)
            {
                switch (obj.sortRotate[sri])
                {
                    case "Y":
                        // rotate Y
                        double dxz = Math.Sqrt(Math.Pow(points[i1].z-rp.z, 2) + Math.Pow(points[i1].x-rp.x, 2));
                        
                        if (points[i1].x-rp.x < 0)
                        {
                            double degxz = Math.PI - Math.Asin( Math2.divide(points[i1].z-rp.z , dxz) );

                            int xr = (int)Math.Round((Math.Cos(degxz + ry) * dxz));
                            int zr = (int)Math.Round((Math.Sin(degxz + ry) * dxz));

                            points[i1].x = xr + rp.x;
                            points[i1].z = zr + rp.z;
                        }
                        else
                        {
                            double degxz = Math.Asin( Math2.divide(points[i1].z-rp.z , dxz) );

                            int xr = (int)Math.Round((Math.Cos(degxz + ry) * dxz));
                            int zr = (int)Math.Round((Math.Sin(degxz + ry) * dxz));

                            points[i1].x = xr + rp.x;
                            points[i1].z = zr + rp.z;

                        }
                        break;
                    case "X":

                        // rotate X
                        double dzy = Math.Sqrt(Math.Pow(points[i1].z-rp.z, 2) + Math.Pow(points[i1].y-rp.y, 2));
                        if (points[i1].z-rp.z < 0)
                        {
                            double degzy = Math.PI - Math.Asin( Math2.divide(points[i1].y-rp.y , dzy) );

                            int yr = (int)Math.Round((Math.Sin(degzy + rx) * dzy));
                            int zr = (int)Math.Round((Math.Cos(degzy + rx) * dzy));

                            points[i1].y = yr + rp.y;
                            points[i1].z = zr + rp.z;
                        }
                        else
                        {
                            double degzy = Math.Asin(Math2.divide(points[i1].y-rp.y , dzy) );

                            int yr = (int)Math.Round((Math.Sin(degzy + rx) * dzy));
                            int zr = (int)Math.Round((Math.Cos(degzy + rx) * dzy));

                            points[i1].y = yr + rp.y;
                            points[i1].z = zr + rp.z;
                        }
                        break;
                    case "Z":

                        // rotate Z
                        double dxy = Math.Sqrt(Math.Pow(points[i1].x-rp.x, 2) + Math.Pow(points[i1].y-rp.y, 2));
                        if (points[i1].x-rp.x < 0)
                        {
                            double degxy = Math.PI - Math.Asin( Math2.divide(points[i1].y-rp.y , dxy) );

                            int xr = (int)Math.Round((Math.Cos(degxy + rz) * dxy));
                            int yr = (int)Math.Round((Math.Sin(degxy + rz) * dxy));

                            points[i1].x = xr + rp.x;
                            points[i1].y = yr + rp.y;
                        }
                        else
                        {
                            double degxy = Math.Asin( Math2.divide(points[i1].y-rp.y , dxy) );

                            int xr = (int)Math.Round((Math.Cos(degxy + rz) * dxy));
                            int yr = (int)Math.Round((Math.Sin(degxy + rz) * dxy));

                            points[i1].x = xr + rp.x;
                            points[i1].y = yr + rp.y;
                        }
                        break;
                }
                sri++;
            }




            i1++;
        }


        return points;
    }


}



class E
{
    public static String empty = "`";
}


