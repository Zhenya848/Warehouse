namespace Entities;

public class Error
{
    public string Code { get; private set; }
    public string Name { get; private set; }

    public Error(string code, string name)
    {
        Code = code;
        Name = name;
    }
}