namespace d9.sbg.examples.baghchal;
public enum BaghChalPlayer { Sheep, Wolf }
public static class BaghChalPlayerExtensions
{
    public static bool IsEmpty(this BaghChalPlayer? value)
        => value is null;
}