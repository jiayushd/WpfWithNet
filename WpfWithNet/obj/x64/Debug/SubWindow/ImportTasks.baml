   M S B A M L   `   `   `  ����E  AWpfWithNet, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:WpfWithNet.SubWindowWpfWithNet.SubWindow  5   	   &     WpfWithNet.SubWindow.ImportTasks   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   4local"clr-namespace:WpfWithNet.SubWindow     5   	     � Title$  添加案件��5   	   $	��510��6   $	��600��6"    � 
ResizeMode$ NoResize=�6/    � WindowStartupLocation$ CenterScreen=�6E   .��5	      � 
������� � RowDefinitions 5
   
   �� 5      $	��160 �6   �� 5      .+�5   
   �� $	��160��6   .+�5      �� $��添加文件��6      
lableStyle#���   6#   � 5      $��30��6   $��10q�6$   .+�5      �� $��导入任务列表：��5      �� 5      -   5         ��Name$ 
tbTasklist��5      $	��300��6)   �� 5      -   5         ��Name$ btnBrowseTasks��5      $��	浏览...��5      $��40��5      $��Right=�5      
������ ��5      Z� 5      �� 5      -   5        $ btnImportTasks��5      $��导入��5       $��40��5!      
������ ��5#      Z� 5$      5.      � 5/      $��30��50      $��10q�51      .+�52      �� $��导入案件列表：��53      �� 54      -   5        $ 
tbCaselist��55      $	��300��56      �� 57      -   5        $ btnBrowseCases��58      $��	浏览...��59      $��40��5:      $��Right=�5;      
������ ��5=      Z� 5>      �� 5@      -   5        $ btnImportCases��5A      $��导入��5B      $��40��5C      
������ ��5E      Z� 5F      5I      5�      5�      5�      