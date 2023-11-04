﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace hope
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
       // Класс для отображения окна приложения
    public partial class MainWindow : Window
    {
        // Конструктор окна
        public MainWindow()
        {
            // Инициализируем компоненты окна
            InitializeComponent();
            // Создаем объект модели представления
            ViewModel vm = new ViewModel();
            // Связываем окно с моделью представления
            DataContext = vm;
        }
    }
}
