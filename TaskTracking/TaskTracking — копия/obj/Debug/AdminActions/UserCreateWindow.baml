   M S B A M L   `   `   `  ����G  CTaskTracking, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:TaskTracking.AdminActionsTaskTracking.AdminActions  5   	   0    *TaskTracking.AdminActions.UserCreateWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   9local'clr-namespace:TaskTracking.AdminActions     5   	     � Title$/  )Создание пользователя��5   	   $	��400��6+    � 
ResizeMode$ NoResize=�5	   	   $	��300��5
   	   .��5      � .+�5   
   �� .+�5   
   �� $��ФИО��6   $��
10,10,10,0q�5      �� 5   
   -   5         ��Name$ nameTextBox��5      $��10q�5      $��30��5      �� 5   
   $��Роль��6   $��
10,10,10,0q�5      �� 5   
   -   5         ��Name$ roleComboBox��5       �� DisplayMemberPath$ DisplayMember��5       �� SelectedValuePath$ Value��5      $��10q�5      $��30��5      �� 5   
   $��Логин (Email)��6   $��
10,10,10,0q�5      �� 5!   
   -   5        $ loginTextBox��5!      $��10q�5"      $��30��5#      �� 5$   
   $��Пароль��6   $��
10,10,10,0q�5%      �� 5'   
   -   5        $ passwordTextBox��5'      $��10q�5(      $��30��5)      �� 5+   
   -   5         ��Name$ 
saveButton��5+      $��Сохранить��5,      $��10q�5-      $��30��5.      50      51      52      