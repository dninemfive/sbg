using d9.bgp.baghchal;

internal class Program
{
    private static void Main(string[] args)
    {
        BaghChalGame game = new(new BaghChalAgent_Random(), new BaghChalAgent_Random());
        game.Play();
    }
}