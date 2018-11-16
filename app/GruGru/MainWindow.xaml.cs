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
        Double height = SystemParameters.WorkArea.Height-30;
        Double width = SystemParameters.WorkArea.Width-30;
        Double height005 = SystemParameters.WorkArea.Height * 0.05;//25
        Double height004 = SystemParameters.WorkArea.Height * 0.04;//20
        Double height003 = SystemParameters.WorkArea.Height * 0.03;//15
        Double height0025 = SystemParameters.WorkArea.Height * 0.025;//12.5
        Double height0027 = SystemParameters.WorkArea.Height * 0.027;
        Double height002 = SystemParameters.WorkArea.Height * 0.02;//40
        Double height0013 = SystemParameters.WorkArea.Height * 0.013;//15
        Double height0018 = SystemParameters.WorkArea.Height * 0.018;//15
        Double height0028 = SystemParameters.WorkArea.Height * 0.028;//40

        public MainWindow()
        {
            InitializeComponent();
            this.Left = 0;
            this.Top = 0;
            this.Height = height+30;
            this.Width = width+30;
            //MainScreen();
            //StatisticalScreen();
            JobCalendarScreen();

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

            //menu
            stpMenu.Height = height - stpTitle.Height-10;
            stpDrink.Height = stpMenu.Height;
            stpDrink.Width = 0.69 * width;

            //chuyển trang
            btnchange.Height = height003;
            btnchange.Width = 0.69 * width;

            //coffee
            gridCoffee.Width = txbCoffee.Width-10;
            gridCoffee.Height =stpDrink.Height-height003;

            tbCf1.Height = gridCoffee.Height *0.6 /12;
            tbCf1.Width = gridCoffee.Width *7/10;
            tbCf1.FontSize = height002;

            btnChooseCf1.Height = gridCoffee.Height * 0.6 / 12;
            btnChooseCf1.Width = gridCoffee.Width *3/10;

            btnInforcf1.Width = gridCoffee.Width *3/10;
            btnInforcf1.Height = gridCoffee.Height * 0.4 / 12;

            cbbSizeCf1.Width = gridCoffee.Width *7/10;
            cbbSizeCf1.Height = gridCoffee.Height *0.4 /12;
            cbbSizeCf1.FontSize = height0013;

            /*tbCf2.Height = gridCoffee.Height * 2.2 / 15;
            tbCf2.Width= gridCoffee.Width /10*3;
            tbCf2.FontSize = 15;
            btnChooseCf2.Height = gridCoffee.Height * 2.2 / 15;
            btnChooseCf2.Width = gridCoffee.Width / 10*2;
            btnInforcf2.Width = gridCoffee.Width / 10*2;
            btnInforcf2.Height = gridCoffee.Height * 0.8 / 15;
            cbbSizeCf2.Width = gridCoffee.Width / 10*3;
            cbbSizeCf2.Height = gridCoffee.Height * 0.8 / 15;
            cbbSizeCf2.FontSize = height0025;
            cbbSizeCf2.Text = "X";*/

            //Milktea
            gridMilktea.Width = txbCoffee.Width;
            gridMilktea.Height = stpDrink.Height - height003;

            tbMt1.Height = gridCoffee.Height * 0.6 / 12;
            tbMt1.Width = gridCoffee.Width * 7 / 10;
            tbMt1.FontSize = height002;

            btnChooseMt1.Height = gridCoffee.Height * 0.6 / 12;
            btnChooseMt1.Width = gridCoffee.Width * 3 / 10;

            btnInforMt1.Width = gridCoffee.Width * 3 / 10;
            btnInforMt1.Height = gridCoffee.Height * 0.4 / 12;

            cbbSizeMt1.Width = gridCoffee.Width * 7 / 10;
            cbbSizeMt1.Height = gridCoffee.Height * 0.4 / 12;
            cbbSizeMt1.FontSize = height0013;

            //Topping
            gridTopping.Width = txbCoffee.Width;
            gridTopping.Height = stpDrink.Height - height003;

            tbTp1.Height = gridCoffee.Height * 0.6 / 12;
            tbTp1.Width = gridCoffee.Width * 7 / 10;
            tbTp1.FontSize = height002;

            btnChooseTp1.Height = gridCoffee.Height * 0.6 / 12;
            btnChooseTp1.Width = gridCoffee.Width * 3 / 10;

            btnInforTp1.Width = gridCoffee.Width * 3 / 10;
            btnInforTp1.Height = gridCoffee.Height * 0.4 / 12;

            cbbSizeTp1.Width = gridCoffee.Width * 7 / 10;
            cbbSizeTp1.Height = gridCoffee.Height * 0.4 / 12;
            cbbSizeTp1.FontSize = height0013;

            //InforBill
            gridInforBill.Width = 0.31 * width;
            gridInforBill.Height = height - height005;

            Double heighttbBill = gridInforBill.Height * 3 / 60;
            tbBillNumber.FontSize = height0027;
            tbBillNumber.Height = heighttbBill;
            tbBillNumber.Width = gridInforBill.Width * 2 / 6-5;

            tbxBillNumber.FontSize = height0027;
            tbxBillNumber.Height= heighttbBill;
            tbxBillNumber.Width = gridInforBill.Width * 4 / 6-5;

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
            tbEmployee.Width = gridInforBill.Width -10;

            tbTime.FontSize = height0027;
            tbTime.Height = heighttbBill;
            tbTime.Width = gridInforBill.Width - 10;

            lvListBill.FontSize = height0027;
            lvListBill.Height = gridInforBill.Height *6/11;
            lvListBill.Width = gridInforBill.Width;

            tbOfferCode.FontSize = height0027;
            tbOfferCode.Height = heighttbBill;
            tbOfferCode.Width = gridInforBill.Width * 3 / 8 - 5;

            cbbOfferCode.FontSize = height0027;
            cbbOfferCode.Height = heighttbBill;
            cbbOfferCode.Width = gridInforBill.Width * 5 / 8 - 5;

            temp2.Height = heighttbBill/4;
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
            tbTimestart.Width = width / 90*8;

            dpDayStart.FontSize = height0018;
            dpDayStart.Height = height005;
            dpDayStart.Width = width / 90 * 6;

            temp5.Width = width / 180;

            mtpHourStart.FontSize = height0018;
            mtpHourStart.Height = height004;
            mtpHourStart.Width = width / 90*6;

            temp6.Width = width / 180*5;

            tbTimeEnd.FontSize = height002;
            tbTimeEnd.Height = height003;
            tbTimeEnd.Width = width / 90*8;

            dpDayEnd.FontSize = height0018;
            dpDayEnd.Height = height005;
            dpDayEnd.Width = width / 90*6;

            temp7.Width = width / 180;

            mtpHourEnd.FontSize = height0018;
            mtpHourEnd.Height = height004;
            mtpHourEnd.Width = width / 90*6;

            temp8.Width = width / 180*5;

            cbbTypeStatistical.FontSize = height0018;
            cbbTypeStatistical.Height = height004;
            cbbTypeStatistical.Width = width / 90*6;

            temp9.Width = width / 180;

            tbxSearchStatistical.FontSize = height0018;
            tbxSearchStatistical.Height = height005;
            tbxSearchStatistical.Width = width / 30 * 8.5;

            temp10.Width = width / 180*3;

            //list statistical
            temp11.Width = width *0.05;

            lvListStatistical.Height = height * 0.85;
            lvListStatistical.Width = width * 0.9;

            gvcSTT.Width = lvListStatistical.Width / 20;
            gvcHour.Width = lvListStatistical.Width / 20*2;
            gvcDay.Width = lvListStatistical.Width / 20*2;
            gvcBillCode.Width = lvListStatistical.Width / 20*2;
            gvcEmployeeCode.Width = lvListStatistical.Width / 15*2;
            gvcCustomerCode.Width = lvListStatistical.Width / 15*2;
            gvcDrink.Width = lvListStatistical.Width / 15*3;
            gvcMoney.Width = lvListStatistical.Width / 20*2;
            gvcNumber.Width = lvListStatistical.Width / 20*2;

            temp12.Width = width / 5*4;
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

            temp32.Width = width / 9*5;
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
    }
}
