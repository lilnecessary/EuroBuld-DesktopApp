using EuroBuld.DataBase;
using LiveCharts;
using LiveCharts.Wpf;
using OfficeOpenXml;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Word = Microsoft.Office.Interop.Word;
using System.Windows.Controls;
using System.Windows.Media;
using System.Runtime.InteropServices;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Microsoft.Win32;
using DocumentFormat.OpenXml.ExtendedProperties;
using Windows.Data.Pdf;
using DocumentFormat.OpenXml.Presentation;
using iText.IO.Font.Constants;
using iText.Kernel.Font;

namespace EuroBuld.Page
{
    public partial class Reports : Window
    {
        public Reports()
        {
            InitializeComponent();
            LoadChartData("Profit");
        }

        private void LoadChartData(string chartType)
        {
            using (var context = new EuroBuldEntities15())
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
                            Fill = new SolidColorBrush(Color.FromRgb(67, 1, 19))
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
                        .GroupBy(order => order.Status)
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


        private void Button_Click_Excel(object sender, RoutedEventArgs e)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx";
            saveFileDialog.DefaultExt = ".xlsx";
            saveFileDialog.FileName = "EuroBuld_Отчет.xlsx";

            string filePath = string.Empty;

            if (saveFileDialog.ShowDialog() == true)
            {
                filePath = saveFileDialog.FileName;

                using (ExcelPackage package = new ExcelPackage())
                using (var context = new EuroBuldEntities15())
                {
                    void AddHeader(ExcelWorksheet sheet, int columnCount)
                    {
                        var range = sheet.Cells[1, 1, 1, columnCount];
                        range.Merge = true;
                        range.Value = "EuroBuld";
                        range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        range.Style.Font.Size = 16;
                        range.Style.Font.Bold = true;
                        range.Style.Font.Color.SetColor(System.Drawing.ColorTranslator.FromHtml("#430113"));
                        range.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                    }

                    void AutoFitColumns(ExcelWorksheet sheet, int columnCount)
                    {
                        for (int col = 1; col <= columnCount; col++)
                        {
                            sheet.Column(col).AutoFit();
                        }
                    }

                    void AddBorders(ExcelWorksheet sheet, int rowCount, int columnCount)
                    {
                        for (int r = 1; r <= rowCount; r++)
                        {
                            for (int c = 1; c <= columnCount; c++)
                            {
                                var cell = sheet.Cells[r, c];
                                cell.Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                            }
                        }
                    }

                    var usersSheet = package.Workbook.Worksheets.Add("Users");
                    var users = context.Users.ToList();
                    AddHeader(usersSheet, 9);
                    usersSheet.Cells[2, 1].Value = "ID";
                    usersSheet.Cells[2, 2].Value = "Email";
                    usersSheet.Cells[2, 3].Value = "Password";
                    usersSheet.Cells[2, 4].Value = "Number_Phone";
                    usersSheet.Cells[2, 5].Value = "Address";
                    usersSheet.Cells[2, 6].Value = "First_name";
                    usersSheet.Cells[2, 7].Value = "Last_name";
                    usersSheet.Cells[2, 8].Value = "Patronymic";
                    usersSheet.Cells[2, 9].Value = "Passport_details";

                    int userRow = 3;
                    foreach (var user in users)
                    {
                        usersSheet.Cells[userRow, 1].Value = user.ID_Users;
                        usersSheet.Cells[userRow, 2].Value = user.Email;
                        usersSheet.Cells[userRow, 3].Value = user.Password;
                        usersSheet.Cells[userRow, 4].Value = user.Number_Phone;
                        //usersSheet.Cells[userRow, 5].Value = user.Address;
                        usersSheet.Cells[userRow, 6].Value = user.First_name;
                        usersSheet.Cells[userRow, 7].Value = user.Last_name;
                        usersSheet.Cells[userRow, 8].Value = user.Patronymic;
                        usersSheet.Cells[userRow, 9].Value = user.Passport_details;
                        userRow++;
                    }

                    AutoFitColumns(usersSheet, 9);
                    AddBorders(usersSheet, userRow - 1, 9);

                    var roleSheet = package.Workbook.Worksheets.Add("Role");
                    var roles = context.Role.ToList();
                    AddHeader(roleSheet, 3);
                    roleSheet.Cells[2, 1].Value = "ID_Role";
                    roleSheet.Cells[2, 2].Value = "roll_name";
                    roleSheet.Cells[2, 3].Value = "salary";

                    int roleRow = 3;
                    foreach (var role in roles)
                    {
                        roleSheet.Cells[roleRow, 1].Value = role.ID_Role;
                        roleSheet.Cells[roleRow, 2].Value = role.roll_name;
                        roleSheet.Cells[roleRow, 3].Value = role.salary;
                        roleRow++;
                    }

                    AutoFitColumns(roleSheet, 3);
                    AddBorders(roleSheet, roleRow - 1, 3);

                    var staffSheet = package.Workbook.Worksheets.Add("Staff");
                    var staff = context.Staff.ToList();
                    AddHeader(staffSheet, 10);
                    staffSheet.Cells[2, 1].Value = "ID_Staff";
                    staffSheet.Cells[2, 2].Value = "ID_Role";
                    staffSheet.Cells[2, 3].Value = "Email";
                    staffSheet.Cells[2, 4].Value = "Password";
                    staffSheet.Cells[2, 5].Value = "First_name";
                    staffSheet.Cells[2, 6].Value = "Last_name";
                    staffSheet.Cells[2, 7].Value = "Patronymic";
                    staffSheet.Cells[2, 8].Value = "Passport_details";
                    staffSheet.Cells[2, 9].Value = "Date_birth";
                    staffSheet.Cells[2, 10].Value = "Date_employment";

                    int staffRow = 3;
                    foreach (var s in staff)
                    {
                        staffSheet.Cells[staffRow, 1].Value = s.ID_Staff;
                        staffSheet.Cells[staffRow, 2].Value = s.ID_Role;
                        staffSheet.Cells[staffRow, 3].Value = s.Email;
                        staffSheet.Cells[staffRow, 4].Value = s.Password;
                        staffSheet.Cells[staffRow, 5].Value = s.First_name;
                        staffSheet.Cells[staffRow, 6].Value = s.Last_name;
                        staffSheet.Cells[staffRow, 7].Value = s.Patronymic;
                        staffSheet.Cells[staffRow, 8].Value = s.Passport_details;
                        staffSheet.Cells[staffRow, 9].Value = s.Date_birth.ToString("yyyy-MM-dd");
                        staffSheet.Cells[staffRow, 10].Value = s.Date_employment.ToString("yyyy-MM-dd");
                        staffRow++;
                    }

                    AutoFitColumns(staffSheet, 10);
                    AddBorders(staffSheet, staffRow - 1, 10);

                    var serviceSheet = package.Workbook.Worksheets.Add("Service");
                    var services = context.Service.ToList();
                    AddHeader(serviceSheet, 4);
                    serviceSheet.Cells[2, 1].Value = "ID_Service";
                    serviceSheet.Cells[2, 2].Value = "Item_Name";
                    serviceSheet.Cells[2, 3].Value = "Item_Description";
                    serviceSheet.Cells[2, 4].Value = "Price";

                    int serviceRow = 3;
                    foreach (var service in services)
                    {
                        serviceSheet.Cells[serviceRow, 1].Value = service.ID_Service;
                        serviceSheet.Cells[serviceRow, 2].Value = service.Item_Name;
                        serviceSheet.Cells[serviceRow, 3].Value = service.Item_Description;
                        serviceSheet.Cells[serviceRow, 4].Value = service.Price;
                        serviceRow++;
                    }

                    AutoFitColumns(serviceSheet, 4);
                    AddBorders(serviceSheet, serviceRow - 1, 4);
                    var customerOrdersSheet = package.Workbook.Worksheets.Add("Customer_orders");
                    var customerOrders = context.Customer_orders.ToList();
                    AddHeader(customerOrdersSheet, 5);
                    customerOrdersSheet.Cells[2, 1].Value = "ID_Customers_orders";
                    customerOrdersSheet.Cells[2, 2].Value = "ID_Service";
                    customerOrdersSheet.Cells[2, 3].Value = "ID_Users";
                    customerOrdersSheet.Cells[2, 4].Value = "Order_Date";
                    customerOrdersSheet.Cells[2, 5].Value = "Quantity";

                    int customerOrdersRow = 3;
                    foreach (var order in customerOrders)
                    {
                        customerOrdersSheet.Cells[customerOrdersRow, 1].Value = order.ID_Customers_orders;
                        customerOrdersSheet.Cells[customerOrdersRow, 2].Value = order.ID_Service;
                        customerOrdersSheet.Cells[customerOrdersRow, 3].Value = order.ID_Users;
                        customerOrdersSheet.Cells[customerOrdersRow, 4].Value = order.Order_Date?.ToString("yyyy-MM-dd");
                        customerOrdersRow++;
                    }

                    AutoFitColumns(customerOrdersSheet, 5);
                    AddBorders(customerOrdersSheet, customerOrdersRow - 1, 5);

                    var processedOrdersSheet = package.Workbook.Worksheets.Add("Processed_customer_orders");
                    var processedOrders = context.Processed_customer_orders.ToList();
                    AddHeader(processedOrdersSheet, 8);
                    processedOrdersSheet.Cells[2, 1].Value = "ID_Processed_customer_orders";
                    processedOrdersSheet.Cells[2, 2].Value = "ID_Customer_orders";
                    processedOrdersSheet.Cells[2, 3].Value = "ID_Staff";
                    processedOrdersSheet.Cells[2, 4].Value = "ID_Construction_Status";
                    processedOrdersSheet.Cells[2, 5].Value = "Project_Name";
                    processedOrdersSheet.Cells[2, 6].Value = "Date_Start";
                    processedOrdersSheet.Cells[2, 7].Value = "Date_Ending";
                    processedOrdersSheet.Cells[2, 8].Value = "Final_sum";

                    int processedOrdersRow = 3;
                    foreach (var processedOrder in processedOrders)
                    {
                        processedOrdersSheet.Cells[processedOrdersRow, 1].Value = processedOrder.ID_Processed_customer_orders;
                        processedOrdersSheet.Cells[processedOrdersRow, 2].Value = processedOrder.ID_Customer_orders;
                        processedOrdersSheet.Cells[processedOrdersRow, 3].Value = processedOrder.ID_Staff;
                        processedOrdersSheet.Cells[processedOrdersRow, 6].Value = processedOrder.Date_Start?.ToString("yyyy-MM-dd");
                        processedOrdersSheet.Cells[processedOrdersRow, 7].Value = processedOrder.Date_Ending?.ToString("yyyy-MM-dd");
                        processedOrdersSheet.Cells[processedOrdersRow, 8].Value = processedOrder.Final_sum;
                        processedOrdersRow++;
                    }

                    AutoFitColumns(processedOrdersSheet, 8);
                    AddBorders(processedOrdersSheet, processedOrdersRow - 1, 8);

                    package.SaveAs(new FileInfo(filePath));
                }

                MessageBoxResult result = MessageBox.Show("Отчет был сохранен. Хотите открыть файл?", "Открыть файл", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
                    {
                        FileName = filePath,
                        UseShellExecute = true
                    });
                }
            }
        }
    }
}

