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
using System.Net;
using System.IO;
using System.Collections.Specialized;
using System.Net.Http;
using Newtonsoft.Json;
using GruGru.Model;

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
        Double height = SystemParameters.WorkArea.Height - 30;
        Double width = SystemParameters.WorkArea.Width - 30;
        Double height005 = SystemParameters.WorkArea.Height * 0.05;//25
        Double height004 = SystemParameters.WorkArea.Height * 0.04;//20
        Double height003 = SystemParameters.WorkArea.Height * 0.03;//15
        Double height0025 = SystemParameters.WorkArea.Height * 0.025;//12.5
        Double height0027 = SystemParameters.WorkArea.Height * 0.027;
        Double height002 = SystemParameters.WorkArea.Height * 0.02;//40
        Double height0013 = SystemParameters.WorkArea.Height * 0.013;//15
        Double height0018 = SystemParameters.WorkArea.Height * 0.018;//15
        Double height0028 = SystemParameters.WorkArea.Height * 0.028;//40
        Double height0023 = SystemParameters.WorkArea.Height * 0.023;//12.5

        public MainWindow()
        {
            InitializeComponent();
            this.Left = 0;
            this.Top = 0;
            this.Height = height + 30;
            this.Width = width + 30;
            //MainScreen();
           StatisticalScreen();
            // JobCalendarScreen();
            // FindScreen();
            //CustomerScreen();
             AgentScreen();
            // SignUp();
            // InforScreen();
            /*coffee.Width = 0.2 * width;
            coffee.Height =height-height006;
            txbCoffee.Width = 0.2 * width;
            milktea.Width = 0.2 * width;
            milktea.Height = height - height006;
            txbMilktea.Width = 0.2 * width;
            topping.Width = 0.2 * width;
            topping.Height = height - height006;
            txbTopping.Width = 0.2 * width;*/
            /* List<ThucUong> Items = new List<ThucUong>();
          Items.Add(new ThucUong() { STT = 1, ten = "tra sua", gia = 15000, soluong = 1 });
          Items.Add(new ThucUong() { STT = 1, ten = "coffee den", gia = 20000, soluong = 1 });
         Items.Add(new ThucUong() { STT = 1, ten = "coffee den", gia = 20000, soluong = 1 });
         Items.Add(new ThucUong() { STT = 1, ten = "coffee den", gia = 20000, soluong = 1 });
         Items.Add(new ThucUong() { STT = 1, ten = "coffee den", gia = 20000, soluong = 1 });
         Items.Add(new ThucUong() { STT = 1, ten = "coffee den", gia = 20000, soluong = 1 });
         Items.Add(new ThucUong() { STT = 1, ten = "coffee den", gia = 20000, soluong = 1 });
           lvListBill.ItemsSource = Items;*/
        }

        public class ThucUong
        {
            public int STT { get; set; }
            public string ten { get; set; }
            public long gia { get; set; }
            public int soluong { get; set; }
        }


       /* public void SetItem(TextBlock tb, Button btnChoose, Button btnInfor, ComboBox cbbSize)
        {
            Double widthItem;
            if (NumberOfDrinks % 3 == 0)
            {
                widthItem = lvMenuDrink.Width * 3 / NumberOfDrinks;
            }
            else
            {
                widthItem = lvMenuDrink.Width / ((int)(NumberOfDrinks / 3) + 1);
            }
            tb.Height = widthItem * 0.6 / 12;
            tb.Width = lvMenuDrink.Width / 3 * 7 / 10;
            tb.FontSize = height002;

            btnChoose.Height = widthItem * 0.6 / 12;
            btnChoose.Width = lvMenuDrink.Width / 3 * 3 / 10;

            btnInfor.Width = lvMenuDrink.Width / 3 * 3 / 10;
            btnInfor.Height = widthItem * 0.4 / 12;

            cbbSize.Width = lvMenuDrink.Width / 3 * 7 / 10;
            cbbSize.Height = widthItem * 0.4 / 12;
            cbbSize.FontSize = height0013;
        }*/
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

            txbInfor.Width = 0.26 * width;
            txbInfor.FontSize = height004;

            cbbEmployee.Width = 0.05 * width;
            cbbEmployee.FontSize = height004;

            cbbManage.Width = 0.05 * width;
            cbbManage.FontSize = height004;

            //menu
            stpMenu.Height = height - stpTitle.Height - 10;
            stpDrink.Height = stpMenu.Height;
            stpDrink.Width = 0.69 * width;

            lvMenuDrink.Height = stpMenu.Height;
            lvMenuDrink.Width = stpDrink.Width;

            tempmenu.Width = width*0.01;

            //SetItem(tbDrink, btnChoose, cbbSize, btnInfor);

            //coffee
            temp01.Width = 6;
            gridCoffee.Width = txbCoffee.Width - 8;
            gridCoffee.Height = stpDrink.Height - height003;

            //từng món trong ô Coffee
            SetItem(tbCf0, btnChooseCf0, btnInforCf0, cbbSizeCf0);
            SetItem(tbCf1, btnChooseCf1, btnInforCf1, cbbSizeCf1);
            SetItem(tbCf2, btnChooseCf2, btnInforCf2, cbbSizeCf2);
            SetItem(tbCf3, btnChooseCf3, btnInforCf3, cbbSizeCf3);
            SetItem(tbCf4, btnChooseCf4, btnInforCf4, cbbSizeCf4);
            SetItem(tbCf5, btnChooseCf5, btnInforCf5, cbbSizeCf5);
            SetItem(tbCf6, btnChooseCf6, btnInforCf6, cbbSizeCf6);
            SetItem(tbCf7, btnChooseCf7, btnInforCf7, cbbSizeCf7);
            SetItem(tbCf8, btnChooseCf8, btnInforCf8, cbbSizeCf8);
            SetItem(tbCf9, btnChooseCf9, btnInforCf9, cbbSizeCf9);
            SetItem(tbCf10, btnChooseCf10, btnInforCf10, cbbSizeCf10);
            SetItem(tbCf11, btnChooseCf11, btnInforCf11, cbbSizeCf11);

            lvMenuMilkteas.Height = stpMenu.Height;
            lvMenuMilkteas.Width = stpDrink.Width/3;

            lvMenuToppings.Height = stpMenu.Height;
            lvMenuToppings.Width = stpDrink.Width/3;

            //InforBill
            gridInforBill.Width = 0.3 * width;
            gridInforBill.Height = height - height005;

            Double heighttbBill = gridInforBill.Height * 3 / 60;
            tbBillNumber.FontSize = height0027;
            tbBillNumber.Height = heighttbBill;
            tbBillNumber.Width = gridInforBill.Width * 2 / 6 - 5;

            tbxBillNumber.FontSize = height0027;
            tbxBillNumber.Height = heighttbBill;
            tbxBillNumber.Width = gridInforBill.Width * 4 / 6 - 5;

            tbCustomer.FontSize = height0027;
            tbCustomer.Height = heighttbBill;
            tbCustomer.Width = gridInforBill.Width * 2 / 6 - 5;

            tbxCustomer.FontSize = height0027;
            tbxCustomer.Height = heighttbBill;
            tbxCustomer.Width = gridInforBill.Width * 4 / 6 - 5;

            temp1.Height = heighttbBill / 4;
            temp1.Width = gridInforBill.Width;

            tbEmployee.FontSize = height0027;
            tbEmployee.Height = heighttbBill;
            tbEmployee.Width = gridInforBill.Width - 10;

            tbTime.FontSize = height0027;
            tbTime.Height = heighttbBill;
            tbTime.Width = gridInforBill.Width - 10;

            lvListBill.FontSize = height0027;
            lvListBill.Height = gridInforBill.Height * 6 / 11;
            lvListBill.Width = gridInforBill.Width;

            tbOfferCode.FontSize = height0027;
            tbOfferCode.Height = heighttbBill;
            tbOfferCode.Width = gridInforBill.Width * 3 / 8 - 5;

            cbbOfferCode.FontSize = height0027;
            cbbOfferCode.Height = heighttbBill;
            cbbOfferCode.Width = gridInforBill.Width * 5 / 8 - 5;

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
            //hàng đầu tiên
            temp4.Width = width / 90;

            tbTimestart.FontSize = height002;
            tbTimestart.Height = height003;
            tbTimestart.Width = width / 90 * 8;

            dpDayStart.FontSize = height0018;
            dpDayStart.Height = height005;
            dpDayStart.Width = width / 90 * 6;

            temp5.Width = width / 180;

            mtpHourStart.FontSize = height0018;
            mtpHourStart.Height = height004;
            mtpHourStart.Width = width / 90 * 6;

            temp6.Width = width / 180 * 5;

            tbTimeEnd.FontSize = height002;
            tbTimeEnd.Height = height003;
            tbTimeEnd.Width = width / 90 * 8;

            dpDayEnd.FontSize = height0018;
            dpDayEnd.Height = height005;
            dpDayEnd.Width = width / 90 * 6;

            temp7.Width = width / 180;

            mtpHourEnd.FontSize = height0018;
            mtpHourEnd.Height = height004;
            mtpHourEnd.Width = width / 90 * 6;

            temp8.Width = width / 180 * 5;

            cbbTypeStatistical.FontSize = height0018;
            cbbTypeStatistical.Height = height004;
            cbbTypeStatistical.Width = width / 90 * 6;

            temp9.Width = width / 180;

            tbxSearchStatistical.FontSize = height0018;
            tbxSearchStatistical.Height = height005;
            tbxSearchStatistical.Width = width / 30 * 8.5;

            temp10.Width = width / 180 * 3;

            //list statistical
            temp11.Width = width * 0.05;

            lvListStatistical.Height = height * 0.85;
            lvListStatistical.Width = width * 0.9;

            gvcSTT.Width = lvListStatistical.Width / 20;
            gvcHour.Width = lvListStatistical.Width / 20 * 2;
            gvcDay.Width = lvListStatistical.Width / 20 * 2;
            gvcBillCode.Width = lvListStatistical.Width / 20 * 2;
            gvcEmployeeCode.Width = lvListStatistical.Width / 15 * 2;
            gvcCustomerCode.Width = lvListStatistical.Width / 15 * 2;
            gvcDrink.Width = lvListStatistical.Width / 15 * 3;
            gvcMoney.Width = lvListStatistical.Width / 20 * 2;
            gvcNumber.Width = lvListStatistical.Width / 20 * 2;

            temp12.Width = width / 5 * 4;
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

            //thứ
            tbMonday.FontSize = height003;
            tbTuesday.FontSize = height003;
            tbWednesday.FontSize = height003;
            tbThursday.FontSize = height003;
            tbFriday.FontSize = height003;
            tbSaturday.FontSize = height003;
            tbSunday.FontSize = height003;

            //giờ làm
            tbTime1.FontSize = height0025;
            tbTime2.FontSize = height0025;
            tbTime3.FontSize = height0025;

            //họ và tên
            tbMonday1.FontSize = height004;
            tbTuesday1.FontSize = height004;
            tbWednesday1.FontSize = height004;
            tbThursday1.FontSize = height004;
            tbFriday1.FontSize = height004;
            tbSaturday1.FontSize = height004;
            tbSunday1.FontSize = height004;

            tbMonday2.FontSize = height004;
            tbTuesday2.FontSize = height004;
            tbWednesday2.FontSize = height004;
            tbThursday2.FontSize = height004;
            tbFriday2.FontSize = height004;
            tbSaturday2.FontSize = height004;
            tbSunday2.FontSize = height004;

            tbMonday3.FontSize = height004;
            tbTuesday3.FontSize = height004;
            tbWednesday3.FontSize = height004;
            tbThursday3.FontSize = height004;
            tbFriday3.FontSize = height004;
            tbSaturday3.FontSize = height004;
            tbSunday3.FontSize = height004;

        }

        public void FindScreen()
        {
            //hàng đầu tiên
            temp41.Width = width / 90;

            tbTimestartFind.FontSize = height002;
            tbTimestartFind.Height = height003;
            tbTimestartFind.Width = width / 90 * 8;

            dpDayStartFind.FontSize = height0018;
            dpDayStartFind.Height = height005;
            dpDayStartFind.Width = width / 90 * 6;

            temp42.Width = width / 180;

            mtpHourStartFind.FontSize = height0018;
            mtpHourStartFind.Height = height004;
            mtpHourStartFind.Width = width / 90 * 6;

            temp43.Width = width / 180 * 5;

            tbTimeEndFind.FontSize = height002;
            tbTimeEndFind.Height = height003;
            tbTimeEndFind.Width = width / 90 * 8;

            dpDayEndFind.FontSize = height0018;
            dpDayEndFind.Height = height005;
            dpDayEndFind.Width = width / 90 * 6;

            temp44.Width = width / 180;

            mtpHourEndFind.FontSize = height0018;
            mtpHourEndFind.Height = height004;
            mtpHourEndFind.Width = width / 90 * 6;

            temp45.Width = width / 180 * 5;

            cbbTypeFind.FontSize = height0018;
            cbbTypeFind.Height = height004;
            cbbTypeFind.Width = width / 90 * 6;

            temp46.Width = width / 180;

            tbxSearchFind.FontSize = height0018;
            tbxSearchFind.Height = height005;
            tbxSearchFind.Width = width / 30 * 8.5;

            temp47.Width = width / 180 * 3;

            //list Find
            temp48.Width = width * 0.05;

            gridFind.Height = height * 0.9;
            gridFind.Width = width * 0.9;

            lvListFind.Height = height * 0.9;
            lvListFind.Width = width * 0.9;

            gvcFindSTT.Width = lvListFind.Width / 20;
            gvcFindHour.Width = lvListFind.Width / 20 * 2;
            gvcFindDay.Width = lvListFind.Width / 20 * 2;
            gvcFindBillCode.Width = lvListFind.Width / 20 * 2;
            gvcFindEmployeeCode.Width = lvListFind.Width / 15 * 2;
            gvcFindCustomerCode.Width = lvListFind.Width / 15 * 2;
            gvcFindDrink.Width = lvListFind.Width / 15 * 3;
            gvcFindMoney.Width = lvListFind.Width / 20 * 2;
            gvcFindNumber.Width = lvListFind.Width / 20 * 2;

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

            tbNameDrink.FontSize = height0023;
            tbxNameDrink.FontSize = height0023;
            tbxNameDrink.Width = stpInforDrinksMini.Width * 0.8;

            temp71.Height = stpInforDrinksMini.Height / 30;

            tbTypeDrink.FontSize = height0023;
            tbxTypeDrink.FontSize = height0023;
            tbxTypeDrink.Width = stpInforDrinksMini.Width * 0.8;

            temp72.Height = stpInforDrinksMini.Height / 30;

            tbIngredients.FontSize = height0023;

            lvListIngredients.FontSize = height0023;
            lvListIngredients.Height = stpInforDrinksMini.Height / 5 * 3;
            lvListIngredients.Width = stpInforDrinksMini.Width;

            lvhOrdinalIngredient.Width = lvListIngredients.Width / 10;
            lvhNameIngredient.Width = lvListIngredients.Width / 10 * 7;
            lvhPercent.Width = lvListIngredients.Width / 10;
            lvhKcal.Width = lvListIngredients.Width / 10;

            stpInforDrinksMini1.Width = stpInforDrinksMini.Width;
            gridbtnDeleteDrink.Width = stpInforDrinksMini1.Width / 3;
            gridbtnUpdateInforDrink.Width = stpInforDrinksMini1.Width / 3;
            gridbtnBackInforDrink.Width = stpInforDrinksMini1.Width / 3;

            btnDeleteDrink.FontSize = height0027;
            btnUpdateInforDrink.FontSize = height0027;
            btnBackInforDrink.FontSize = height0027;
            stpMainScreen.Opacity = 0.1;
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
            temp91.Width = width / 10*8;

            temp92.Width = width ;
            temp92.Height = height / 5;

            temp93.Width = width*0.13;


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


        public void DoLogin()
        {
            if ((txtUsername.Text == "") && (txtPassword.Password == ""))
            {
                tbMessageBox.Text = "invalid username and password!!!";
            }
            else if (txtUsername.Text == "")
            {
                tbMessageBox.Text = "invalid username!!!";
            }
            else if (txtPassword.Password == "")
            {
                tbMessageBox.Text = "invalid password!!!";
            }
            else
            {
                tbMessageBox.Text = "Username or password is wrong!!!";
            }
        }

        private void txtUsername_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                DoLogin();
            }
        }

        private void txtPassword_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                DoLogin();
            }
        }
  
     
    }
}
