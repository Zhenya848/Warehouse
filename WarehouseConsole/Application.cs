namespace WarehouseConsole;

public class Application
{
    private readonly WarehouseConsole _warehouseConsole;
    private readonly ProductTypesConsole _productTypesConsole;
    private readonly ProductsConsole _productsConsole;
    private readonly ContainerConsole _containerConsole;

    public Application(
        WarehouseConsole warehouseConsole, 
        ProductTypesConsole productTypesConsole,
        ProductsConsole productsConsole,
        ContainerConsole containerConsole)
    {
        _warehouseConsole = warehouseConsole;
        _productTypesConsole = productTypesConsole;
        _productsConsole = productsConsole;
        _containerConsole = containerConsole;
    }
    
    public void Run()
    {
        while (true)
        {
            Console.WriteLine("Warehouse Simulator\n");

            Console.WriteLine("1. Создать склад");
            Console.WriteLine("2. Создать товар");
            Console.WriteLine("3. Создать тип товара");
            Console.WriteLine("4. Создать контейнер определенных товаров для склада");
            Console.WriteLine("5. Показать все склады");
            Console.WriteLine("6. Показать все товары");
            Console.WriteLine("7. Изменить склад");
            Console.WriteLine("8. Уничтожить склад");
            Console.WriteLine("9. Убрать контейнер из склада");
            Console.WriteLine("10. Удалить товар");
            Console.WriteLine("11.Удалить тип товара");

            var choose = Extensions.GetFloatFromReadLine("\nВаш выбор: ");

            switch (choose)
            {
                case 1:
                    _warehouseConsole.WarehouseCreating();
                    break;
                case 2:
                    _productsConsole.ProductCreating();
                    break;
                case 3:
                    _productTypesConsole.ProductTypeCreating();
                    break;
                case 4:
                    _containerConsole.ContainerCreating();
                    break;
                case 5:
                    _warehouseConsole.ShowAllWarehouses();
                    break;
                case 6:
                    _productsConsole.ShowAllProducts();
                    break;
                case 7:
                    _warehouseConsole.WarehouseUpdate();
                    break;
                case 8:
                    _warehouseConsole.WarehouseDelete();
                    break;
                case 9:
                    _warehouseConsole.WarehouseDeleteContainer();
                    break;
                case 10:
                    _productsConsole.DeleteProduct();
                    break;
                case 11:
                    _productTypesConsole.ProductTypeDelete();
                    break;
            }

            Console.Clear();
        }
    }
}