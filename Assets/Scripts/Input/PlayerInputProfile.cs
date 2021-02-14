using System.IO;

[System.Serializable]
public struct PlayerInputProfile
{
    public string primary;
    public string secondary;
    public string moveLeft;
    public string moveRight;
    public string moveUp;
    public string moveDown;
    public string start;

    public void Serialize(StreamWriter writer)
    {
        writer.WriteLine(primary);
        writer.WriteLine(secondary);
        writer.WriteLine(moveLeft);
        writer.WriteLine(moveRight);
        writer.WriteLine(moveUp);
        writer.WriteLine(moveDown);
        writer.WriteLine(start);

    }

    public void Deserialize(StreamReader reader)
    {
        primary = reader.ReadLine();
        secondary = reader.ReadLine();
        moveLeft = reader.ReadLine();
        moveRight = reader.ReadLine();
        moveUp = reader.ReadLine();
        moveDown = reader.ReadLine();
        start = reader.ReadLine();
    }
}
