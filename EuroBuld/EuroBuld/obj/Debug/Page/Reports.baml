   M S B A M L   `   `   `  ����U  QLiveCharts.Wpf, Version=0.9.7.0, Culture=neutral, PublicKeyToken=0bc1f845d1ebb8dfG4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.WpfLiveCharts.Wpf  5   	   C ?EuroBuld, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null-clr-namespace:EuroBuld.PageEuroBuld.Page 5   	      EuroBuld.Page.Reports   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   T PMahApps.Metro, Version=2.0.0.0, Culture=neutral, PublicKeyToken=51482d6f650b2b3f8metro,http://metro.mahapps.com/winfx/xaml/controls  5   	   >lvc4clr-namespace:LiveCharts.Wpf;assembly=LiveCharts.Wpf  5   	   'localclr-namespace:EuroBuld.Page  5   	     � WindowStartupLocation$  CenterScreen=�5
   	    � Title$! Панель отчетов��5   	   $	��700��5   	   $
��1200��6   .��5      � ���#FFFFFF.+�5   
   �� ���#E0BFBF$��	0,0,959,0q�6*   6?   �� 5   
   ���#E0BFBF$	��200��6*   $��20,270,980,0q�66   $	��Top=�6L   $	��313��6d   .+�5      �� -   5        $��
Назад��5      $��10q�6%   
������ 
�������
������ $��1q�6v   $
��10,5q�6�   ��5      �� 5       �� 
TargetType" ��6   .�5      ��  �� Property � �� �� Value
 ���� 67   �� 5       � �� �#88000067   �� 5       � ��
 ���� 68   �� 5       � ��$ 1q�6<   �� 5       � ��$
 10,5q�64   �� 5       � ��$ 14(�65   �� 5       � ��$
 Bold"�67    �� Triggers 5      P� 5       P� Property �@J�IsMouseOver P� Value . . �5   "   ��  � �� �#D9D9D96?   �� 5   "    � �� �#8800006?   5      5       5"      5#      �� 5$   
   -   5        $��ChartTypeComboBox��5$      $	��Top=�6,   $
��Left=�6D   $��
332,53,0,0q�6_   $	��605��6s   .@�5%      �� $$��Прибыль по годам��6   �� 5&      $9��3Количество заказов по годам��6   �� 5'      $R��LКоличество завершённых проектов по годам��6   �� 5(      $$��Доход по месяцам��6   �� 5)      $A��;Количество сотрудников по ролям��6   �� 5*      $=��7Количество заявок по статусам��6   5+      #   LiveCharts.Wpf.CartesianChart  5-   
   -   5        $��ProfitChart��5-      $��332,80,63,80q�60   &    LiveCharts.Wpf.Charts.Base.Chart   AxisX 5/         LiveCharts.Wpf.Axis  	   Title$	 Год��6   
   Labels
 6'   �� *Years+6B      AxisY 52        $	 
Сумма��6      LabelFormatter 6)   �� *ValueFormatter+6U   54      �� 55   
   $��0,609,360,-144q�6   .+�56      �� -   5        ���#217346$��15(�56   *   $��50��68   
�������$	��180��6W   $
��Bold"�6c   ��57      �� 58       �� 
TargetType" ��6&   .R�59      �� #��z��6!   #��z��5:      #��z��5;       �� CornerRadius$ 10��5<      $��0,5,0,0q�6$   ._�5=      �� $��Center=�6/   $��Center=�6L   $��Excel��6g   5>      5?      5A      5B      5C      5D      