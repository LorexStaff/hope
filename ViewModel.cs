using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Input;
using Newtonsoft.Json;
using System.Windows.Documents;
using GalaSoft.MvvmLight.Command;

namespace hope
{
    // Класс модели представления
    public class ViewModel : INotifyPropertyChanged
    {
        // Событие для уведомления об изменении свойств
        public event PropertyChangedEventHandler PropertyChanged;

        // Метод для вызова события
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // Конструктор модели представления
        public ViewModel()
        {
            // Инициализируем список цветов
            Flowers = new BindingList<Flower>();
        }

        // Свойство для хранения списка цветов
        public BindingList<Flower> Flowers { get; set; }

        // Свойство для хранения выбранного цветка
        private Flower selectedFlower;
        public Flower SelectedFlower
        {
            get { return selectedFlower; }
            set
            {
                selectedFlower = value;
                OnPropertyChanged(nameof(SelectedFlower));
            }
        }

        // Свойство для хранения имени нового цветка
        private string newName;
        public string NewName
        {
            get { return newName; }
            set
            {
                newName = value;
                OnPropertyChanged(nameof(NewName));
            }
        }

        // Свойство для хранения региона нового цветка
        private string newRegion;
        public string NewRegion
        {
            get { return newRegion; }
            set
            {
                newRegion = value;
                OnPropertyChanged(nameof(NewRegion));
            }
        }

        // Свойство для хранения цены нового цветка
        private int newPrice;
        public int NewPrice
        {
            get { return newPrice; }
            set
            {
                newPrice = value;
                OnPropertyChanged(nameof(NewPrice));
            }
        }
        // Свойство для хранения редкости нового цветка
        private bool newRare;
        public bool NewRare
        {
            get { return newRare; }
            set
            {
                newRare = value;
                OnPropertyChanged(nameof(NewRare));
            }
        }

        public bool AddCommand
        {
            get
            {
                return false;
            }
            set
            {
                Add();

            }
        }
        public bool DeleteCommand
        {
            get
            {
                return false;
            }
            set
            {
                Delete();

            }
        }
        public bool LoadCommand
        {
            get
            {
                return false;
            }
            set
            {
                Load();

            }
        }
        public bool SaveCommand
        {
            get
            {
                return false;
            }
            set
            {
                Save();

            }
        }

        // Метод для выполнения команды загрузки
        public void Load()
        {
            // Создаем UDP-клиент для отправки запроса на сервер
            UdpClient sender = new UdpClient();
            // Формируем запрос в виде строки с методом GET
            string request = "GET";
            // Преобразуем строку в массив байтов
            byte[] data = Encoding.UTF8.GetBytes(request);
            // Отправляем запрос на сервер по адресу 127.0.0.1:8000
            sender.Send(data, data.Length, "127.0.0.1", 8000);
            // Ожидаем ответ от сервера в течение 5 секунд
            sender.Client.ReceiveTimeout = 5000;
            try
            {
                // Получаем ответ от сервера в виде массива байтов и его удаленный адрес
                IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);
                byte[] data2 = sender.Receive(ref remoteEP);
                // Преобразуем массив байтов в строку
                string response = Encoding.UTF8.GetString(data2);
                // Преобразуем строку в список цветов
                var flowers = JsonConvert.DeserializeObject<List<Flower>>(response);
                // Очищаем текущий список цветов
                Flowers.Clear();
                // Добавляем полученные цветы в список
                foreach (var flower in flowers)
                {
                    Flowers.Add(flower);
                }
                // Выводим сообщение об успешной загрузке
                MessageBox.Show("Данные загружены с сервера");
            }
            catch (SocketException ex)
            {
                // Выводим сообщение об ошибке
                MessageBox.Show("Не удалось получить ответ от сервера: " + ex.Message);
            }
            // Закрываем соединение
            sender.Close();
        }
        public void Add()
        {
            // Проверяем, что все поля заполнены
            if (string.IsNullOrEmpty(NewName) || string.IsNullOrEmpty(NewRegion) || NewPrice <= 0)
            {
                // Выводим сообщение об ошибке
                MessageBox.Show("Пожалуйста, заполните все поля правильно");
                return;
            }
            // Создаем новый объект цветка с введенными данными
            Flower newFlower = new Flower(NewName, NewRegion, NewPrice, NewRare);

            // Добавляем новый цветок в список
            Flowers.Add(newFlower);
            // Очищаем поля ввода
            NewName = "";
            NewRegion = "";
            NewPrice = 0;
            NewRare = false;
            // Выводим сообщение об успешном добавлении
            MessageBox.Show("Цветок добавлен в список");
        }
        public void Delete()
        {
            // Проверяем, что выбран цветок для удаления
            if (SelectedFlower == null)
            {
                // Выводим сообщение об ошибке
                MessageBox.Show("Пожалуйста, выберите цветок для удаления");
                return;
            }
            // Удаляем выбранный цветок из списка
            Flowers.Remove(SelectedFlower);
            // Выводим сообщение об успешном удалении
            MessageBox.Show("Цветок удален из списка");
        }
        public void Save()
        {
            // Создаем UDP-клиент для отправки запроса на сервер
            UdpClient sender = new UdpClient();
            // Формируем запрос в виде строки с методом POST и списком цветов в формате JSON
            string request = "POST" + JsonConvert.SerializeObject(Flowers);
            // Преобразуем строку в массив байтов
            byte[] data = Encoding.UTF8.GetBytes(request);
            // Отправляем запрос на сервер по адресу 127.0.0.1:8000
            sender.Send(data, data.Length, "127.0.0.1", 8000);
            try
            {
                // Получаем ответ от сервера в виде массива байтов и его удаленный адрес
                IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);
                byte[] data2 = sender.Receive(ref remoteEP);
                // Преобразуем массив байтов в строку
                string response = Encoding.UTF8.GetString(data2);
                // Выводим сообщение об ответе
                MessageBox.Show(response);
            }
            catch (SocketException ex)
            {
                // Выводим сообщение об ошибке
                MessageBox.Show("Не удалось получить ответ от сервера: " + ex.Message);
            }
        }
    }
}





