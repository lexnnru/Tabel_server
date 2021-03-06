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
using Tabel_server.Model;
using System.Data;
using Tabel_server.Interfaces;
using Tabel_server.Model.Data;
using System.Collections.ObjectModel;
using Calendar;
using Tabel_server.Model.Data.Table;
using System.Globalization;
using System.Data.Linq;

namespace Tabel_server
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    ///
    //public interface ImainWindow
    //{  void ShowMess(string message);
    //    event Action<string> Lb_users_SelectionChange;
    //    event Action<string> LoadHoli;
        
    //    event Func <DateTime, List<MonthEmployeesDatasOld>>GetMonthEmployeeData;
    //    event Action UpdateMonthEmployees;
    //    event Action <List<string>> LoadDataTableToDB;
    //    event Action ShowCalendar;
    //    void SetlbUsers(ObservableCollection<MonthEmployee> employees);
    //    List<DateTime> HoliDateTimes { get; set; }
    //    Window Get { get; set; }
    //    IUserControl1 uc1 { get; }
    //    UserControl3 uc3 { get; }
    //    MangeUsers mu { get; set; }
    //    Calendar.MainWindow calendar { get; set; }
    //    List<(DateTime, DayType, TimeSpan)> SpecialDays { get; set; }

    //    string tabelNamber { get; set; }

    //    DateTime dtMain { get; set; }
        
    //        ObservableCollection<MonthEmployee> monthEmployees { get; set; }

    //}
    public partial class MainWindow : Window
    {
        public MainWindow()
        {   
            InitializeComponent();
                DataContext = this;
            dtMain = dtpicker.Data;
            uc2 = new UserControl2();
            uc1 = new UserControl1();
            uc3 = new UserControl3();
            mu = new MangeUsers();
            new Presenter.Presenter(this);
            dtpicker.SelectedDateChanged += Dtpicker_SelectedDateChanged;
            
            //Loger.LogChange += Loger_LogChange;
        }

        private void Loger_LogChange(string obj)
        {
            MessageBox.Show(obj);
            
        }

        private void Dtpicker_SelectedDateChanged(DateTime obj)
        {
            dtMain = obj;
            if (lbUsers.SelectedIndex == -1)
            { }
            else
            {
                MonthEmployee employee = monthEmployees[lbUsers.SelectedIndex];
                Lb_users_SelectionChange?.Invoke(employee.Employee.TabelNumber);
            }
            UpdateMonthEmployees?.Invoke();
            
        }
        
        public IUserControl1 uc1 { get; private set; }
        public UserControl2 uc2 { get; private set; }
        public UserControl3 uc3 { get;  set; }
        public string tabelNamber { get; set; }
        public DateTime dtMain { get; set; }
        public List<DateTime> HoliDateTimes { get; set; }
        public MangeUsers mu { get; set; }
        public Calendar.MainWindow calendar { get; set; }
        public List<(DateTime, DayType, TimeSpan)> SpecialDays { get; set; }
        public ObservableCollection<MonthEmployee> monthEmployees { get; set; }
       
        public Window Get
        {
            get { return this; }
            set { }
        }

        public event Action<string> LoadHoli;
        public event Action<string> Lb_users_SelectionChange;
        public event Func<DateTime, List<MonthEmployeesDatasOld>> GetMonthEmployeeData;
        public event Action UpdateMonthEmployees;
        public event Action<List<string>> LoadDataTableToDB;
        public event Action ShowCalendar;
        public void ShowMess(string message)
        {
            MessageBox.Show(message);
        }
        private void LbUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox lb = (ListBox)sender;
            if (lb.SelectedIndex!=-1)
            {
                //MonthEmployeesDatasOld emp = employees[lb.SelectedIndex];
                MainGrid.Children.Clear();
               // uc1.SetSource(emp);
                uc1.SetSource(monthEmployees[lb.SelectedIndex]);
                MainGrid.Children.Add(uc1.uc1);
                tbNameTable.Text = "Табель работника: "+ monthEmployees[lb.SelectedIndex].Employee.Surname +" " + monthEmployees[lb.SelectedIndex].Employee.Surname + ", за " +dtMain.ToString("MMMM",  CultureInfo.CurrentCulture).ToLower() +" " +dtMain.Year +" года.";
            }
            else { }
        }
        public void SetlbUsers(ObservableCollection<MonthEmployee> monthemployees)
        {
            lbUsers.ItemsSource = monthemployees;
            this.monthEmployees = monthemployees;
        }
        private void View2_Click(object sender, RoutedEventArgs e)
        {
            if (monthEmployees.Count!=0)
            {
                uc2.SetSummaryTable(monthEmployees, dtMain);
                MainGrid.Children.Clear();
                MainGrid.Children.Add(uc2.uc2);
            }
            tbNameTable.Text = "Таблица отработанных сотрудниками часов за: " + dtMain.ToString("MMMM", CultureInfo.CurrentCulture).ToLower() + " " + dtMain.Year + " года.";
        }
        private void View3_Click(object sender, RoutedEventArgs e)
        {
            uc3.SetSource(monthEmployees, HoliDateTimes, dtMain);
            MainGrid.Children.Clear();
            MainGrid.Children.Add(uc3.uc3);
            tbNameTable.Text = "Сводная таблица отработанных часов: " + dtMain.ToString("MMMM", CultureInfo.CurrentCulture).ToLower() + " " + dtMain.Year + " года.";
        }
        //private void BtloadHoli_Click(object sender, RoutedEventArgs e)
        //{
        //    Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();
        //    openFileDlg.Filter = ("File Json(*.json)|*.json|All files(*.*)|*.*");
        //    Nullable<bool> result = openFileDlg.ShowDialog();
        //    if (result == true)
        //    {
        //        LoadHoli?.Invoke(openFileDlg.FileName);
        //        MessageBox.Show("Готово");
        //    }
            
        //    UpdateMonthEmployees?.Invoke();
        //}
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if ( min.Width== new GridLength(0))
            {
                min.Width = new GridLength(240, GridUnitType.Pixel);
            }
            else
            min.Width = new GridLength(0);
        }
        private void BtManageEployees_MouseLeftButtonDown(object sender, RoutedEventArgs e)
        {
            MainGrid.Children.Clear();
            MainGrid.Children.Add(mu);
        }

        private void BtLoadTabl_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDlg = new Microsoft.Win32.OpenFileDialog();
            openFileDlg.Filter = ("File TimeTable(*.tt)|*.tt|All files(*.*)|*.*");
            openFileDlg.Multiselect=true;
            Nullable<bool> result = openFileDlg.ShowDialog();
            if (result == true)
            {
                LoadDataTableToDB?.Invoke(openFileDlg.FileNames.ToList());
            }
            UpdateMonthEmployees?.Invoke();
        }

        private void BtCalendar_Click(object sender, RoutedEventArgs e)
        {
            calendar = new Calendar.MainWindow(SpecialDays);
            ShowCalendar?.Invoke();
            calendar.Show();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            ShowMess(sender.ToString());
        }

        private void BtCalculateSalary_Click(object sender, RoutedEventArgs e)
        {
            MainGrid.Children.Clear();
            MainGrid.Children.Add(new UCCalculateZP(monthEmployees));
            tbNameTable.Text = "Рассчет ЗП за: " + dtMain.ToString("MMMM", CultureInfo.CurrentCulture).ToLower() + " " + dtMain.Year + " года.";
        }

        private void lbUsers_LostFocus(object sender, RoutedEventArgs e)
        {
            lbUsers.SelectedIndex = -1;
        }

        private void btSettings_Click(object sender, RoutedEventArgs e)
        {
            Tabel_server.UCSettings uCSettings = new UCSettings();
            MainGrid.Children.Clear();
            MainGrid.Children.Add(uCSettings);
        }
    }
}
