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

        public MainWindow()
        {
            InitializeComponent();
            this.Left = 0;
            this.Top = 0;
            this.Height = height + 30;
            this.Width = width + 30;
            MainScreen();
            //StatisticalScreen();
            //JobCalendarScreen();
            //FindScreen();

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

        public void SetItem(TextBlock tb, Button btnChoose, Button btnInfor, ComboBox cbbSize)
        {
            tb.Height = gridCoffee.Height * 0.6 / 12;
            tb.Width = gridCoffee.Width * 7 / 10;
            tb.FontSize = height002;

            btnChoose.Height = gridCoffee.Height * 0.6 / 12;
            btnChoose.Width = gridCoffee.Width * 3 / 10;

            btnInfor.Width = gridCoffee.Width * 3 / 10;
            btnInfor.Height = gridCoffee.Height * 0.4 / 12;

            cbbSize.Width = gridCoffee.Width * 7 / 10;
            cbbSize.Height = gridCoffee.Height * 0.4 / 12;
            cbbSize.FontSize = height0013;
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
            stpMenu.Height = height - stpTitle.Height - 10;
            stpDrink.Height = stpMenu.Height;
            stpDrink.Width = 0.69 * width;

            //chuyển trang
            btnchange.Height = height003;
            btnchange.Width = 0.69 * width;

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
            temp02.Width = 6;
            gridMilktea.Width = txbCoffee.Width-8;
            gridMilktea.Height = stpDrink.Height - height003;

            //từng món trong ô Milktea
            SetItem(tbMt0, btnChooseMt0, btnInforMt0, cbbSizeMt0);
            SetItem(tbMt1, btnChooseMt1, btnInforMt1, cbbSizeMt1);
            SetItem(tbMt2, btnChooseMt2, btnInforMt2, cbbSizeMt2);
            SetItem(tbMt3, btnChooseMt3, btnInforMt3, cbbSizeMt3);
            SetItem(tbMt4, btnChooseMt4, btnInforMt4, cbbSizeMt4);
            SetItem(tbMt5, btnChooseMt5, btnInforMt5, cbbSizeMt5);
            SetItem(tbMt6, btnChooseMt6, btnInforMt6, cbbSizeMt6);
            SetItem(tbMt7, btnChooseMt7, btnInforMt7, cbbSizeMt7);
            SetItem(tbMt8, btnChooseMt8, btnInforMt8, cbbSizeMt8);
            SetItem(tbMt9, btnChooseMt9, btnInforMt9, cbbSizeMt9);
            SetItem(tbMt10, btnChooseMt10, btnInforMt10, cbbSizeMt10);
            SetItem(tbMt11, btnChooseMt11, btnInforMt11, cbbSizeMt11);

            //Topping
            temp03.Width = 6;
            gridTopping.Width = txbCoffee.Width-8;
            gridTopping.Height = stpDrink.Height - height003;

            //Từng món trong ô Topping
            SetItem(tbTp0, btnChooseTp0, btnInforTp0, cbbSizeTp0);
            SetItem(tbTp1, btnChooseTp1, btnInforTp1, cbbSizeTp1);
            SetItem(tbTp2, btnChooseTp2, btnInforTp2, cbbSizeTp2);
            SetItem(tbTp3, btnChooseTp3, btnInforTp3, cbbSizeTp3);
            SetItem(tbTp4, btnChooseTp4, btnInforTp4, cbbSizeTp4);
            SetItem(tbTp5, btnChooseTp5, btnInforTp5, cbbSizeTp5);
            SetItem(tbTp6, btnChooseTp6, btnInforTp6, cbbSizeTp6);
            SetItem(tbTp7, btnChooseTp7, btnInforTp7, cbbSizeTp7);
            SetItem(tbTp8, btnChooseTp8, btnInforTp8, cbbSizeTp8);
            SetItem(tbTp9, btnChooseTp9, btnInforTp9, cbbSizeTp9);
            SetItem(tbTp10, btnChooseTp10, btnInforTp10, cbbSizeTp10);
            SetItem(tbTp11, btnChooseTp11, btnInforTp11, cbbSizeTp11);


            //InforBill
            gridInforBill.Width = 0.31 * width;
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

            //list thông tin khách hàng
            temp49.Height = height * 0.15;
            temp49.Width = width;

            temp48.Width = width * 0.1;

            gridFind.Height = height * 0.85;
            gridFind.Width = width * 0.8;

            stpInforCustomer.Height = gridFind.Height;
            stpInforCustomer.Width = gridFind.Width;

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
    }
}
