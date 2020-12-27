using System.IO;

[System.Serializable]
public struct PlayerInputProfile
{
    public string primary;
    public string moveLeft;
    public string moveRight;
    public string moveUp;
    public string moveDown;

    public void Serialize(StreamWriter writer)
    {
        writer.WriteLine(primary);
        writer.WriteLine(moveLeft);
        writer.WriteLine(moveRight);
        writer.WriteLine(moveUp);
        writer.WriteLine(moveDown);
    }

    public void Deserialize(StreamReader reader)
    {
        primary = reader.ReadLine();
        moveLeft = reader.ReadLine();
        moveRight = reader.ReadLine();
        moveUp = reader.ReadLine();
        moveDown = reader.ReadLine();
    }
}
