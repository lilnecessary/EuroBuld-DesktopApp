   M S B A M L   `   `   `  ����  EuroBuld"    EuroBuld.Page.ManagersOrders   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	     � WindowStartupLocation$  CenterScreen=�5   	    � Title$% Заказы менеджера��5   	   $	��700��5	   	   $
��1200��6    � 	Resources 5      &   System.Windows.Controls.DataGrid �� 
TargetType �� Property �� Value)  #System.Windows.Controls.DataGridRow=  7System.Windows.Controls.Primitives.DataGridColumnHeader��%5  '        �� 5   
   "  6   .�5      ��  �@ AutoGenerateColumns .  64   �� 5       �@ HeadersVisibility$ Column=�62   �� 5       �@ GridLinesVisibility$
 None=�64   �� 5       �@ 	RowHeight$ 50Q�6*   �� 5       �@ ColumnHeaderHeight$ 50Q�63   �� 5       � �� �#F8F8F86+   �� 5       � ��
 ���� 6,   �� 5       �@ RowStyle 5      �� 5      "  6   .�5      ��  � ��
 �����67   �� 5       � ��$ 0,5,0,5q�63   5      5      �� 5       �@ ColumnHeaderStyle 5      �� 5      "  6   .�5      ��  � �� �#F8F8F867   �� 5        � ��$
 Bold"�67   �� 5!       � �� �#43011367   �� 5"       � ��$ 14(�65   5#      5%      5&      5'      .��5)      � ���#FFFFFF.+�5*   
   �� ���#E0BFBF$��	0,0,956,0q�6*   6?   �� 5+   
   ���#E0BFBF$	��200��6*   $��
22,0,978,0q�66   $��Center=�6J   $��94��6e   .+�5,      �� -   5        $��Заказы��5,      $��0q�6&   
������ 
�������
������ $��1q�6v   $
��10,5q�6�   $��20(�6�   �� 5-      $��Мои заказы��6   $��10q�6*   
������ ���#430113
������ $��1q�6   $
��10,5q�6�   $��20(�6�   5.      � 5/   
   $��
200,0,10,0q�6    � RowDefinitions 50      �� 51      $
��Auto �6    �� 52      $��* �6    �� 53      $
��Auto �6    .+�56        -   5          Name$ managerOrdersDataGrid��56       %� IsVirtualizing . 	 %� VirtualizationMode$	 	Recycling=�59      $��
46,85,0,90q�5:      $
   HorizontalScrollBarVisibility$

 Auto=�5;      $��1��5<      $��2��6       CanUserAddRows .    CanUserDeleteRows .    
IsReadOnly .     AutoGenerateColumns .  ��57      ��    Columns 5A      0  *System.Windows.Controls.DataGridTextColumn  5B      ,  &System.Windows.Controls.DataGridColumn   Header$ Статус��6)      Width5  /System.Windows.Controls.DataGridLengthConverter$
 Auto 6g   1  +System.Windows.Controls.DataGridBoundColumn   Binding 69   �� *Status_Orders.Name_Status+6g     5C      $ Дата начала��6)   $
 Auto 6]    6>   �� *
Date_Start+6]     5D      $! Дата окончания��6)   $
 Auto 6a    6A   �� *Date_Ending+6a     5E      $+ %Окончательная сумма��6)   $
 Auto 6d    6F   �� *	Final_sum+6d   5G      �� 5H      $��2��6    �� Orientation$ 
Horizontal=�6&   $��Center=�6?   $
��0,10q�6\   .+�5I      �� -   5        $	��120��5I      $��20q�6%   ���#430113
�������$��40��6w   ��5J      �� 5K       �� 
TargetType" ��6*   .R�5L      �� #��z��6%   #��z��5M   %   #��z��5N   %    �� CornerRadius$ 10��5O   %   ._�5P   "   �� $��Center=�63   $��Center=�6P   $��Изменить��6k   5Q      5R      5T      5U      5V      5W      5X      