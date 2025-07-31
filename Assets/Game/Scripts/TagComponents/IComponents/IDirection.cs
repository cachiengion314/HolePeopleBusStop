using Unity.Mathematics;

public struct DirectionValueData
{
  public int2 DirectionValue;
}

public interface IDirection
{
  public int2 GetDirectionValue();
  public void SetDirectionValue(int2 value);
}
