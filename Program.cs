using d9.bgp.baghchal;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.Unicode;
        BaghChalGame game = new(new BaghChalAgent_Random(), new BaghChalAgent_Random());
        game.Play();
    }
}