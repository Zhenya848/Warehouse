namespace Entities;

public class ErrorTypes
{
    public static Error Validation(string fieldName) =>
        new ("Некорректные данные", $"Поле {fieldName} некорректное");
    
    public static Error IsRequired(string fieldName) =>
        new ("Данные ожидаются", $"Не были получены данные для поля {fieldName}");

    public static Error NotFound(Guid? id = null) =>
        new Error("Не найдено", $"Элемент {(id is not null ? $"С guid {id}" : "")} не найден");
}