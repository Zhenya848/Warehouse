using Entities;

namespace WarehouseConsole;

public static class Extensions
{
    public static string ToResponse(this Error error)
    {
        return $"\nКод ошибки: {error.Code}, Сообщение: {error.Name}";
    }
    
    public static float GetFloatFromReadLine(string fieldName)
    {
        while (true)
        {
            Console.Write(fieldName);
            float value;

            if (float.TryParse(Console.ReadLine(), out value) == false)
                continue;

            return value;
        }
    }
    
    public static int GetIntFromReadLine(string fieldName)
    {
        while (true)
        {
            Console.Write(fieldName);
            int value;

            if (int.TryParse(Console.ReadLine(), out value) == false)
                continue;

            return value;
        }
    }
    
    public static long GetLongFromReadLine(string fieldName)
    {
        while (true)
        {
            Console.Write(fieldName);
            long value;

            if (long.TryParse(Console.ReadLine(), out value) == false)
                continue;

            return value;
        }
    }
}