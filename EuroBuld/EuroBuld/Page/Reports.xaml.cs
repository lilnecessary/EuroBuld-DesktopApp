using EuroBuld.DataBase;
using LiveCharts;
using LiveCharts.Wpf;
using OfficeOpenXml;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Runtime.InteropServices;

namespace EuroBuld.Page
{
    public partial class Reports : Window
    {
        public Reports()
        {
            InitializeComponent();
            LoadChartData("Profit"); // Загружаем данные для прибыли по умолчанию
        }

        private void LoadChartData(string chartType)
        {
            using (var context = new EuroBuldEntities7())
            {
                if (chartType == "Profit")
                {
                    // График прибыли по годам
                    var rawData = context.Processed_customer_orders
                        .Where(order => order.Date_Ending.HasValue)
                        .Select(order => new
                        {
                            Year = order.Date_Ending.Value.Year,
                            FinalSum = order.Final_sum
                        })
                        .ToList();

                    var profitsByYear = rawData
                        .GroupBy(order => order.Year)
                        .Select(g => new
                        {
                            Year = g.Key,
                            TotalProfit = g.Sum(o => decimal.TryParse(o.FinalSum, out var sum) ? sum : 0)
                        })
                        .OrderBy(x => x.Year)
                        .ToList();

                    var years = profitsByYear.Select(x => x.Year.ToString()).ToList();
                    var profits = profitsByYear.Select(x => (double)x.TotalProfit).ToList();

                    ProfitChart.AxisX[0].Labels = years;
                    ProfitChart.Series = new SeriesCollection
                    {
                        new ColumnSeries
                        {
                            Title = "Прибыль",
                            Values = new ChartValues<double>(profits),
                            Fill = new SolidColorBrush(Color.FromRgb(67, 1, 19))
                        }
                };
                }
                else if (chartType == "Orders")
                {
                    // График количества заказов по годам
                    var orderData = context.Customer_orders
                        .Where(order => order.Order_Date.HasValue)
                        .Select(order => new
                        {
                            Year = order.Order_Date.Value.Year
                        })
                        .ToList();

                    var ordersByYear = orderData
                        .GroupBy(order => order.Year)
                        .Select(g => new
                        {
                            Year = g.Key,
                            OrderCount = g.Count()
                        })
                        .OrderBy(x => x.Year)
                        .ToList();

                    var years = ordersByYear.Select(x => x.Year.ToString()).ToList();
                    var orderCounts = ordersByYear.Select(x => (double)x.OrderCount).ToList();

                    ProfitChart.AxisX[0].Labels = years;
                    ProfitChart.Series = new SeriesCollection
                    {
                        new ColumnSeries
                        {
                            Title = "Количество заказов",
                            Values = new ChartValues<double>(orderCounts),
                            Fill = new SolidColorBrush(Color.FromRgb(67, 1, 19)) // Цвет #430113
                        }
                    };
                }
                else if (chartType == "CompletedProjects")
                {
                    // График количества завершённых проектов по годам
                    var completedProjectsData = context.Processed_customer_orders
                        .Where(order => order.Date_Ending.HasValue)
                        .Select(order => new
                        {
                            Year = order.Date_Ending.Value.Year
                        })
                        .ToList();

                    var completedProjectsByYear = completedProjectsData
                        .GroupBy(order => order.Year)
                        .Select(g => new
                        {
                            Year = g.Key,
                            ProjectCount = g.Count()
                        })
                        .OrderBy(x => x.Year)
                        .ToList();

                    var years = completedProjectsByYear.Select(x => x.Year.ToString()).ToList();
                    var projectCounts = completedProjectsByYear.Select(x => (double)x.ProjectCount).ToList();

                    ProfitChart.AxisX[0].Labels = years;
                    ProfitChart.Series = new SeriesCollection
                    {
                        new ColumnSeries
                        {
                            Title = "Завершённые проекты",
                            Values = new ChartValues<double>(projectCounts),
                            Fill = new SolidColorBrush(Color.FromRgb(67, 1, 19)) // Цвет #430113
                        }
                    };
                }
                else if (chartType == "OrdersByStatus")
                {
                    // График количества заявок по статусам
                    var ordersByStatusData = context.Requests
                        .GroupBy(order => order.Status) // Предполагается, что у вас есть поле Status в таблице Customer_orders
                        .Select(g => new
                        {
                            Status = g.Key,
                            OrderCount = g.Count()
                        })
                        .OrderBy(x => x.Status)
                        .ToList();

                    var statuses = ordersByStatusData.Select(x => x.Status).ToList();
                    var orderCounts = ordersByStatusData.Select(x => (double)x.OrderCount).ToList();

                    ProfitChart.AxisX[0].Labels = statuses;
                    ProfitChart.Series = new SeriesCollection
                    {
                        new ColumnSeries
                        {
                            Title = "Количество заявок",
                            Values = new ChartValues<double>(orderCounts),
                            Fill = new SolidColorBrush(Color.FromRgb(67, 1, 19))
                        }
                    };
                }

                else if (chartType == "EmployeesByRole")
                {
                    // График количества сотрудников по ролям
                    var employeesByRoleData = context.Staff
                        .GroupBy(staff => staff.Role.roll_name)
                        .Select(g => new
                        {
                            Role = g.Key,
                            EmployeeCount = g.Count()
                        })
                        .OrderBy(x => x.Role)
                        .ToList();

                    var roles = employeesByRoleData.Select(x => x.Role).ToList();
                    var employeeCounts = employeesByRoleData.Select(x => (double)x.EmployeeCount).ToList();

                    ProfitChart.AxisX[0].Labels = roles;
                    ProfitChart.Series = new SeriesCollection
                    {
                        new ColumnSeries
                        {
                            Title = "Количество сотрудников",
                            Values = new ChartValues<double>(employeeCounts),
                            Fill = new SolidColorBrush(Color.FromRgb(67, 1, 19))
                        }
                    };
                }
                else if (chartType == "MonthlyIncome")
                {
                    var monthlyIncomeData = context.Processed_customer_orders
                        .Where(order => order.Date_Ending.HasValue)
                        .Select(order => new
                        {
                            Month = order.Date_Ending.Value.Month,
                            FinalSum = order.Final_sum
                        })
                        .ToList();

                    var incomeByMonth = monthlyIncomeData
                        .GroupBy(order => order.Month)
                        .Select(g => new
                        {
                            Month = g.Key,
                            TotalIncome = g.Sum(o => decimal.TryParse(o.FinalSum, out var sum) ? sum : 0)
                        })
                        .OrderBy(x => x.Month)
                        .ToList();

                    var months = incomeByMonth.Select(x => new DateTime(1, x.Month, 1).ToString("MMMM")).ToList();
                    var income = incomeByMonth.Select(x => (double)x.TotalIncome).ToList();

                    ProfitChart.AxisX[0].Labels = months;
                    ProfitChart.Series = new SeriesCollection
                    {
                        new ColumnSeries
                        {
                            Title = "Доход",
                            Values = new ChartValues<double>(income),
                            Fill = new SolidColorBrush(Color.FromRgb(67, 1, 19))
                        }
                    };
                }
            }
        }


        private void ChartTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (ComboBoxItem)ChartTypeComboBox.SelectedItem;
            string selectedType = selectedItem.Content.ToString();

            if (selectedType == "Прибыль по годам")
            {
                LoadChartData("Profit");
            }
            else if (selectedType == "Количество заказов по годам")
            {
                LoadChartData("Orders");
            }
            else if (selectedType == "Количество завершённых проектов по годам")
            {
                LoadChartData("CompletedProjects");
            }
            else if (selectedType == "Доход по месяцам")
            {
                LoadChartData("MonthlyIncome");
            }
            else if (selectedType == "Количество сотрудников по ролям")
            {
                LoadChartData("EmployeesByRole");
            }
            else if (selectedType == "Количество заявок по статусам")
            {
                LoadChartData("OrdersByStatus");
            }
        }


        private void bac_click(object sender, RoutedEventArgs e)
        {
            this.Close();
            AdminPage adminPage = new AdminPage();
            adminPage.Show();
            this.Visibility = Visibility.Collapsed;
        }


       
    }
}
