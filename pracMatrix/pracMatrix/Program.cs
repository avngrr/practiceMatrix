using pracMatrix;

string fileName = @"fractal - input.txt";
Fractal fr = new Fractal(".#./..#/###");
using (StreamReader reader = File.OpenText(fileName))
{
    string line;
    while ((line = reader.ReadLine()) != null)
    {
        fr.AddExtension(line);
    }
    for (int i = 0; i < 18; i++)
    {
        fr.RunExtensions();
    }
    Console.WriteLine(fr.CountLightsOn().ToString());
}