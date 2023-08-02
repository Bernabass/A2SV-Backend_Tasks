using System;

class Shape{
    public string Name { get; private set; }

    public Shape(string name){
        this.Name = name;
    }

    public virtual double CalculateArea(){
        return 0;
    }
}

class Circle : Shape{
    public double Radius { get; private set; }

    public Circle(string name, double radius) : base(name){
        this.Radius = radius;
    }

    public override double CalculateArea(){
        return Math.PI * this.Radius * this.Radius;
    }
}

class Rectangle : Shape{
    public double Width { get; private set; }
    public double Height { get; private set; }

    public Rectangle(string name, double width, double height) : base(name){
        this.Width = width;
        this.Height = height;
    }

    public override double CalculateArea(){
    
        return this.Width * this.Height;
    }
}

class Triangle : Shape{

    public double Base { get; private set; }
    public double Height { get; private set; }

    public Triangle(string name, double triangleBase, double height) : base(name){
        this.Base = triangleBase;
        this.Height = height;
    }

    public override double CalculateArea(){
        return (this.Base * this.Height) / 2;
    }
}

class Demo{
    public static void PrintShapeArea(Shape shape){
        double area = shape.CalculateArea();
        Console.WriteLine($"Shape: {shape.Name}, Area: {area}");
    }

    public static void Main(string[] args){
        Circle circle = new Circle("Circle", 3);
        Rectangle rectangle = new Rectangle("Rectangle", 5, 6);
        Triangle triangle = new Triangle("Triangle", 10, 30);

        PrintShapeArea(circle);
        PrintShapeArea(rectangle);
        PrintShapeArea(triangle);
    }
}
