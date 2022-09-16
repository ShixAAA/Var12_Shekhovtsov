using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Var12_Shekhovtsov.Model;
using Var12_Shekhovtsov.Services;


namespace Var12_Shekhovtsov.ViewModel
{
    public class ComponentViewModel : BindableObject
    {
        // Переменная для хранения состояния
        // выбранного элемента коллекции
        private Component _selectedItem;
        // Объект с логикой по извлечению данных
        // из источника
        ComponentService ComponentService = new();

        // Коллекция извлекаемых объектов
        public ObservableCollection<Component> Components { get; } = new();

        // Конструктор с вызовом метода
        // получения данных
        public ComponentViewModel()
        {
            GetComponentsAsync();
        }

        // Публичное свойство для представления
        // описания выбранного элемента из коллекции
        public string Desc { get; set; }

        // Свойство для представления и изменения
        // состояния выбранного объекта
        public Component SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                Desc = value?.Description;
                // Метод отвечает за обновление данных
                // в реальном времени
                OnPropertyChanged(nameof(Desc));
            }
        }
        public ICommand AddItemCommand => new Command(() => AddNewItem());
        private void AddNewItem()
        {
            Components.Add(new Component
            {
                Id = Components.Count + 1,
                Name = "Title " + Components.Count,
                Description = "Description",
                TypeOfComponent = "country"
            });
        }

        // Метод получения коллекции объектов
        async Task GetComponentsAsync()
        {
            try
            {
                var components = await ComponentService.GetComponent();

                if (Components.Count != 0)
                    Components.Clear();

                foreach (var component in components)
                {
                    Components.Add(component);
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Ошибка!",
                    $"Что-то пошло не так: {ex.Message}", "OK");
            }
        }
    }

}
