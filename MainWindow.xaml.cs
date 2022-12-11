using System;
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
using lib_13;
using LibMas;

namespace Prakt3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int[,] mas;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Random_Click(object sender, RoutedEventArgs e) // Создание и заполнение массива
        {
            try
            {
                Itogovoe.Text = ""; //Очистка поля вывода
                Int32.TryParse(randomchik.Text, out int ran);
                Int32.TryParse(Stroki.Text, out int rows);
                Int32.TryParse(Stolbzi.Text, out int column);
                mas = LibMas.Masssiv.DVZapol(rows, column, ran); //заполнение массива с помощью функции
                dataGrid.ItemsSource = VisualArray.ToDataTable(mas).DefaultView; //Вывод массива в таблицу
            }
            catch(OverflowException)
            {
                MessageBox.Show("Введите значения размерности массива больше -1");
            }
            catch(ArgumentOutOfRangeException)
            {
                MessageBox.Show("Введите значения рандома больше 0");
            }
        }

        private void Chistk_Click(object sender, RoutedEventArgs e)
        {
            mas = LibMas.Masssiv.Clear(mas); //Очистка массива с помощью функции
            dataGrid.ItemsSource = null; //Очистка таблицы путем присваивания пустого значения
            Itogovoe.Clear();
        }

        private void Itog_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Itogovoe.Text = Resh.SredArif(mas); //Получение итогового расчёта использованием функции
            }
            catch(NullReferenceException)
            {
                MessageBox.Show("Массив не может быть пустым");
            }
        }

        private void Quit_Clicl(object sender, RoutedEventArgs e)
        {
            this.Close(); //Выход
        }

        private void Spravka_Click(object sender, RoutedEventArgs e) //Вывод Текстового окна о создателе программы и задании
        {
            MessageBox.Show("Калитин С.А. ИСП-31 Вариант 13\n Дана матрица размера M x N"
                + " В каждом ее столбце найти количество элементов,меньших среднего арифметического всех элементов этого столбца");
        }

        private void Zapisat_Click(object sender, RoutedEventArgs e) //Запись массива(матрицы) в файл
        {
            try
            {
                LibMas.Masssiv.DVSaveMassiv(mas);//Использование функции для записи
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Массив не может быть пустым");
            }
        }

        private void Open_Click(object sender, RoutedEventArgs e) //Открытие массива(матрицы) из файл
        {
            try
            {
                LibMas.Masssiv.DVOpenMassiv(ref mas); //Функция для чтения массива из файла
                dataGrid.ItemsSource = VisualArray.ToDataTable(mas).DefaultView; //Вывод открытого массива в таблицу
            }
            catch(NullReferenceException)
            {
                MessageBox.Show("Массив не может быть пустым");
            }
            catch(OverflowException)
            {
                MessageBox.Show("Размерность массива не может быть меньше 0");
            }
        }

        private void dataGrid_CellEditEnding(object sender,DataGridCellEditEndingEventArgs e) //Занесение редактируемого в таблице значения в массив
        {
            int indexColumn = e.Column.DisplayIndex;//Получение индекса столбца
            int indexRow = e.Row.GetIndex(); //Получение индекста строки
            mas[indexRow, indexColumn] = Convert.ToInt32(((TextBox)e.EditingElement).Text); //Присваивание нового значение элементу массива под редактируемым номером
            Itogovoe.Text = "";//Очистка поля вывода
        }
    }
}
