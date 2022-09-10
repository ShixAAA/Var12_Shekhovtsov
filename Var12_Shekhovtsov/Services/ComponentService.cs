using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Var12_Shekhovtsov.Model;


namespace Var12_Shekhovtsov.Services
{
    public class ComponentService
    {
        // Создаем список для хранения данных из источника
        List<Component> ComponentList = new();

        // Метод GetFood() служит для извлечения и сруктурирования данных
        // в соответсвии с существующей моделью данных
        public async Task<IEnumerable<Component>> GetComponent()
        {
            // Если список содержит какие-то элементы
            // то вернется последовательность с содержимым этого списка
            if (ComponentList?.Count > 0)
                return ComponentList;

            // В данном блоке кода осуществляется подключение, чтение
            // и дессериализация файла - источника данных
            using var stream = await FileSystem.OpenAppPackageFileAsync("Components.json");
            using var reader = new StreamReader(stream);
            var contents = await reader.ReadToEndAsync();
            ComponentList = JsonSerializer.Deserialize<List<Component>>(contents);

            return ComponentList;
        }
    }

}
