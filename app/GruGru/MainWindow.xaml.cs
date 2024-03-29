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
using System.Net;
using System.IO;
using System.Collections.Specialized;
using System.Net.Http;
using Newtonsoft.Json;
using GruGru.Model;
using System.Collections.ObjectModel;
using System.Xml.Linq;
using System.Xml;
using System.Security.Cryptography;
using System.Timers;
using LiveCharts;
using LiveCharts.Wpf;

namespace GruGru
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //btn: button
        //tb: textblock
        //tbx: textbox
        //grid: grid
        //cbb: combobox
        //lv: listview
        //gvc: gridviewcolumn
        Double height = SystemParameters.WorkArea.Height - 10;
        Double width = SystemParameters.WorkArea.Width - 10;
        Double height005 = SystemParameters.WorkArea.Height * 0.05;//25
        Double height004 = SystemParameters.WorkArea.Height * 0.04;//20
        Double height003 = SystemParameters.WorkArea.Height * 0.03;//15
        Double height0025 = SystemParameters.WorkArea.Height * 0.025;//12.5
        Double height0027 = SystemParameters.WorkArea.Height * 0.027;
        Double height002 = SystemParameters.WorkArea.Height * 0.019;//40
        Double height0013 = SystemParameters.WorkArea.Height * 0.013;//15
        Double height0018 = SystemParameters.WorkArea.Height * 0.018;//15
        Double height0028 = SystemParameters.WorkArea.Height * 0.028;//40
        Double height0023 = SystemParameters.WorkArea.Height * 0.023;//12.5

        const string SERVER = "http://localhost:8080/api/";

        private static Timer timer;

        bool rememberme = false;
        string userLocal;
        string passLocal;

        int loggedInUserId = -1;
        string loggedInUserType = "1";
        string loggedInUserName = "";

        static string GetMd5Hash(MD5 md5Hash, string input)
        {

            // Convert the input string to a byte array and compute the hash.
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

            // Create a new Stringbuilder to collect the bytes
            // and create a string.
            StringBuilder sBuilder = new StringBuilder();

            // Loop through each byte of the hashed data 
            // and format each one as a hexadecimal string.
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }

            // Return the hexadecimal string.
            return sBuilder.ToString();
        }

        public string Get(string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        public string Post(string uri, string payload)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.ContentType = "application/json";
            request.Method = "POST";
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(payload);
                streamWriter.Write("\n");
                streamWriter.Flush();
                streamWriter.Close();
            }

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
            this.Left = 0;
            this.Top = 0;
            this.Height = height + 10;
            this.Width = width + 10;
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            XDocument objDoc = XDocument.Load(path + "/Rememberme.xml");

            try
            {
                LoadMenu();
            }
            catch
            {
                MessageBoxResult tmp = MessageBox.Show("Không thể kết nối đến server");
                Application.Current.Shutdown();
            }

            if (objDoc.Root.Elements().ElementAt(0).Value == "True")
            {
                rememberme = true;
                userLocal = objDoc.Root.Elements().ElementAt(1).Value;
                passLocal = objDoc.Root.Elements().ElementAt(2).Value;
                try
                {
                    DoLogin();
                }
                catch
                {
                    MessageBoxResult tmp = MessageBox.Show("Không thể kết nối đến server");
                    Application.Current.Shutdown();
                }
            }

            MainScreen();
            StatisticalScreen();
            JobCalendarScreen();
            FindScreen();
            CustomerScreen();
            AgentScreen();
            SignUp();
            PersonalInforScreen();
            InforScreen();
            LoadCalendar();

            DateTime now = DateTime.Now;
            tbTime.Text = "Ngày giờ:   " + now.ToShortTimeString() + "        " + now.ToShortDateString();

            timer = new Timer();
            timer.Interval = ((60 - DateTime.Now.Second) * 1000 - DateTime.Now.Millisecond);
            timer.AutoReset = true;
            timer.Elapsed += (Object o, ElapsedEventArgs e) =>
            {
                timer.Interval = ((60 - DateTime.Now.Second) * 1000 - DateTime.Now.Millisecond);
                timer.Start();
                this.Dispatcher.Invoke(() =>
                {
                    tbTime.Text = "Ngày giờ:   " + DateTime.Now.ToShortTimeString() + "        " + DateTime.Now.ToShortDateString();
                });
            };
            timer.Start();
        }

        class Name
        {
            public string name { get; set; }
        }

        public void LoadCalendar()
        {
            //load các thứ
            List<Name> listday = new List<Name>();
            listday.Add(new Name() { name = "Thứ 2" });
            listday.Add(new Name() { name = "Thứ 3" });
            listday.Add(new Name() { name = "Thứ 4" });
            listday.Add(new Name() { name = "Thứ 5" });
            listday.Add(new Name() { name = "Thứ 6" });
            listday.Add(new Name() { name = "Thứ 7" });
            listday.Add(new Name() { name = "Chủ nhật" });
            lvcalendar0.ItemsSource = listday;
            List<Name> ListName = new List<Name>();
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            XDocument objDoc = XDocument.Load(path + "/calendar.xml");
            foreach (var item in objDoc.Descendants("Name"))
            {
                ListName.Add(new Name() { name = item.Value });

            }
            lvcalendar1.ItemsSource = ListName.GetRange(0, 7);
            lvcalendar2.ItemsSource = ListName.GetRange(7, 7);
            lvcalendar3.ItemsSource = ListName.GetRange(14, 7);

        }

        public void MainScreen()
        {
            //thanh ngang đầu tiên
            stpTitle.Height = height005;

            txbCoffee.Width = 0.23 * width;
            txbCoffee.FontSize = height004;

            txbMilktea.Width = 0.23 * width;
            txbMilktea.FontSize = height004;

            txbTopping.Width = 0.23 * width;
            txbTopping.FontSize = height004;

            temp1000.Width = 0.005 * width;

            txbInfor.Width = 0.26 * width;
            txbInfor.FontSize = height004;

            cbbEmployee.Width = 0.045 * width;
            cbbEmployee.FontSize = height004;

            cbbManage.Width = 0.045 * width;
            cbbManage.FontSize = height004;

            //menu
            stpMenu.Height = height - stpTitle.Height - 10;
            stpDrink.Height = stpMenu.Height;
            stpDrink.Width = 0.69 * width;

            lvMenuCoffees.Height = stpMenu.Height;
            lvMenuCoffees.Width = stpDrink.Width / 3;

            lvMenuMilkteas.Height = stpMenu.Height;
            lvMenuMilkteas.Width = stpDrink.Width / 3;

            lvMenuToppings.Height = stpMenu.Height;
            lvMenuToppings.Width = stpDrink.Width / 3;

            temp1001.Width = 0.01 * width;
            //InforBill
            gridInforBill.Width = 0.308 * width;
            gridInforBill.Height = height - height005;

            Double heighttbBill = gridInforBill.Height * 3 / 60;

            temp1002.Height = heighttbBill * 0.75;
            temp1002.Width = gridInforBill.Width - 5;

            tbCustomer.FontSize = height0027;
            tbCustomer.Height = heighttbBill;
            tbCustomer.Width = gridInforBill.Width * 2 / 6 - 5;

            cbbCustomer.FontSize = height0027;
            cbbCustomer.Height = heighttbBill;
            cbbCustomer.Width = gridInforBill.Width * 4 / 6 - 5;

            temp1.Height = heighttbBill / 4;
            temp1.Width = gridInforBill.Width;

            tbEmployee.FontSize = height0027;
            tbEmployee.Height = heighttbBill;
            tbEmployee.Width = gridInforBill.Width - 10;

            tbTime.FontSize = height0027;
            tbTime.Height = heighttbBill;
            tbTime.Width = gridInforBill.Width - 10;

            lvListBill.FontSize = height002;
            lvListBill.Height = gridInforBill.Height * 7.7 / 13;
            lvListBill.Width = gridInforBill.Width;

            temp2.Height = heighttbBill / 4;
            temp2.Width = gridInforBill.Width;

            tbTotalMoney.FontSize = height0028;
            tbTotalMoney.Height = heighttbBill;
            tbTotalMoney.Width = gridInforBill.Width * 6 / 8 - 5;

            btnPay.FontSize = height0028;
            btnPay.Height = heighttbBill;
            btnPay.Width = gridInforBill.Width * 2 / 8;

            tbGetMoney.FontSize = height0028;
            tbGetMoney.Height = heighttbBill;
            tbGetMoney.Width = gridInforBill.Width * 2 / 6 - 5;

            tbxGetMoney.FontSize = height0028;
            tbxGetMoney.Height = heighttbBill;
            tbxGetMoney.Width = gridInforBill.Width * 4 / 6 - 5;

            temp3.Height = heighttbBill / 4;
            temp3.Width = gridInforBill.Width;

            tbPayMoney.FontSize = height0028;
            tbPayMoney.Height = heighttbBill;
            tbPayMoney.Width = gridInforBill.Width - 10;


        }

        public void StatisticalScreen()
        {
            rbByDay.Visibility = Visibility.Hidden;
            rbByMonth.Visibility = Visibility.Hidden;
            rbByYear.Visibility = Visibility.Hidden;
            cvStatisticalTime.Visibility = Visibility.Hidden;
            cvSelectSaleStatistical.Visibility = Visibility.Hidden;
            cvStatisticalTimeTop10.Visibility = Visibility.Hidden;

        }

        public void JobCalendarScreen()
        {

            temp31.Width = width / 30;

            tbDayStartCalendar.FontSize = height0028;
            tbDayStartCalendar.Height = height004;
            tbDayStartCalendar.Width = width / 6;

            dpDayStartCalendar.FontSize = height002;
            dpDayStartCalendar.Height = height005;
            dpDayStartCalendar.Width = width / 10;

            temp32.Width = width / 9 * 5;
            temp33.Width = width * 0.06;

            gridCalendar.Width = width * 0.85;
            gridCalendar.Height = height * 0.9;

            lvcalendar0.FontSize = height003;

            //giờ làm
            tbTime1.FontSize = height0025;
            tbTime2.FontSize = height0025;
            tbTime3.FontSize = height0025;



        }

        public void FindScreen()
        {
            //hàng đầu tiên
            temp41.Width = width / 90;

            tbTimestartFind.FontSize = height002;
            tbTimestartFind.Height = height003;
            tbTimestartFind.Width = width / 90 * 4;

            dpDayStartFind.FontSize = height0018;
            dpDayStartFind.Height = height005;
            dpDayStartFind.Width = width / 90 * 6;

            temp42.Width = width / 90 * 7;

            tbTimeEndFind.FontSize = height002;
            tbTimeEndFind.Height = height003;
            tbTimeEndFind.Width = width / 90 * 4;

            dpDayEndFind.FontSize = height0018;
            dpDayEndFind.Height = height005;
            dpDayEndFind.Width = width / 90 * 6;

            temp45.Width = width / 90;

            /*cbbTypeFind.FontSize = height0018;
            cbbTypeFind.Height = height004;
            cbbTypeFind.Width = width / 90 * 6;*/

            temp46.Width = width / 90 * 6.5;

            tbEmployeeName.FontSize = height002;
            tbEmployeeName.Height = height003;

            temp43.Width = width / 90;
            temp44.Width = width / 90;


            tbPhoneCustomer.FontSize = height002;
            tbPhoneCustomer.Height = height003;


            /* tbxSearchFind.FontSize = height0018;
             tbxSearchFind.Height = height005;
             tbxSearchFind.Width = width / 30 * 8.5;

            */ //temp47.Width = width / 180 * 3;

            //list Find
            temp48.Width = width * 0.05;

            gridFind.Height = height * 0.9;
            gridFind.Width = width * 0.9;

            lvListFind.Height = height * 0.9;
            lvListFind.Width = width * 0.7;

            gvcFindSTT.Width = lvListFind.Width / 15 * 2;
            gvcFindDate.Width = lvListFind.Width / 10 * 2;
            gvcFindBillCode.Width = lvListFind.Width / 10 * 2;
            gvcFindEmployeeCode.Width = lvListFind.Width / 10 * 2;
            gvcFindCustomerCode.Width = lvListFind.Width / 10 * 2;
            gvcFindMoney.Width = lvListFind.Width / 10 * 2;

        }

        public void CustomerScreen()
        {
            //hàng đầu tiên
            temp51.Width = width / 10;

            tbxSearchCustomer.FontSize = height0018;
            tbxSearchCustomer.Height = height005;
            tbxSearchCustomer.Width = width / 2;

            temp57.Width = width / 5;

            //list thông tin khách hàng
            temp59.Height = height * 0.15;
            temp59.Width = width;

            temp58.Width = width * 0.1;

            gridCustomer.Height = height * 0.85;
            gridCustomer.Width = width * 0.8;

            stpInforCustomer.Height = gridCustomer.Height;
            stpInforCustomer.Width = gridCustomer.Width;

            stpInforCustomer1.Height = stpInforCustomer.Height;
            stpInforCustomer1.Width = stpInforCustomer.Width / 2;

            stpInforCustomer2.Height = stpInforCustomer.Height;
            stpInforCustomer2.Width = stpInforCustomer.Width / 2;

            //Mã khách hàng
            tbCustomerCode.FontSize = height0028;

            tbxCustomerCode.FontSize = height0028;
            tbxCustomerCode.Width = stpInforCustomer1.Width * 0.6;

            temp491.Height = stpInforCustomer1.Height / 12;

            //Tên khách hàng
            tbCustomerName.FontSize = height0028;
            tbxCustomerName.FontSize = height0028;
            tbxCustomerName.Width = stpInforCustomer1.Width * 0.6;

            temp492.Height = stpInforCustomer1.Height / 12;

            //Điểm
            tbScore.FontSize = height0028;
            tbxScore.FontSize = height0028;
            tbxScore.Width = stpInforCustomer1.Width * 0.6;

            temp493.Height = stpInforCustomer1.Height / 12;

            //Cập nhật
            btnCustomerUpdate.FontSize = height003;
            btnCustomerUpdate.Height = height005;

            //Số điện thoại
            tbPhoneNumber.FontSize = height0028;
            tbxPhoneNumber.FontSize = height0028;
            tbxPhoneNumber.Width = stpInforCustomer2.Width * 0.6;

            temp494.Height = stpInforCustomer1.Height / 12;

            //Ngày sinh
            tbBirthDay.FontSize = height0028;
            tbxBirthDay.FontSize = height0028;
            tbxBirthDay.Width = stpInforCustomer2.Width * 0.6;

            temp495.Height = stpInforCustomer1.Height / 12;

            //CMND
            tbID.FontSize = height0028;
            tbxID.FontSize = height0028;
            tbxID.Width = stpInforCustomer2.Width * 0.6;

            temp496.Height = stpInforCustomer1.Height / 12;

            //Xóa
            btnCustomerDelete.FontSize = height003;
            btnCustomerDelete.Height = height005;
        }

        public void SignUp()
        {
            temp61.Width = width / 10 * 9;
            temp61.Height = height / 30;
            temp62.Width = width;
            temp62.Height = height / 6;
            temp66.Height = height * 0.5;//số bn cũng được
            temp66.Width = width * 0.09;

            //list thông tin khách hàng
            gridNewCustomer.Height = height * 0.85;
            gridNewCustomer.Width = width * 0.8;

            stpInforNewCustomer.Height = gridNewCustomer.Height;
            stpInforNewCustomer.Width = gridNewCustomer.Width;

            //4 thông tin đầu
            stpInforNewCustomerMini1.Height = stpInforNewCustomer.Height / 2;
            stpInforNewCustomerMini1.Width = stpInforNewCustomer.Width;

            //2 thông tin đầu
            stpInforNewCustomerMini11.Height = stpInforNewCustomerMini1.Height;
            stpInforNewCustomerMini11.Width = stpInforNewCustomerMini1.Width / 2;

            stpInforNewCustomerMini12.Height = stpInforNewCustomerMini1.Height;
            stpInforNewCustomerMini12.Width = stpInforNewCustomerMini1.Width / 2;

            //Tên khách hàng
            tbNewCustomerName.FontSize = height0028;
            tbxNewCustomerName.FontSize = height0028;
            tbxNewCustomerName.Width = stpInforNewCustomerMini11.Width * 0.6;

            temp63.Height = stpInforNewCustomerMini11.Height / 6;

            //CMND
            tbNewID.FontSize = height0028;
            tbxNewID.FontSize = height0028;
            tbxNewID.Width = stpInforNewCustomerMini11.Width * 0.6;

            //Số điện thoại
            tbNewPhoneNumber.FontSize = height0028;
            tbxNewPhoneNumber.FontSize = height0028;
            tbxNewPhoneNumber.Width = stpInforNewCustomerMini12.Width * 0.6;

            temp64.Height = stpInforNewCustomerMini12.Height / 6;

            //Ngày sinh
            tbNewBirthDay.FontSize = height0028;
            tbxNewBirthDay.FontSize = height0028;
            tbxNewBirthDay.Width = stpInforNewCustomerMini12.Width * 0.6;

            //đăng ký
            temp65.Height = stpInforNewCustomer.Height / 50;

            btnSignUp.FontSize = height003;
            btnSignUp.Height = height005;
        }

        public void InforScreen()
        {
            griInforDrinks.Height = height * 0.75;
            griInforDrinks.Width = width * 0.7;

            stpInforDrinksMini.Height = height * 0.7;
            stpInforDrinksMini.Width = width * 0.65;

            stpIngredientmini.Width = stpInforDrinksMini.Width * 0.8;

            tbNameDrink.FontSize = height0023;
            tbxNameDrink.FontSize = height0023;
            tbxNameDrink.Width = stpInforDrinksMini.Width * 0.6;

            cbbIngredients.FontSize = height0023;
            //cbbIngredients.Height = stpInforDrinksMini.Height / 20;
            cbbIngredients.Width = stpInforDrinksMini.Width * 0.25;

            temp71.Height = stpInforDrinksMini.Height / 30;

            tbCostDrink.FontSize = height0023;
            tbxCostDrink.FontSize = height0023;
            tbxCostDrink.Width = stpInforDrinksMini.Width * 0.8;

            temp72.Height = stpInforDrinksMini.Height / 30;

            tbIngredients.FontSize = height0023;
            tbIngredients.Height = height005;

            tbxIngredients.FontSize = height0023;
            tbxIngredients.Height = stpInforDrinksMini.Height / 5 * 2;
            tbxIngredients.Width = stpInforDrinksMini.Width;



            temp100.Height = stpInforDrinksMini.Height / 8;

            stpInforDrinksMini1.Width = stpInforDrinksMini.Width;
            gridbtnDeleteDrink.Width = stpInforDrinksMini1.Width / 4;
            gridbtnUpdateInforDrink.Width = stpInforDrinksMini1.Width / 4;
            gridbtnBackInforDrink.Width = stpInforDrinksMini1.Width / 4;
            gridbtnInsertDrink.Width = stpInforDrinksMini1.Width / 4;


            btnDeleteDrink.FontSize = height0027;
            btnUpdateInforDrink.FontSize = height0027;
            btnBackInforDrink.FontSize = height0027;
            btnInsertDrink.FontSize = height0027;

        }

        public void AgentScreen()
        {
            //hàng đầu tiên
            temp81.Width = width / 10;

            tbxSearchAgent.FontSize = height0018;
            tbxSearchAgent.Height = height005;
            tbxSearchAgent.Width = width / 2;

            temp87.Width = width / 5;

            //list thông tin khách hàng
            temp89.Height = height * 0.15;
            temp89.Width = width;

            temp88.Width = width * 0.1;

            gridAgent.Height = height * 0.85;
            gridAgent.Width = width * 0.8;

            stpInforAgent.Height = gridAgent.Height;
            stpInforAgent.Width = gridAgent.Width;

            stpInforAgent1.Height = stpInforAgent.Height;
            stpInforAgent1.Width = stpInforAgent.Width / 2;

            stpInforAgent2.Height = stpInforAgent.Height;
            stpInforAgent2.Width = stpInforAgent.Width / 2;

            //Mã khách hàng
            tbAgentCode.FontSize = height0028;

            tbxAgentCode.FontSize = height0028;
            tbxAgentCode.Width = stpInforAgent1.Width * 0.6;

            temp891.Height = stpInforAgent1.Height / 12;

            //Tên khách hàng
            tbAgentName.FontSize = height0028;
            tbxAgentName.FontSize = height0028;
            tbxAgentName.Width = stpInforAgent1.Width * 0.6;

            temp892.Height = stpInforAgent1.Height / 12;

            //Điểm
            tbMonth.FontSize = height0028;
            tbxMonth.FontSize = height0028;
            tbxMonth.Width = stpInforAgent1.Width * 0.6;

            temp893.Height = stpInforAgent1.Height / 12;

            //Cập nhật
            btnAgentUpdate.FontSize = height003;
            btnAgentUpdate.Height = height005;

            //Số điện thoại
            tbPhoneNumberAgent.FontSize = height0028;
            tbxPhoneNumberAgent.FontSize = height0028;
            tbxPhoneNumberAgent.Width = stpInforAgent2.Width * 0.6;

            temp894.Height = stpInforAgent1.Height / 12;

            //Ngày sinh
            tbBirthDayAgent.FontSize = height0028;
            tbxBirthDayAgent.FontSize = height0028;
            tbxBirthDayAgent.Width = stpInforAgent2.Width * 0.6;

            temp895.Height = stpInforAgent1.Height / 12;

            //CMND
            tbIDAgent.FontSize = height0028;
            tbxIDAgent.FontSize = height0028;
            tbxIDAgent.Width = stpInforAgent2.Width * 0.6;

            temp896.Height = stpInforAgent1.Height / 12;

            //Xóa
            btnAgentDelete.FontSize = height003;
            btnAgentDelete.Height = height005;
        }

        public void PersonalInforScreen()
        {
            //hàng đầu tiên
            temp91.Width = width / 10 * 8;

            temp92.Width = width;
            temp92.Height = height / 5;

            temp93.Width = width * 0.13;


            gridPersonalInfor.Height = height * 0.85;
            gridPersonalInfor.Width = width * 0.8;

            stpInforPersonalInfor.Height = gridAgent.Height;
            stpInforPersonalInfor.Width = gridAgent.Width;

            stpInforPersonalInfor1.Height = stpInforAgent.Height;
            stpInforPersonalInfor1.Width = stpInforAgent.Width / 2;

            stpInforPersonalInfor2.Height = stpInforAgent.Height;
            stpInforPersonalInfor2.Width = stpInforAgent.Width / 2;

            //Mã nhân viên
            tbPersonalInforCode.FontSize = height0028;

            tbxPersonalInforCode.FontSize = height0028;
            tbxPersonalInforCode.Width = stpInforPersonalInfor1.Width * 0.6;

            temp991.Height = stpInforPersonalInfor1.Height / 12;

            //Tên nhân viên
            tbPersonalInforName.FontSize = height0028;
            tbxPersonalInforName.FontSize = height0028;
            tbxPersonalInforName.Width = stpInforPersonalInfor1.Width * 0.6;

            temp992.Height = stpInforPersonalInfor1.Height / 12;

            //tháng kinh nghiệm
            tbMonthPersonalInfor.FontSize = height0028;
            tbxMonthPersonalInfor.FontSize = height0028;
            tbxMonthPersonalInfor.Width = stpInforPersonalInfor1.Width * 0.6;

            temp993.Height = stpInforPersonalInfor1.Height / 12;


            //Cập nhật
            btnPersonalInforUpdate.FontSize = height003;
            btnPersonalInforUpdate.Height = height005;

            //Số điện thoại
            tbPhoneNumberPersonalInfor.FontSize = height0028;
            tbxPhoneNumberPersonalInfor.FontSize = height0028;
            tbxPhoneNumberPersonalInfor.Width = stpInforPersonalInfor2.Width * 0.6;

            temp994.Height = stpInforPersonalInfor1.Height / 12;

            //Ngày sinh
            tbBirthDayPersonalInfor.FontSize = height0028;
            tbxBirthDayPersonalInfor.FontSize = height0028;
            tbxBirthDayPersonalInfor.Width = stpInforPersonalInfor2.Width * 0.6;

            temp995.Height = stpInforPersonalInfor1.Height / 12;

            //CMND
            tbIDPersonalInfor.FontSize = height0028;
            tbxIDPersonalInfor.FontSize = height0028;
            tbxIDPersonalInfor.Width = stpInforPersonalInfor2.Width * 0.6;

            temp996.Height = stpInforPersonalInfor1.Height / 12;

            //đổi mật khẩu
            btnChangePassword.FontSize = height003;
            btnChangePassword.Height = height005;
        }

        public string DoLogin()
        {
            string type = "";
            bool error = false;
            if (rememberme == false)
            {
                if ((txtUsername.Text == "") && (txtPassword.Password == ""))
                {
                    error = true;
                    tbMessageBox.Text = "Invalid username and password!!!";
                }
                else if (txtUsername.Text == "")
                {
                    error = true;
                    tbMessageBox.Text = "invalid username!!!";
                }
                else if (txtPassword.Password == "")
                {
                    error = true;
                    tbMessageBox.Text = "Invalid password!!!";
                }
            }

            if (error == false)
            {
                string result;
                if (rememberme == true)
                {
                    result = loginRequest(userLocal, passLocal);

                }
                else
                {
                    using (MD5 md5Hash = MD5.Create())
                    {
                        //đăng nhập với password đã mã hóa
                        result = loginRequest(txtUsername.Text, GetMd5Hash(md5Hash, txtPassword.Password));
                    }
                }
                dynamic stuff = JsonConvert.DeserializeObject(result);

                string msg = stuff.msg;
                string code = stuff.code;
                if (code == "0")
                {

                    GridLoginScreen.Visibility = System.Windows.Visibility.Hidden;
                    stpMainScreen.Visibility = System.Windows.Visibility.Visible;
                    loggedInUserType = type = stuff.loaiNV;
                    loggedInUserName = stuff.hoTen;
                    loggedInUserId = stuff.id;

                    cbbEmployee.Visibility = System.Windows.Visibility.Visible;
                    cbbManage.Visibility = System.Windows.Visibility.Visible;
                    if (type == "1")//nhân viên
                    {
                        cbbManage.Visibility = System.Windows.Visibility.Hidden;
                    }
                    if (type == "2")
                    {
                        cbbEmployee.Visibility = System.Windows.Visibility.Hidden;
                    }
                    tbEmployee.Text = "Phục vụ: " + loggedInUserName;

                    if (RememberMe.IsThreeState == true)
                    {
                        string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
                        XDocument objDoc = XDocument.Load(path + "/Rememberme.xml");

                        objDoc.Root.Elements().ElementAt(0).Value = "True";
                        objDoc.Root.Elements().ElementAt(1).Value = txtUsername.Text;

                        using (MD5 md5Hash = MD5.Create())
                        {
                            objDoc.Root.Elements().ElementAt(2).Value = GetMd5Hash(md5Hash, txtPassword.Password);
                        };

                        objDoc.Save(path + "/Rememberme.xml");
                    }
                }
                else
                {
                    tbMessageBox.Text = "Username or password is wrong!!!";
                }
            }
            return type;

        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            DoLogin();
        }

        private string loginRequest(string username, string password)
        {

            string json = "{\"username\": \"" + username + "\", \"password\": \"" + password + "\"}";
            string url = SERVER + "login";

            try
            {
                return Post(url, json);
            }
            catch
            {
                MessageBox.Show("Không thể kết nối đến server");
                return "";
            }
        }

        private void LoadMenu()
        {
            string url = SERVER + "getFoodList";

            string result = Get(url);

            dynamic stuff = JsonConvert.DeserializeObject(result);

            string code = stuff.code;
            List<SanPham> coffees = new List<SanPham>();
            List<SanPham> milkTeas = new List<SanPham>();
            List<SanPham> toppings = new List<SanPham>();

            foreach (var item in stuff.coffees)
            {
                coffees.Add(new SanPham()
                {
                    id = item.id,
                    gia = item.gia,
                    maSanPham = item.maSanPham,
                    tenSanPham = item.tenSanPham,
                    thongTin = item.thongTin
                });
            }
            foreach (var item in stuff.milkTeas)
            {
                milkTeas.Add(new SanPham()
                {
                    id = item.id,
                    gia = item.gia,
                    maSanPham = item.maSanPham,
                    tenSanPham = item.tenSanPham,
                    thongTin = item.thongTin
                });
            }
            foreach (var item in stuff.toppings)
            {
                toppings.Add(new SanPham()
                {
                    id = item.id,
                    gia = item.gia,
                    maSanPham = item.maSanPham,
                    tenSanPham = item.tenSanPham,
                    thongTin = item.thongTin
                });
            }

            lvMenuCoffees.ItemsSource = coffees;
            lvMenuMilkteas.ItemsSource = milkTeas;
            lvMenuToppings.ItemsSource = toppings;
        }

        private void btnPersonalInforMode_Click(object sender, RoutedEventArgs e)
        {
            //hiện thị thông tin cá nhân của nhân viên
            stpMainScreen.Visibility = System.Windows.Visibility.Hidden;
            wrpPersonalInfor.Visibility = System.Windows.Visibility.Visible;
        }

        private void btnRegisterMode_Click(object sender, RoutedEventArgs e)
        {
            //đăng kí khách hàng
            stpMainScreen.Visibility = System.Windows.Visibility.Hidden;
            wrpRegisterNewCustomer.Visibility = System.Windows.Visibility.Visible;
        }

        private void btnLogoutMode_Click(object sender, RoutedEventArgs e)
        {
            //đăng xuất
            stpMainScreen.Visibility = System.Windows.Visibility.Hidden;
            GridLoginScreen.Visibility = System.Windows.Visibility.Visible;
            txtUsername.Text = "";
            txtPassword.Password = "";

            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            XDocument objDoc = XDocument.Load(path + "/Rememberme.xml");

            objDoc.Root.Elements().ElementAt(0).Value = "False";
            objDoc.Save(path + "/Rememberme.xml");
        }

        private void btnStatisticalScreenMode1_Click(object sender, RoutedEventArgs e)
        {
            //thống kê
            stpMainScreen.Visibility = System.Windows.Visibility.Hidden;
            gridStatisticalScreen.Visibility = System.Windows.Visibility.Visible;
        }

        private void btnFindMode1_Click(object sender, RoutedEventArgs e)
        {
            //tìm kiếm
            stpMainScreen.Visibility = System.Windows.Visibility.Hidden;
            wrpFind.Visibility = System.Windows.Visibility.Visible;
        }

        private void btnManageMode1_Click(object sender, RoutedEventArgs e)
        {
            //quản lý nhân viên
            stpMainScreen.Visibility = System.Windows.Visibility.Hidden;
            wrpAgent.Visibility = System.Windows.Visibility.Visible;
        }

        private void btnCustomerInforMode_Click(object sender, RoutedEventArgs e)
        {
            //thông tin khách hàng
            stpMainScreen.Visibility = System.Windows.Visibility.Hidden;
            wrpCustomer.Visibility = System.Windows.Visibility.Visible;
        }

        public class Drink
        {
            public int id { get; set; }
            public int soLuong { get; set; }
            public string size { get; set; }
            public int stt { get; set; }
            public string ten { get; set; }
            public decimal gia { get; set; }
        }

        ObservableCollection<Drink> ListDrinks = new ObservableCollection<Drink>();

        private void btnChoose_Click(object sender, RoutedEventArgs e)
        {
            int idtemp = int.Parse(((TextBlock)((StackPanel)((Grid)((StackPanel)((Button)sender).Parent).Parent).Children[0]).Children[2]).Text);
            string sizetemp = ((ComboBoxItem)((ComboBox)((StackPanel)((Grid)((StackPanel)((Button)sender).Parent).Parent).Children[0]).Children[1]).SelectedItem).Content.ToString();
            string tentemp = ((TextBlock)((StackPanel)((Grid)((StackPanel)((Button)sender).Parent).Parent).Children[0]).Children[0]).Text;
            decimal giatemp = decimal.Parse(((TextBlock)((StackPanel)((Grid)((StackPanel)((Button)sender).Parent).Parent).Children[0]).Children[3]).Text);
            bool temp = false;
            foreach (var item in ListDrinks)
            {
                if ((item.id == idtemp) && (item.size == sizetemp))
                {
                    item.soLuong++;
                    temp = true;
                    break;
                }

            }
            if (temp == false)
            {
                if (sizetemp == "M")
                {
                    giatemp = giatemp * 110 / 100;
                }
                if (sizetemp == "L")
                {
                    giatemp = giatemp * 120 / 100;
                }
                giatemp = Math.Round(giatemp / 1000, 0) * 1000;

                ListDrinks.Add(new Drink()
                {
                    id = idtemp,
                    soLuong = 1,
                    size = sizetemp,
                    gia = giatemp,
                    ten = tentemp,
                    stt = ListDrinks.Count() + 1,
                });

                ((Button)((StackPanel)((Grid)((StackPanel)((Button)sender).Parent).Parent).Children[1]).Children[1]).Visibility = System.Windows.Visibility.Visible;
            }
            lvListBill.ClearValue(ListView.ItemsSourceProperty);
            lvListBill.ItemsSource = ListDrinks;
            TongTien();
        }

        private void btnPay_Click(object sender, RoutedEventArgs e)
        {
            if (ListDrinks.Count == 0)
            {
                MessageBox.Show("Đơn hàng trống");
            }
            else
            {
                const string prefix = "__idKhachHang";
                string gia = TongTien().ToString();//"usercfrnh"
                string idKhachHangMua = ((ComboBoxItem)cbbCustomer.SelectedItem).Name.Substring(prefix.Length);//"13874383";

                if (idKhachHangMua == "null")
                {
                    idKhachHangMua = "";
                }

                string json = "{\"gia\": " + gia + ", \"idKhachHangMua\": \"" + idKhachHangMua + "\", \"idNhanVienLap\": " + loggedInUserId + ", \"danhSachMonAn\":";

                List<Drink> listtemp = new List<Drink>();
                
                foreach (Drink ld in ListDrinks)
                {
                    bool flag = false;
                    foreach (Drink d in listtemp)
                    {
                        if (ld.id == d.id)
                        {
                            d.soLuong += ld.soLuong;
                            flag = true;
                        }
           
                    }

                    if (flag == false)
                    {
                        listtemp.Add(ld);
                    }
                }

                json += JsonConvert.SerializeObject(listtemp);
                json += "}";


                string url = SERVER + "putOrder";
                try
                {
                    string result = Post(url, json);
                    dynamic resObject = JsonConvert.DeserializeObject(result);
                    if (resObject.code == "0")
                    {
                        MessageBox.Show("Đơn hàng đã được xác nhận");
                        ListDrinks.Clear();
                        lvListBill.ItemsSource = null;
                        tbTotalMoney.Text = " Tổng tiền:        ";

                        lvMenuCoffees.Items.Refresh();
                        lvMenuMilkteas.Items.Refresh();
                        lvMenuToppings.Items.Refresh();

                    }
                    else
                    {
                        MessageBox.Show("Đặt hàng thất bại, vui lòng thử lại");
                    }
                }
                catch
                {
                    MessageBox.Show("Không thể kết nối đến server");
                }
            }
        }

        private void btnMinus_Click(object sender, RoutedEventArgs e)
        {
            int idtemp = int.Parse(((TextBlock)((StackPanel)((Grid)((StackPanel)((Button)sender).Parent).Parent).Children[0]).Children[2]).Text);
            string sizetemp = ((ComboBoxItem)((ComboBox)((StackPanel)((Grid)((StackPanel)((Button)sender).Parent).Parent).Children[0]).Children[1]).SelectedItem).Content.ToString();
            int i = 0;
            foreach (var item in ListDrinks)
            {
                if ((item.id == idtemp) && (item.size == sizetemp))
                {
                    if (item.soLuong > 1)
                    {
                        item.soLuong--;
                    }
                    else
                    {
                        ListDrinks.RemoveAt(i);
                        for (int j = i; j < ListDrinks.Count; j++)
                        {
                            ListDrinks[j].stt--;
                        }
                        ((Button)((StackPanel)((Grid)((StackPanel)((Button)sender).Parent).Parent).Children[1]).Children[1]).Visibility = System.Windows.Visibility.Hidden;
                    }
                    break;
                }
                i++;
            }
            lvListBill.ClearValue(ListView.ItemsSourceProperty);
            lvListBill.ItemsSource = ListDrinks;
            TongTien();
        }

        private void TbxSearchCustomer_KeyUp(object sender, KeyEventArgs e)
        {
            bool found = false;
            var border = (resultStack.Parent as ScrollViewer).Parent as Border;

            string query = (sender as TextBox).Text;

            if (query.Length == 0)
            {
                // Clear   
                resultStack.Children.Clear();
                border.Visibility = Visibility.Hidden;
            }
            else
            {
                border.Visibility = Visibility.Visible;
                try
                {
                    string res = Get(SERVER + "getCustomerByPhone/" + query);
                    dynamic resObject = JsonConvert.DeserializeObject(res);
                    // Clear the list   
                    resultStack.Children.Clear();

                    if (resObject.code == "0")
                    {
                        for (int i = 0; i < resObject.payload.Count; i++)
                        {
                            string text = resObject.payload[i].hoTen;

                            // The word starts with this... Autocomplete must work   
                            TextBlock block = new TextBlock();

                            // Add the text   
                            block.Text = text;
                            //Add id
                            string fullPhoneNumber = ((string)resObject.payload[i].soDienThoai).Trim();
                            block.Name = "_" + fullPhoneNumber;

                            // A little style...   
                            block.Margin = new Thickness(2, 3, 2, 3);
                            block.Cursor = Cursors.Hand;

                            // Mouse events   
                            block.MouseLeftButtonUp += (s, notCare) =>
                            {
                                tbxSearchCustomer.Text = (s as TextBlock).Name.Remove(0, 1);
                                border.Visibility = Visibility.Hidden;
                                BtnCustomer_Click(null, null);
                            };

                            block.MouseEnter += (s, notCare) =>
                            {
                                TextBlock b = s as TextBlock;
                                b.Background = Brushes.PeachPuff;
                            };

                            block.MouseLeave += (s, notCare) =>
                            {
                                TextBlock b = s as TextBlock;
                                b.Background = Brushes.Transparent;
                            };

                            // Add to the panel   
                            resultStack.Children.Add(block);
                            found = true;
                        }
                    }

                    if (!found)
                    {
                        resultStack.Children.Add(new TextBlock() { Text = "No results found." });
                    }
                }
                catch
                {
                    MessageBox.Show("Không thể kết nối đến server");
                }
            }

        }

        private void BtnCustomer_Click(object sender, RoutedEventArgs e)
        {
            string phoneNumber = tbxSearchCustomer.Text;
            dynamic resObject;

            if (phoneNumber.Length == 0)
            {
                MessageBox.Show("Vui lòng nhập số điện thoại để tìm kiếm");
                return;
            }

            try
            {
                string res = Get(SERVER + "getCustomerByPhone/" + phoneNumber);
                resObject = JsonConvert.DeserializeObject(res);
            }
            catch
            {
                MessageBox.Show("Không thể kết nối đến server");
                return;
            }

            if (resObject.code == "0")
            {
                tbxCustomerId.Text = resObject.payload[0].id;
                tbxCustomerCode.Text = resObject.payload[0].maKH;
                tbxCustomerName.Text = resObject.payload[0].hoTen;
                tbxScore.Text = resObject.payload[0].diemTichLuy;
                tbxPhoneNumber.Text = resObject.payload[0].soDienThoai;
                tbxBirthDay.Text = resObject.payload[0].ngaySinh;
                tbxID.Text = resObject.payload[0].cmnd;
            }
            else if (resObject.code == "-4")
            {
                MessageBox.Show("Không tìm thấy khách hàng");
            }
            else
            {
                MessageBox.Show("Đã xảy ra lỗi, vui lòng bấm lại nút Tìm kiếm");
            }
        }

        private void BtnCustomerDelete_Click(object sender, RoutedEventArgs e)
        {
            string id = tbxCustomerId.Text;

            if (id.Length == 0)
            {
                MessageBox.Show("Vui lòng tìm kiếm khách hàng trước khi xoá");
                return;

            }

            string customerName = tbxCustomerName.Text;
            string messageBoxText = "Bạn có chắc chắn muốn xoá khách hàng \"" + customerName + "\"?";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            // Display message box
            MessageBoxResult result = MessageBox.Show(messageBoxText, "", button, icon);
            // Process message box results
            switch (result)
            {
                case MessageBoxResult.Yes:
                    dynamic resObject;
                    try
                    {
                        string res = Get(SERVER + "deleteCustomer/" + id);
                        resObject = JsonConvert.DeserializeObject(res);
                    }
                    catch
                    {
                        MessageBox.Show("Không thể kết nối đến server");
                        break;
                    }
                    if (resObject.code == "0")
                    {
                        MessageBox.Show("Xoá khách hàng thành công!");
                        tbxCustomerId.Text = "";
                        tbxCustomerCode.Text = "";
                        tbxCustomerName.Text = "";
                        tbxScore.Text = "";
                        tbxPhoneNumber.Text = "";
                        tbxBirthDay.Text = "";
                        tbxID.Text = "";
                    }
                    else if (resObject.code == "-4")
                    {
                        MessageBox.Show("Khách hàng không tồn tại");

                    }
                    else
                    {
                        MessageBox.Show("Đã xảy ra lỗi, vui lòng thử lại!");
                    }

                    break;
                default:
                    break;
            }
        }

        private void BtnCustomerUpdate_Click(object sender, RoutedEventArgs e)
        {
            string id = tbxCustomerId.Text;

            if (id.Length == 0)
            {
                MessageBox.Show("Vui lòng tìm kiếm khách hàng trước khi cập nhật");
                return;

            }

            string payload = $"{{\"id\": {tbxCustomerId.Text},\"maKH\": \"{tbxCustomerCode.Text}\",\"hoTen\": \"{tbxCustomerName.Text}\",\"diemTichLuy\": {tbxScore.Text},\"soDienThoai\": \"{ tbxPhoneNumber.Text}\",\"ngaySinh\": \"{tbxBirthDay.Text}\",\"cmnd\": \"{tbxID.Text}\"}}";
            dynamic resObject;
            try
            {
                string res = Post(SERVER + "updateCustomer", payload);
                resObject = JsonConvert.DeserializeObject(res);
            }
            catch
            {
                MessageBox.Show("Không thể kết nối đến server");
                return;
            }

            if (resObject.code == "0")
            {
                MessageBox.Show("Cập nhật thông tin khách hàng thành công");
            }
            else
            {
                MessageBox.Show("Đã xảy ra lỗi, vui lòng thử lại");
            }
        }

        public decimal TongTien()
        {
            decimal tongTien = 0;
            foreach (var item in ListDrinks)
            {
                tongTien += item.gia * item.soLuong;
            }
            decimal tmp = tongTien;
            tbTotalMoney.Text = " Tổng tiền:        " + tongTien.ToString();
            return tongTien;
        }

        public void TienCanTra()
        {
            decimal tiennhan = 0;

            if (decimal.TryParse(tbxGetMoney.Text, out tiennhan))
            {
                tbPayMoney.Text = "    Tiền trả:        " + (tiennhan - TongTien()).ToString();
            }
            else
            {
                tbPayMoney.Text = "    Tiền trả:        " + "Tiền nhận không hợp lệ!!!";

            }
        }

        private void tbxGetMoney_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            TienCanTra();
        }

        private void BtnUpdateCalendar_Click(object sender, RoutedEventArgs e)
        {
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            XDocument objDoc = XDocument.Load(path + "/calendar.xml");
            objDoc.Root.RemoveNodes();

            foreach (Name item in lvcalendar1.ItemsSource)
            {
                objDoc.Root.Add(new XElement("Name", item.name));
            }
            foreach (Name item in lvcalendar2.ItemsSource)
            {
                objDoc.Root.Add(new XElement("Name", item.name));
            }
            foreach (Name item in lvcalendar3.ItemsSource)
            {
                objDoc.Root.Add(new XElement("Name", item.name));
            }

            objDoc.Save(path + "/calendar.xml");
            MessageBox.Show("Cập nhập lịch làm việc thành công!");
        }

        private void CbbSize_DropDownClosed(object sender, EventArgs e)
        {
            int idtemp = int.Parse(((TextBlock)((StackPanel)((Grid)((StackPanel)((ComboBox)sender).Parent).Parent).Children[0]).Children[2]).Text);
            string sizetemp = ((ComboBoxItem)((ComboBox)((StackPanel)((Grid)((StackPanel)((ComboBox)sender).Parent).Parent).Children[0]).Children[1]).SelectedItem).Content.ToString();
            ((Button)((StackPanel)((Grid)((StackPanel)((ComboBox)sender).Parent).Parent).Children[1]).Children[1]).Visibility = System.Windows.Visibility.Hidden;
            foreach (var item in ListDrinks)
            {
                if ((item.id == idtemp) && (item.size == sizetemp))
                {
                    ((Button)((StackPanel)((Grid)((StackPanel)((ComboBox)sender).Parent).Parent).Children[1]).Children[1]).Visibility = System.Windows.Visibility.Visible;
                    break;
                }
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            ((WrapPanel)((Grid)((WrapPanel)((Button)sender).Parent).Parent).Children[1]).Visibility = Visibility.Visible;
            ((WrapPanel)((Button)sender).Parent).Visibility = Visibility.Hidden;
        }

        private void btnAgent_Click(object sender, RoutedEventArgs e)
        {
            string nameOfEmployee = tbxSearchAgent.Text;
            dynamic resObject;

            if (nameOfEmployee.Length == 0)
            {
                MessageBox.Show("Vui lòng nhập tên để tìm kiếm");
                return;
            }

            try
            {
                string res = Get(SERVER + "getEmployeeByName/" + nameOfEmployee);
                resObject = JsonConvert.DeserializeObject(res);
            }
            catch
            {
                MessageBox.Show("Không thể kết nối đến server");
                return;
            }

            if (resObject.code == "0")
            {
                tbxAgentCode.Text = resObject.payload[0].maNV;
                tbxAgentName.Text = resObject.payload[0].hoTen;
                tbxMonth.Text = resObject.payload[0].soThangKinhNghiem;
                tbxPhoneNumberAgent.Text = resObject.payload[0].soDienThoai;
                tbxBirthDayAgent.Text = resObject.payload[0].ngaySinh;
                tbxIDAgent.Text = resObject.payload[0].cmnd;
            }
            else if (resObject.code == "-4")
            {
                MessageBox.Show("Không tìm thấy nhân viên nào");
            }
            else
            {
                MessageBox.Show("Đã xảy ra lỗi, vui lòng bấm lại nút Tìm kiếm");
            }
        }

        private void btnAgentUpdate_Click(object sender, RoutedEventArgs e)
        {
            string name = tbAgentName.Text;

            if (name.Length == 0)
            {
                MessageBox.Show("Vui lòng tìm kiếm nhân viên trước khi cập nhật");
                return;

            }

            string payload = $"{{\"maNV\": \"{tbxAgentCode.Text}\",\"hoTen\": \"{tbxAgentName.Text}\",\"kinhNghiem\": {tbxMonth.Text},\"soDienThoai\": \"{ tbxPhoneNumberAgent.Text}\",\"ngaySinh\": \"{tbxBirthDayAgent.Text}\",\"cmnd\": \"{tbxIDAgent.Text}\"}}";
            dynamic resObject;
            try
            {
                string res = Post(SERVER + "updateEmployee", payload);
                resObject = JsonConvert.DeserializeObject(res);
            }
            catch
            {
                MessageBox.Show("Không thể kết nối đến server");
                return;
            }

            if (resObject.code == "0")
            {
                MessageBox.Show("Cập nhật thông tin nhân viên thành công");
            }
            else
            {
                MessageBox.Show("Đã xảy ra lỗi, vui lòng thử lại");
            }
        }

        private void btnAgentDelete_Click(object sender, RoutedEventArgs e)
        {
            string name = tbxAgentName.Text;

            if (name.Length == 0)
            {
                MessageBox.Show("Vui lòng tìm kiếm nhân viên trước khi xoá");
                return;

            }

            //string name = tbxAgentName.Text;
            string messageBoxText = "Bạn có chắc chắn muốn xoá nhân viên \"" + name + "\"?";
            MessageBoxButton button = MessageBoxButton.YesNo;
            MessageBoxImage icon = MessageBoxImage.Warning;
            // Display message box
            MessageBoxResult result = MessageBox.Show(messageBoxText, "", button, icon);
            // Process message box results
            switch (result)
            {
                case MessageBoxResult.Yes:
                    dynamic resObject;
                    try
                    {
                        string res = Get(SERVER + "deleteEmployee/" + name);
                        resObject = JsonConvert.DeserializeObject(res);
                    }
                    catch
                    {
                        MessageBox.Show("Không thể kết nối đến server");
                        break;
                    }
                    if (resObject.code == "0")
                    {
                        MessageBox.Show("Xoá nhân viên thành công!");
                        tbxAgentCode.Text = "";
                        tbxAgentName.Text = "";
                        tbMonth.Text = "";
                        tbxPhoneNumberAgent.Text = "";
                        tbxBirthDayAgent.Text = "";
                        tbxIDAgent.Text = "";
                    }
                    else if (resObject.code == "-4")
                    {
                        MessageBox.Show("Nhân viên không tồn tại");

                    }
                    else
                    {
                        MessageBox.Show("Đã xảy ra lỗi, vui lòng thử lại!");
                    }

                    break;
                default:
                    break;
            }

        }

        private void BtnCalendarMode_Click(object sender, RoutedEventArgs e)
        {
            wrpJobCalendar.Visibility = Visibility.Visible;
            btnUpdateCalendar.Visibility = Visibility.Visible;
            if (loggedInUserType == "1")
            {
                btnUpdateCalendar.Visibility = Visibility.Hidden;
            }
        }

        private void TbDrink_MouseDown(object sender, MouseButtonEventArgs e)
        {
            griInforDrinks.Visibility = Visibility.Visible;
            stpMainScreen.Opacity = 0.1;
            tbIdDrink.Text = ((SanPham)((TextBlock)sender).DataContext).id.ToString();
            tbxNameDrink.Text = ((TextBlock)sender).Text;
            tbxCostDrink.Text = ((TextBlock)((StackPanel)((TextBlock)sender).Parent).Children[3]).Text;
            tbxIngredients.Text = ((TextBlock)((StackPanel)((TextBlock)sender).Parent).Children[4]).Text;
            btnInsertDrink.Content = "Thêm mới";
            cbbIngredients.Visibility = Visibility.Hidden;

            btnDeleteDrink.Visibility = Visibility.Visible;
            btnUpdateInforDrink.Visibility = Visibility.Visible;
            btnInsertDrink.Visibility = Visibility.Visible;
            if (loggedInUserType == "1")
            {
                btnDeleteDrink.Visibility = Visibility.Collapsed;
                btnUpdateInforDrink.Visibility = Visibility.Collapsed;
                btnInsertDrink.Visibility = Visibility.Collapsed;
            }

        }

        private void BtnBackInforDrink_Click(object sender, RoutedEventArgs e)
        {
            griInforDrinks.Visibility = Visibility.Hidden;
            stpMainScreen.Opacity = 1;
        }

        private void BtnDeleteDrink_Click(object sender, RoutedEventArgs e)
        {
            string id = tbIdDrink.Text;
            string url = SERVER + "deleteDrink/" + id;
            try
            {
                string result = Get(url);
                dynamic stuff = JsonConvert.DeserializeObject(result);

                string code = stuff.code;
                if (code == "0")
                {
                    MessageBox.Show("xóa món thành công");
                    griInforDrinks.Visibility = Visibility.Hidden;
                    ListDrinks.Clear();
                    LoadMenu();
                    tbTotalMoney.Text = " Tổng tiền:        ";
                    stpMainScreen.Opacity = 1;
                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra, vui lòng thử lại");
                }
            }
            catch
            {
                MessageBox.Show("Không thể kết nối đến server");

            }
        }

        private void BtnUpdateInforDrink_Click(object sender, RoutedEventArgs e)
        {
            string id = tbIdDrink.Text;
            string NameDrink = tbxNameDrink.Text;//"namedrink"
            string Cost = tbxCostDrink.Text;//"giá"
            string Ingredients = tbxIngredients.Text;//mô tả
            string json = "{\"id\": \"" + id + "\",\"tenSanPham\": \"" + NameDrink + "\",\"thongTin\": \"" + Ingredients + "\", \"gia\": " + Cost + "}";
            string url = SERVER + "updateDrink";
            try
            {
                string result = Post(url, json);
                dynamic stuff = JsonConvert.DeserializeObject(result);

                string code = stuff.code;
                if (code == "0")
                {
                    MessageBox.Show("Cập nhật món thành công");
                    griInforDrinks.Visibility = Visibility.Hidden;
                    ListDrinks.Clear();
                    LoadMenu();
                    tbTotalMoney.Text = " Tổng tiền:        ";
                    stpMainScreen.Opacity = 1;

                }
                else
                {
                    MessageBox.Show("Có lỗi xảy ra, vui lòng thử lại");
                }
            }
            catch
            {
                MessageBox.Show("Không thể kết nối đến server");
            }
        }

        private void RememberMe_Click(object sender, RoutedEventArgs e)
        {
            RememberMe.IsThreeState = RememberMe.IsThreeState ? false : true;
        }

        private void BtnSignUp_Click(object sender, RoutedEventArgs e)
        {
            string name = tbxNewCustomerName.Text;
            string cmnd = tbxNewID.Text;
            string phone = tbxNewPhoneNumber.Text;
            string birthDay = tbxNewBirthDay.Text;

            if (name.Length == 0)
            {
                MessageBox.Show("Họ và tên là bắt buộc");
                return;

            }
            if (cmnd.Length == 0)
            {
                MessageBox.Show("Chứng minh nhân dân là bắt buộc");
                return;

            }
            if (phone.Length == 0)
            {
                MessageBox.Show("Số điện thoại là bắt buộc");
                return;

            }
            if (birthDay.Length == 0)
            {
                MessageBox.Show("Ngày sinh là bắt buộc");
                return;

            }
            try
            {
                DateTime.ParseExact(birthDay, "dd/MM/yyyy", null);
            }
            catch
            {
                MessageBox.Show("Ngày sinh không hợp lệ");
                return;
            }

            string payload = $"{{\"hoTen\": \"{name}\",\"ngaySinh\": \"{birthDay}\",\"soDienThoai\": \"{phone}\",\"cmnd\": \"{ cmnd}\"}}";
            dynamic resObject;
            try
            {
                string res = Post(SERVER + "addCustomer", payload);
                resObject = JsonConvert.DeserializeObject(res);
            }
            catch
            {
                MessageBox.Show("Không thể kết nối đến server");
                return;
            }

            if (resObject.code == "0")
            {
                MessageBox.Show("Tạo khách hàng thành công");
            }
            else if (resObject.code == "-4")
            {
                MessageBox.Show("Số điện thoại đã tồn tại");
            }
            else
            {
                MessageBox.Show("Đã xảy ra lỗi, vui lòng thử lại");
            }

        }

        private void btnPersonalInforUpdate_Click(object sender, RoutedEventArgs e)
        {
            string payload = $"{{\"id\": {loggedInUserId},\"maNV\": \"{tbxPersonalInforCode.Text}\",\"hoTen\": \"{tbxPersonalInforName.Text}\",\"kinhNghiem\": {tbxMonthPersonalInfor.Text},\"soDienThoai\": \"{ tbxPhoneNumberPersonalInfor.Text}\",\"ngaySinh\": \"{tbxBirthDayPersonalInfor.Text}\",\"cmnd\": \"{tbxIDPersonalInfor.Text}\"}}";
            dynamic resObject;
            try
            {
                string res = Post(SERVER + "updateEmployee", payload);
                resObject = JsonConvert.DeserializeObject(res);
            }
            catch
            {
                MessageBox.Show("Không thể kết nối đến server");
                return;
            }

            if (resObject.code == "0")
            {
                MessageBox.Show("Cập nhật thông tin nhân viên thành công");
            }
            else
            {
                MessageBox.Show("Đã xảy ra lỗi, vui lòng thử lại");
            }
        }

        public void DoChangePass()
        {
            if ((txtCurrentPass.Password == "") && (txtNewPassword.Password == ""))
            {
                tbMessageWarning.Text = "invalid password!!!";
            }
            else if (txtCurrentPass.Password == "")
            {
                tbMessageWarning.Text = "invalid current password!!!\nPress cancel to return !!!";
            }
            else if (txtNewPassword.Password == "")
            {
                tbMessageWarning.Text = "invalid new password.\nPress cancel to return !!!";
            }
            else if (txtConfirmPass.Password == "")
            {
                tbMessageWarning.Text = "Please confirm your password !!!";
            }
            else if (txtConfirmPass.Password != txtNewPassword.Password)
            {
                tbMessageWarning.Text = "Please enter password again !!!";
            }
            else //Hoặc check chỗ này
            {
                try
                {
                    dynamic resObject;
                    string currentPass = GetMd5Hash(MD5.Create(), txtCurrentPass.Password);
                    string newPass = GetMd5Hash(MD5.Create(), txtNewPassword.Password);
                    //string newPassConfirm = TODO: Thêm ô confirm new pass
                    //Check newPass = newPassConfirm

                    string json = "{\"id\": \"" + loggedInUserId + "\", \"oldPass\": \"" + currentPass + "\", \"newPass\": \"" + newPass + "\"}";
                    string url = SERVER + "changePassword";
                    string res = Post(url, json);

                    resObject = JsonConvert.DeserializeObject(res);
                    if (resObject.code == "0")
                    {
                        MessageBox.Show("Cập nhật mật khẩu nhân viên thành công");
                    }
                    else if (resObject.code == "-5")
                    {
                        tbMessageWarning.Text = "Current password is wrong!!!";
                    }
                    else
                    {
                        MessageBox.Show("Đã xảy ra lỗi, vui lòng thử lại");
                    }
                }
                catch
                {
                    MessageBox.Show("Không thể kết nối đến server");
                }
            }
        }

        private void btnChangePassword_Click(object sender, RoutedEventArgs e)
        {
            GridChangePassScreen.Visibility = Visibility.Visible;
        }

        private void btnChange_Click(object sender, RoutedEventArgs e)
        {
            DoChangePass();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            GridChangePassScreen.Visibility = Visibility.Hidden;
            wrpPersonalInfor.Visibility = Visibility.Visible;
        }

        private void WrpPersonalInfor_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (wrpPersonalInfor.Visibility == Visibility.Visible)
            {
                dynamic resObject;
                try
                {
                    string res = Get(SERVER + "getEmployeeById/" + loggedInUserId.ToString());
                    resObject = JsonConvert.DeserializeObject(res);
                    if (resObject.code == "0")
                    {
                        tbxPersonalInforCode.Text = resObject.payload[0].maNV;
                        tbxPersonalInforName.Text = resObject.payload[0].hoTen;
                        tbxMonthPersonalInfor.Text = resObject.payload[0].soThangKinhNghiem;
                        tbxPhoneNumberPersonalInfor.Text = resObject.payload[0].soDienThoai;
                        tbxBirthDayPersonalInfor.Text = resObject.payload[0].ngaySinh;
                        tbxIDPersonalInfor.Text = resObject.payload[0].cmnd;
                    }
                    else if (resObject.code == "-4")
                    {
                        MessageBox.Show("Không tìm thấy nhân viên");
                    }
                    else
                    {
                        MessageBox.Show("Đã xảy ra lỗi, vui lòng bấm lại nút Tìm kiếm");
                    }
                }
                catch
                {
                    MessageBox.Show("Không thể kết nối đến server");
                }
            }
        }

        private void GridInforBill_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                string res = Get(SERVER + "getCustomers");
                dynamic resObject = JsonConvert.DeserializeObject(res);
                if (resObject.code == "0")
                {
                    ComboBoxItem defaultItem = new ComboBoxItem();
                    defaultItem.Name = "__idKhachHangnull";
                    defaultItem.Content = "Khách hàng chưa đăng ký";
                    cbbCustomer.Items.Add(defaultItem);

                    cbbCustomer.SelectedIndex = 0;
                    for (int i = 0; i < resObject.payload.Count; i++)
                    {
                        ComboBoxItem item = new ComboBoxItem();
                        item.Name = "__idKhachHang" + resObject.payload[i].id;
                        item.Content = resObject.payload[i].hoTen;
                        cbbCustomer.Items.Add(item);
                    }
                }
                else if (resObject.code == "-1")
                {
                    ComboBoxItem defaultItem = new ComboBoxItem();
                    defaultItem.Name = "__idKhachHangnull";
                    defaultItem.Content = "Khách hàng chưa đăng ký";
                    cbbCustomer.Items.Add(defaultItem);

                    cbbCustomer.SelectedIndex = 0;
                }
                else
                {
                    MessageBox.Show("Không thể kết nối đến server");
                }
            }
            catch
            {
                MessageBox.Show("Không thể kết nối đến server");
            }
        }

        private void cbbEmployeeFind_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                string res = Get(SERVER + "getEmployers");
                dynamic resObject = JsonConvert.DeserializeObject(res);
                if (resObject.code == "0")
                {
                    ComboBoxItem defaultItem = new ComboBoxItem();
                    defaultItem.Name = "__idNhânViennull";
                    defaultItem.Content = loggedInUserName;
                    cbbEmployeeFind.Items.Add(defaultItem);

                    cbbCustomer.SelectedIndex = 0;
                    for (int i = 0; i < resObject.payload.Count; i++)
                    {
                        ComboBoxItem item = new ComboBoxItem();
                        item.Name = "__idNhânViên" + resObject.payload[i].id;
                        item.Content = resObject.payload[i].hoTen;
                        cbbEmployeeFind.Items.Add(item);
                    }
                }
                else
                {
                    MessageBox.Show("Không thể kết nối đến server");
                }
            }
            catch
            {
                MessageBox.Show("Không thể kết nối đến server");
            }
        }

        private void TbxSearchAgent_KeyUp(object sender, KeyEventArgs e)
        {

            bool found = false;
            var border = (agentResultStack.Parent as ScrollViewer).Parent as Border;

            string query = (sender as TextBox).Text;

            if (query.Length == 0)
            {
                // Clear   
                agentResultStack.Children.Clear();
                border.Visibility = Visibility.Hidden;
            }
            else
            {
                border.Visibility = Visibility.Visible;
                try
                {
                    string res = Get(SERVER + "getEmployeeByName/" + query);
                    dynamic resObject = JsonConvert.DeserializeObject(res);
                    // Clear the list   
                    agentResultStack.Children.Clear();

                    if (resObject.code == "0")
                    {
                        for (int i = 0; i < resObject.payload.Count; i++)
                        {
                            string text = resObject.payload[i].hoTen;

                            // The word starts with this... Autocomplete must work   
                            TextBlock block = new TextBlock();

                            // Add the text   
                            block.Text = text;

                            // A little style...   
                            block.Margin = new Thickness(2, 3, 2, 3);
                            block.Cursor = Cursors.Hand;

                            // Mouse events   
                            block.MouseLeftButtonUp += (s, notCare) =>
                            {
                                border.Visibility = Visibility.Hidden;
                                btnAgent_Click(null, null);
                            };

                            block.MouseEnter += (s, notCare) =>
                            {
                                TextBlock b = s as TextBlock;
                                b.Background = Brushes.PeachPuff;
                            };

                            block.MouseLeave += (s, notCare) =>
                            {
                                TextBlock b = s as TextBlock;
                                b.Background = Brushes.Transparent;
                            };

                            // Add to the panel   
                            agentResultStack.Children.Add(block);
                            found = true;
                        }
                    }

                    if (!found)
                    {
                        agentResultStack.Children.Add(new TextBlock() { Text = "No results found." });
                    }
                }
                catch
                {
                    MessageBox.Show("Không thể kết nối đến server");
                }
            }
        }

        private void WrpFind_Loaded(object sender, RoutedEventArgs e)
        {
            //Tên khách hàng mặc định
            ComboBoxItem defaultCustomer = new ComboBoxItem();
            defaultCustomer.Name = "__idCustomernull";
            defaultCustomer.Content = "Khách hàng chưa đăng ký";
            cbbCustomerFind.Items.Add(defaultCustomer);

            try
            {
                string res = Get(SERVER + "getCustomers");
                dynamic resObject = JsonConvert.DeserializeObject(res);
                if (resObject.code == "0")
                {
                    for (int i = 0; i < resObject.payload.Count; i++)
                    {
                        ComboBoxItem item = new ComboBoxItem();
                        item.Name = "__idCustomer" + resObject.payload[i].id;
                        item.Content = resObject.payload[i].hoTen;
                        cbbCustomerFind.Items.Add(item);
                    }
                }
                else if (resObject.code == "-1")
                {
                    //trick
                }
                else
                {
                    MessageBox.Show("Không thể kết nối đến server");
                }
            }
            catch
            {
                MessageBox.Show("Không thể kết nối đến server");
            }

            try
            {
                string res = Get(SERVER + "getEmployees");
                dynamic resObject = JsonConvert.DeserializeObject(res);
                if (resObject.code == "0")
                {
                    for (int i = 0; i < resObject.payload.Count; i++)
                    {
                        ComboBoxItem item = new ComboBoxItem();
                        item.Name = "__idEmployee" + resObject.payload[i].id;
                        item.Content = resObject.payload[i].hoTen;
                        cbbEmployeeFind.Items.Add(item);
                    }
                }
                else if (resObject.code == "-1")
                {
                    //trick
                }
                else
                {
                    MessageBox.Show("Không thể kết nối đến server");
                }
            }
            catch
            {
                MessageBox.Show("Không thể kết nối đến server");
            }
        }

        private void BtnFind_Click(object sender, RoutedEventArgs e)
        {
            const string employeePrefix = "__idEmployee";
            const string customerPrefix = "__idCustomer";
            string BeginDate = "";
            string EndDate = "";
            string idNhanVien;
            string idKhachHang;
            string json = "";

            //Kiểm tra dữ liệu đầu vào
            if (dpDayStartFind.SelectedDate != null)
            {
                BeginDate = ((DateTime)dpDayStartFind.SelectedDate).ToString("dd/MM/yyyy");
            }

            if (dpDayEndFind.SelectedDate != null)
            {
                EndDate = ((DateTime)dpDayEndFind.SelectedDate).ToString("dd/MM/yyyy");
            }

            if (cbbEmployeeFind.SelectedItem != null)
            {
                idNhanVien = ((ComboBoxItem)cbbEmployeeFind.SelectedItem).Name.Substring(employeePrefix.Length);
            }
            else
            {
                idNhanVien = "";
            }

            if (cbbCustomerFind.SelectedItem != null)
            {
                idKhachHang = ((ComboBoxItem)cbbCustomerFind.SelectedItem).Name.Substring(customerPrefix.Length);
            }
            else
            {
                idKhachHang = "";
            }

            json = "{\"BeginDate\": \"" + BeginDate +
                "\", \"EndDate\": \"" + EndDate +
                "\", \"idNhanVien\": \"" + idNhanVien + "\", \"idKhachHang\": \"" + idKhachHang + "\"}";

            string url = SERVER + "search";
            try
            {
                string result = Post(url, json);
                dynamic stuff = JsonConvert.DeserializeObject(result);

                string code = stuff.code;
                if (code == "0")
                {
                    List<DanhSach> danhSachTimKiem = new List<DanhSach>();
                    int i = 1;
                    foreach (var item in stuff.payload)
                    {
                        danhSachTimKiem.Add(new DanhSach()
                        {
                            stt = i++,
                            thoiGian = item.thoiGian,
                            maHoaDon = item.maHoaDon,
                            nhanVien = item.nhanVien,
                            khachHang = item.khachHang == null ? "Khách vãng lai" : item.khachHang,
                            gia = item.gia,
                        });
                    }
                    lvListFind.ItemsSource = danhSachTimKiem;
                }
                else
                {
                    MessageBox.Show("Không thể kết nối đến server");
                }
            }
            catch
            {
                MessageBox.Show("Không thể kết nối đến server");
            }
        }

        private void cbbCustomerFind_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                string res = Get(SERVER + "getCustomers");
                dynamic resObject = JsonConvert.DeserializeObject(res);
                if (resObject.code == "0")
                {
                    ComboBoxItem defaultItem = new ComboBoxItem();
                    defaultItem.Name = "__idKhachHangnull";
                    defaultItem.Content = "Khách hàng chưa đăng kí";
                    cbbCustomerFind.Items.Add(defaultItem);

                    cbbCustomer.SelectedIndex = 0;
                    for (int i = 0; i < resObject.payload.Count; i++)
                    {
                        ComboBoxItem item = new ComboBoxItem();
                        item.Name = "_idKhachHang" + resObject.payload[i].id;
                        item.Content = resObject.payload[i].hoTen;
                        cbbCustomerFind.Items.Add(item);
                    }
                }
                else
                {
                    MessageBox.Show("Không thể kết nối đến server");
                }
            }
            catch
            {
                MessageBox.Show("Không thể kết nối đến server");
            }
        }

        private void tbxSearchAgent_KeyUp(object sender, KeyEventArgs e)
        {
            bool found = false;
            var border = (resultStack.Parent as ScrollViewer).Parent as Border;

            string query = (sender as TextBox).Text;

            if (query.Length == 0)
            {
                // Clear   
                resultStack.Children.Clear();
                border.Visibility = Visibility.Hidden;
            }
            else
            {
                border.Visibility = Visibility.Visible;
                try
                {
                    string res = Get(SERVER + "getEmployeeByName/" + query);
                    dynamic resObject = JsonConvert.DeserializeObject(res);
                    // Clear the list   
                    resultStack.Children.Clear();

                    if (resObject.code == "0")
                    {
                        for (int i = 0; i < resObject.payload.Count; i++)
                        {
                            string text = resObject.payload[i].hoTen;

                            // The word starts with this... Autocomplete must work   
                            TextBlock block = new TextBlock();

                            // Add the text   
                            block.Text = text;
                            //Add id
                            string nameOfEmployee = ((string)resObject.payload[i].hoTen).Trim();
                            block.Name = "_" + nameOfEmployee;

                            // A little style...   
                            block.Margin = new Thickness(2, 3, 2, 3);
                            block.Cursor = Cursors.Hand;

                            // Mouse events   
                            block.MouseLeftButtonUp += (s, notCare) =>
                            {
                                tbxSearchAgent.Text = (s as TextBlock).Name.Remove(0, 1);
                                border.Visibility = Visibility.Hidden;
                                btnAgent_Click(null, null);
                            };

                            block.MouseEnter += (s, notCare) =>
                            {
                                TextBlock b = s as TextBlock;
                                b.Background = Brushes.PeachPuff;
                            };

                            block.MouseLeave += (s, notCare) =>
                            {
                                TextBlock b = s as TextBlock;
                                b.Background = Brushes.Transparent;
                            };

                            // Add to the panel   
                            resultStack.Children.Add(block);
                            found = true;
                        }
                    }

                    if (!found)
                    {
                        resultStack.Children.Add(new TextBlock() { Text = "No results found." });
                    }
                }
                catch
                {
                    MessageBox.Show("Không thể kết nối đến server");
                }
            }


        }

        private void btnBackStatisticalScreen_Click(object sender, RoutedEventArgs e)
        {
            ((WrapPanel)((Grid)((Grid)((Button)sender).Parent).Parent).Children[1]).Visibility = Visibility.Visible;
            ((Grid)((Button)sender).Parent).Visibility = Visibility.Hidden;
        }

        private void cbSales_Checked(object sender, RoutedEventArgs e)
        {
            cvStatisticalTime.Visibility = Visibility.Visible;
        }

        private void cbSales_UnChecked(object sender, RoutedEventArgs e)
        {
            cvSelectSaleStatistical.Visibility = Visibility.Hidden;
            cvStatisticalTime.Visibility = Visibility.Hidden;

        }

        private void rbByDay_Checked(object sender, RoutedEventArgs e)
        {
        }

        private void rbByMonth_Checked(object sender, RoutedEventArgs e)
        {
        }

        private void rbByYear_Checked(object sender, RoutedEventArgs e)
        {
        }

        private void rbByDay_Unchecked(object sender, RoutedEventArgs e)
        {
        }

        private void rbByMonth_Unchecked(object sender, RoutedEventArgs e)
        {
        }

        private void rbByYear_Unchecked(object sender, RoutedEventArgs e)
        {
        }

        private void btnInsertDrink_Click(object sender, RoutedEventArgs e)
        {
            btnDeleteDrink.Visibility = Visibility.Collapsed;
            btnUpdateInforDrink.Visibility = Visibility.Collapsed;



            if (btnInsertDrink.Content.ToString() == "Thêm mới")
            {
                btnInsertDrink.Content = "Xác nhận";
                tbxNameDrink.Text = "";
                tbxCostDrink.Text = "";
                tbxIngredients.Text = "";
                cbbIngredients.Visibility = Visibility.Visible;
            }
            else if (btnInsertDrink.Content.ToString() == "Xác nhận")
            {
                btnInsertDrink.Content = "Thêm mới";
                cbbIngredients.Visibility = Visibility.Hidden;
                string maSanPham = "";
                if (cbbIngredients.SelectedIndex == 0)
                {
                    maSanPham = "C";
                }
                else if (cbbIngredients.SelectedIndex == 1)
                {
                    maSanPham = "M";

                }
                else if (cbbIngredients.SelectedIndex == 2)
                {
                    maSanPham = "T";

                }
                var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                var stringChars = new char[8];
                var random = new Random();

                for (int i = 0; i < stringChars.Length; i++)
                {
                    stringChars[i] = chars[random.Next(chars.Length)];
                }

                maSanPham += new String(stringChars);
                string NameDrink = tbxNameDrink.Text;//"namedrink"
                string Cost = tbxCostDrink.Text;//"giá"
                string Ingredients = tbxIngredients.Text;//mô tả
                string json = "{\"maSanPham\": \"" + maSanPham + "\",\"tenSanPham\": \"" + NameDrink + "\",\"thongTin\": \"" + Ingredients + "\", \"gia\": " + Cost + "}";
                string url = SERVER + "insertDrink";
                try
                {
                    string result = Post(url, json);
                    dynamic stuff = JsonConvert.DeserializeObject(result);

                    string code = stuff.code;
                    if (code == "0")
                    {
                        MessageBox.Show("Thêm món thành công");
                        griInforDrinks.Visibility = Visibility.Hidden;
                        LoadMenu();
                        ListDrinks.Clear();
                        tbTotalMoney.Text = " Tổng tiền:        ";
                        stpMainScreen.Opacity = 1;

                    }
                    else
                    {
                        MessageBox.Show("Có lỗi xảy ra, vui lòng thử lại");
                    }
                }
                catch
                {
                    MessageBox.Show("Có lỗi xảy ra, vui lòng thử lại");
                }
            }
        }

        private void btnStatistical_Click(object sender, RoutedEventArgs e)
        {
            string BeginDateSale = txtStartDateStatistical.Text;
            string EndDateSale = txtEndDateStatistical.Text;
            string BeginDateTop10 = txtStartDateStatisticalTop10.Text;
            string EndDateTop10 = txtEndDateStatisticalTop10.Text;
            string TypeSaleStatistical = "";
            string url = "";
            string result = "";

            if (cbSales.IsChecked == true)
            {
                if (rbByDay.IsChecked == true)
                {
                    TypeSaleStatistical = "Day";
                }
                if (rbByMonth.IsChecked == true)
                {
                    TypeSaleStatistical = "Month";
                }
                if (rbByYear.IsChecked == true)
                {
                    TypeSaleStatistical = "Year";
                }

                try
                {
                    DateTime.ParseExact(BeginDateSale, "dd-MM-yyyy", null);
                    DateTime.ParseExact(EndDateSale, "dd-MM-yyyy", null);
                }
                catch
                {
                    MessageBox.Show("Vui lòng nhập định dạng ngày-tháng-năm");
                    return;
                }

                url = SERVER + "saleReport/" + BeginDateSale + "/" + EndDateSale + "/" + TypeSaleStatistical;

                try
                {
                    result = Get(url);
                    dynamic stuff = JsonConvert.DeserializeObject(result);

                    if (stuff.code == "0")
                    {
                        ReportChart saleReport = new ReportChart();
                        long[] data = new long[stuff.payload.Count];
                        string[] label = new string[stuff.payload.Count];

                        for (int i = 0; i < stuff.payload.Count; i++)
                        {
                            //Label
                            if (TypeSaleStatistical == "Day")
                            {
                                saleReport.AxisX.Title = "Ngày";
                                if (((Newtonsoft.Json.Linq.JContainer)stuff.payload[i]).Count == 2)
                                {
                                    label[i] = stuff.payload[i].DAY.Value + "";
                                }
                                else if (((Newtonsoft.Json.Linq.JContainer)stuff.payload[i]).Count == 3)
                                {
                                    label[i] = stuff.payload[i].DAY.Value + "/" + stuff.payload[i].MONTH.Value;
                                }
                                else if (((Newtonsoft.Json.Linq.JContainer)stuff.payload[i]).Count == 4)
                                {
                                    label[i] = stuff.payload[i].DAY.Value + "/" + stuff.payload[i].MONTH.Value + "/" + stuff.payload[i].YEAR.Value;
                                }
                            }
                            else if (TypeSaleStatistical == "Month")
                            {
                                saleReport.AxisX.Title = "Tháng";
                                if (((Newtonsoft.Json.Linq.JContainer)stuff.payload[i]).Count == 2)
                                {
                                    label[i] = stuff.payload[i].MONTH.Value + "";
                                }
                                else if (((Newtonsoft.Json.Linq.JContainer)stuff.payload[i]).Count == 3)
                                {
                                    label[i] = stuff.payload[i].MONTH.Value + "/" + stuff.payload[i].YEAR.Value;
                                }

                            }
                            else if (TypeSaleStatistical == "Year")
                            {
                                saleReport.AxisX.Title = "Năm";
                                if (((Newtonsoft.Json.Linq.JContainer)stuff.payload[i]).Count == 2)
                                {
                                    label[i] = stuff.payload[i].YEAR.Value + "";
                                }
                            }

                            data[i] = stuff.payload[i].DoanhThu;
                        }

                        saleReport.SeriesCollection = new SeriesCollection
                        {
                            new ColumnSeries
                            {
                                Title = "Doanh thu",
                                Values = new ChartValues<long>(data)
                            }
                        };
                        saleReport.Labels = label;
                        saleReport.AxisY.Title = "Doanh thu";
                        saleReport.Formatter = value =>
                        {
                            return value.ToString("N0") + " VND";
                        };
                        saleReport.Title = "Biểu đồ doanh thu";
                        saleReport.DataContext = saleReport;
                        saleReport.Show();
                    }
                    else
                    {
                        MessageBox.Show("Không thể kết nối đến server");
                    }
                }
                catch
                {
                    MessageBox.Show("Đã xảy ra lỗi");
                }
            }
            if (cbTop10Famous.IsChecked == true)
            {
                try
                {
                    DateTime.ParseExact(BeginDateTop10, "dd-MM-yyyy", null);
                    DateTime.ParseExact(EndDateTop10, "dd-MM-yyyy", null);
                }
                catch
                {
                    MessageBox.Show("Vui lòng nhập định dạng ngày-tháng-năm");
                    return;
                }
                url = SERVER + "reportTopDrink/" + BeginDateTop10 + "/" + EndDateTop10 + "/10";

                try
                {
                    result = Get(url);
                    dynamic stuff = JsonConvert.DeserializeObject(result);

                    if (stuff.code == "0")
                    {
                        ReportChart topReport = new ReportChart();
                        long[] data = new long[stuff.payload.Count];
                        string[] label = new string[stuff.payload.Count];

                        for (int i = 0; i < stuff.payload.Count; i++)
                        {
                            label[i] = stuff.payload[i].tenSanPham.Value + "";
                            data[i] = stuff.payload[i].soLuong;
                        }

                        topReport.SeriesCollection = new SeriesCollection
                        {
                            new ColumnSeries
                            {
                                Title = "Số lượng",
                                Values = new ChartValues<long>(data)
                            }
                        };
                        topReport.Labels = label;
                        topReport.AxisY.Title = "Loại thức uống";
                        topReport.AxisX.LabelsRotation = 45;
                        topReport.Formatter = value =>
                        {
                            return value.ToString("N0");
                        };
                        topReport.Title = "Biểu đồ top 10 thức uống";
                        topReport.DataContext = topReport;
                        topReport.Show();
                    }
                    else
                    {
                        MessageBox.Show("Không thể kết nối đến server");
                    }
                }
                catch
                {
                    MessageBox.Show("Đã xảy ra lỗi");
                }
                
            }

        }

        private void btnTypeSaleStatistical_Click(object sender, RoutedEventArgs e)
        {
            cvSelectSaleStatistical.Visibility = Visibility.Visible;
            rbByDay.Visibility = Visibility.Visible;
            rbByMonth.Visibility = Visibility.Visible;
            rbByYear.Visibility = Visibility.Visible;

        }

        private void cbTop10Famous_Checked(object sender, RoutedEventArgs e)
        {
            cvStatisticalTimeTop10.Visibility = Visibility.Visible;

        }

        private void cbTop10Famous_Unchecked(object sender, RoutedEventArgs e)
        {
            cvStatisticalTimeTop10.Visibility = Visibility.Hidden;
        }


        private void btnCancelStatis_Click(object sender, RoutedEventArgs e)
        {
            cbSales.IsChecked = false;
            cbTop10Famous.IsChecked = false;
            txtStartDateStatistical.Text = "";
            txtEndDateStatistical.Text = "";
            txtStartDateStatisticalTop10.Text = "";
            txtEndDateStatisticalTop10.Text = "";
            rbByDay.IsChecked = false;
            rbByMonth.IsChecked = false;
            rbByYear.IsChecked = false;
        }
    }
}