   M S B A M L   `   `   `  ����G  CTaskTracking, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:TaskTrackingTaskTracking  5   	       TaskTracking.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:TaskTracking     5   	     � 
ResizeMode$  NoResize=�5   	    � Title$ Авторизация��5	      $	��200��6   $	��300��6)   .��5
      �  � ColumnDefinitions 5   
   �� 5      �� 5      $��2* �6    � RowDefinitions 5   
   �� 5      �� 5      �� 5      .+�5   
   �� $��
Логин��5      $��Center=�5      $��Center=�5      �� 5   
   $��Пароль��5      $��1��5      $��Center=�5      $��Center=�5      �� 5    
   -   5         ��Name$ loginTextBox��5!      $��1��5"      $��Center=�5#      $��12(�5$      $��2q�5%      $��15q�5&      G� 5(   
   -   5        $��passwordTextBox��5)      $��1��5*      $��1��5+      $��Center=�5,      $��12(�5-      $��2q�5.      $��15q�5/      �� 52   
   -   5         ��Name$ loginButton��53      $��
Войти��54      $��2��55      $��2��56      $��15q�57      58      5;      5<      