using System;

public class MyRandom : System.Random
{
  public MyRandom() : base()
  {
  }

  public MyRandom(int initializer) : base(initializer)
  {
  }

  public float NextFloat(float min, float max)
  {
    if (min > max)
      throw new Exception ("max " + max + " is less than min " + min);
    
    return ((float)NextDouble () * (max - min)) + min;
  }
}