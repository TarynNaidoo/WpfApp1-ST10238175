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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public class Global
        {
            //Global Variable class, for all variables within the application
            public static int incomeBD;
            public static int incomeTAX;
            public static int incomeAD;

            public static string Accomadation;
            public static double MonthlyRental;
            public static double PropertyPurPrice;
            public static double TotalDeposit;
            public static double InterestRate;
            public static int repayMonths;
            public static double MonthlyAmount;


            public static string ModelMake;
            public static double VechPurPrice;
            public static double TotalVechDeposit;
            public static double VechInterestRate;
            public static double VechInsurance;
            public static double Repayments;

            public static string ExpenseCategory;
            public static string ExpenseDescription;
            public static double ExpenseAmount;
            public static DateTime ExpenseDateTime;

            public static string InvestmentTitle;
            public static double MonthlyInvestmentAmount;
            public static double InvestmentInterestRate;
            public static int DurationofInvestment;
            public static double InvestmentGoal;
        }

        private void btnInSubmission_Click(object sender, RoutedEventArgs e)
        {
            Global.incomeBD = Int32.Parse(tbIncome.Text);

            //https://www.sars.gov.za/Tax-Rates/Income-Tax/Pages/Rates%20of%20Tax%20for%20Individuals.aspx - rate of TAX per individual 

            if (Global.incomeBD <= 6000)
            {
                Global.incomeTAX = 0;
                tbETDeduction.Text = "Not Applicable";
            }
            else if (Global.incomeBD <= 17158)
            {
                Global.incomeTAX = (Global.incomeBD * 18) / 100;
                tbETDeduction.Text = Global.incomeTAX.ToString();
            }
            else if (Global.incomeBD <= 26800)
            {
                Global.incomeTAX = (Global.incomeBD * 26) / 100;
                tbETDeduction.Text = Global.incomeTAX.ToString();
            }
            else if (Global.incomeBD <= 37092)
            {
                Global.incomeTAX = (Global.incomeBD * 31) / 100;
                tbETDeduction.Text = Global.incomeTAX.ToString();
            }
            else if (Global.incomeBD <= 48683)
            {
                Global.incomeTAX = (Global.incomeBD * 36) / 100;
                tbETDeduction.Text = Global.incomeTAX.ToString();
            }
            else if (Global.incomeBD <= 62067)
            {
                Global.incomeTAX = (Global.incomeBD * 39) / 100;
                tbETDeduction.Text = Global.incomeTAX.ToString();
            }
            else if (Global.incomeBD <= 131441)
            {
                Global.incomeTAX = (Global.incomeBD * 41) / 100;
                tbETDeduction.Text = Global.incomeTAX.ToString();
            }
            else
            {
                Global.incomeTAX = (Global.incomeBD * 45) / 100;
                tbETDeduction.Text = Global.incomeTAX.ToString();
            }

            Global.incomeAD = Global.incomeBD - Global.incomeTAX;
            tbIATax.Text = Global.incomeAD.ToString();
        }

        private void btnHTSelect_Click(object sender, RoutedEventArgs e)
        {
            if (radRenting.IsChecked== true)
            {
                tbMRA.IsEnabled = true;

                tbPPoP.IsEnabled = false;
                tbPPoP.Clear();
                tbTHDeposit.IsEnabled = false;
                tbTHDeposit.Clear();
                tbHIRate.IsEnabled = false;
                tbHIRate.Clear();
                tbHMRepay.IsEnabled = false;
                tbHMRepay.Clear();
            }
            else
            {
                tbMRA.IsEnabled = false;
                tbMRA.Clear();
                tbPPoP.IsEnabled = true;
                tbTHDeposit.IsEnabled = true;
                tbHIRate.IsEnabled = true;
                tbHMRepay.IsEnabled = true;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tbMRA.Text))
            {
                Global.PropertyPurPrice = Convert.ToDouble(tbPPoP.Text);
                Global.TotalDeposit = Convert.ToDouble(tbTHDeposit.Text);
                Global.InterestRate = Convert.ToDouble(tbHIRate.Text);

                //between 240 and 360 months 
                //https://stackoverflow.com/questions/2109441/how-to-show-error-warning-message-box-in-net-how-to-customize-messagebox

                if (Convert.ToInt32(tbHMRepay.Text) < 240)
                {
                    tbHMRepay.Clear();
                    MessageBox.Show("Please Select a value within a 240-360 month range", "Out Of Parameters");

                }
                else if (Convert.ToInt32(tbHMRepay.Text) > 360)
                {
                    tbHMRepay.Clear();
                    MessageBox.Show("Please Select a value within a 240-360 month range", "Out Of Parameters");

                }
                else
                {
                    Global.repayMonths = Int32.Parse(tbHMRepay.Text);
                }

                //calculation for monthly repayment values
                int InterestAnnun = Convert.ToInt32(Global.repayMonths) / 12;
                InterestAnnun.ToString("#,.##");

                Global.MonthlyAmount = ((((Global.InterestRate / 100) * InterestAnnun) + 1) * ((Global.PropertyPurPrice - Global.TotalDeposit)) / Global.repayMonths);

                //purchase display in the display box
                tbDisplay.Text = "ACCOMADATION REPORT" + "\r\nAccomadation Type:\tPurchasing Home" +
                                  "\r\nPrice Of Property:\tR " + Global.PropertyPurPrice.ToString("#.##") +
                                  "\r\nTotal Deposit:\tR " + Global.TotalDeposit.ToString("#.##") +
                                  "\r\nInterest Rate:\t" + Global.InterestRate.ToString("#.##") + "%" +
                                  "\r\nNumber of Monthly Repayments:\t" + Global.repayMonths.ToString("#.##") +
                                  "\r\nMonthly Repayment Value:\tR " + Global.MonthlyAmount.ToString("#.##");

                //Home Loan Repayment Alert
                if (Global.MonthlyAmount > (Global.incomeAD / 3))
                {
                    MessageBox.Show("Your Application for a Home Loan has been Declined,\r\nDue to the Monthly Repayment value being over a third of your salary", "Home Loan Application Assessment");
                }
                else
                {
                    MessageBox.Show("Your Application for a Home Loan has been Successful,\r\nCongratulations on your first step to owning your own Home!!!", "Home Loan Application Assessment");
                }

            }
            else
            {
                Global.MonthlyRental = Convert.ToDouble(tbMRA.Text);

                //rental display in the display textbox
                tbDisplay.Text = "ACCOMADATION REPORT" + "\r\nAccomadation Type:\tRenting" +
                                 "\r\nAccomadation Amount:\tR " + Global.MonthlyRental.ToString("#.##");
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            // this button clears whatever existing report is displayed in the Text Box serving as a Display
            tbDisplay.Clear();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            //this closes/terminates the program
            Close();
        }

        private void btnTransportationSelect_Click(object sender, RoutedEventArgs e)
        {
            if (radOtherTransportation.IsChecked == true)
            {
                tbVechMandM.IsEnabled = false;
                tbVehiclePPrice.IsEnabled = false;
                tbVechTDeposit.IsEnabled = false;
                tbVehicleIntRate.IsEnabled = false;
                tbEIP.IsEnabled = false;

                MessageBox.Show("Please Capture Monthly Travel Cost int the Expenditure Window", " Other Means of Transport Cost");
            }
            else
            {
                tbVechMandM.IsEnabled = true;
                tbVehiclePPrice.IsEnabled = true;
                tbVechTDeposit.IsEnabled = true;
                tbVehicleIntRate.IsEnabled = true;
                tbEIP.IsEnabled = true;

                MessageBox.Show("Please fill out the relevant fields below, to generate a vehicle finance report");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Global.ModelMake = tbVechMandM.Text;
            Global.VechPurPrice = Convert.ToDouble(tbVehiclePPrice.Text);
            Global.TotalVechDeposit = Convert.ToDouble(tbVechTDeposit.Text);
            Global.VechInterestRate = Convert.ToDouble(tbVehicleIntRate.Text);
            Global.VechInsurance = Convert.ToDouble(tbEIP.Text);
            int Duration = 60;

            Global.Repayments = ((((((Global.VechInterestRate / 100) * 5) + 1) * (Global.VechPurPrice - Global.TotalVechDeposit)) / Duration) + Global.VechInsurance);

            tbDisplay.Text = "VEHICLE FINANCING " + "\r\nMake and Model:\t" + Global.ModelMake + "\r\nVehicle Purchase Price:\tR " + Global.VechPurPrice.ToString("#.##") +
                             "\r\nTotal Deposit:\tR " + Global.TotalVechDeposit.ToString("#.##") + "\r\nInterest Rate(%/annum):\t" + Global.VechInterestRate.ToString("#.##") +
                             "\r\nMonthly Insurance Premium:\tR " + Global.VechInsurance.ToString("#.##") + "\r\nMonthly Repayments (excl Insurance):\tR " + (Global.Repayments - Global.VechInsurance).ToString("#.##") +
                             "\r\nMonthly Repayments(incl Insurance):\tR" + Global.Repayments.ToString("#.##");

            if (Global.Repayments > Global.incomeAD * 0.25 == true)
            {
                MessageBox.Show("WARNING" + "\r\n It isnt feasible to purchase this vehicle", "Vehicle Finance Advice");
            }
            else
            {
                MessageBox.Show("CONGRATULATIONS" + "\r\n It looks like your on your way to purchasing a vehicle", "Vehicle Finance Advice");
            }
        }

        private void btnISReport_Click(object sender, RoutedEventArgs e)
        {
            double totalInvestmentPayout;
            double monthlyInvestmentEarning;

            //retrieveing the values that the user has input into the sytem.
            Global.InvestmentTitle = tbISTitle.Text;
            Global.MonthlyInvestmentAmount = Convert.ToDouble(tbISAmount.Text);
            Global.InvestmentInterestRate = Double.Parse(tbISInterestRate.Text);
            Global.DurationofInvestment = int.Parse(tbDurationOfInvestment.Text);
            Global.InvestmentGoal = Double.Parse(tbISGoal.Text);

            double monthlyInterestRate = ((Global.InvestmentInterestRate/100) / 12);
            //calculation of the Saving Plan
            totalInvestmentPayout =(Global.MonthlyInvestmentAmount*monthlyInterestRate)*Global.DurationofInvestment;
            monthlyInvestmentEarning = (totalInvestmentPayout / Global.DurationofInvestment);


            //setting up the Display of the report thats generated
            tbDisplay.Text = "SAVINGS/INVESTMENT REPORTS" + "\r\nInvestment Title:\t" + Global.InvestmentTitle +
                                "\r\nMonthly Investment Contribution:\tR" + Global.MonthlyInvestmentAmount +
                                "\r\nInterest Rate:\t" + Global.InvestmentInterestRate + "\r\nDuration of Investment Period:\t" +
                                +Global.DurationofInvestment + "\r\nInvestment Goal:\t" + Global.InvestmentGoal +
                                "\r\n\r\nbased on the information provided, here's analysis of your investment" +
                                "\r\nThe monthly interest earned, based on your monthly installments is:\t R" + monthlyInvestmentEarning +
                                "\r\nThe total investment payout is:\r R" + totalInvestmentPayout;
                                ;


        }
    }
}
