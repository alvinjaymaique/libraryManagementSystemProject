   M S B A M L   `   `   `  ����  LMS    LMS.MainWindow   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	     � Title$  Library Management System��5   	   $	��750��6+   $
��1250��68   $	��750��6E    � SizeToContent$ Height=�6U    � WindowStartupLocation$ CenterScreen=�6l    � 
ResizeMode$ NoResize=�6�   ���#5a3122.��5      � $��10q�6    � RowDefinitions 5	   
   �� 5
      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $	��61* �6   �� 5      $
��558* �6   .+�5   
   �� $��/9.png��6   $��86��5   	   $��69��5   	   $
��Left=�5   	   $	��Top=�5   	   $��
218,30,0,0q�6!   $��2��65    J� IsHitTestVisible .  6G   � 5   
   $��0��6   $��0,0,0,10q�6    � ColumnDefinitions 5      �� 5      $��* �6#   �� 5      $
��Auto �6#   .+�5      �� -   5         ��Name$ DateDisplay��5      $��0��5      $��14(�5      ���#3d2015
�������$��Center=�5"       �� Padding$
 10,5q�5#      �� 5&      -   5        $ UsernameDisplay��5&      $��0��5'      $��14(�5(      ���#ff7e2b$��Center=�5*      $��-700 ,0,0,0q�5+      $��Center=�5,      �� 5/      -   5        $��1��5/      $��Logout��50         DangerButton#��[  51      $	��100��52      $��35��53      54      55      �� 58   
   $��1��6   $*��$Welcome to Library Management System��59      $��30(�5:      $
��Bold"�5;      $��Center=�5<      $��0,0,0,15q�5=      
�������5>      �� 5A   
   -   5        $��2��5A      
�������$��2��6c   	 � 	Resources	 5B      
 �� 
TargetType �� Property �� Value �� 
TargetType �� CornerRadius ��Name �� Triggers P� Property P� Value �� 
TargetName��%�  '��       �� 5D      "
 ��6   .�5E      ��  � �� 5F      �� 5G      " ��6.   .R�5H   "   �� $��Border��6)   $��0,0,4,0q�5I   )   ���#d4a276$��0,0,0,2q�5K   )   
������ $ 5��5M   )   ._�5P   &   �� $ ContentSite��67   $��Center=�5Q   5   $��Center=�5R   5   $��Header��5S   5   $
��10,5q�5T   5   5U   #    5V   "   P� 5W   &    �@��
IsSelected . . �5X   *   �� $ Border��61    � �� �#2196F36\   �� 5Y   *    � �� �#3d20156G   5Z   '   P� 5[   &    �@��
IsSelected .  . �5\   *   ��  � ��
 �����6G   5]   '   5_      5a      �� 5b       � ��$ 14(�61   �� 5c       � ��$
 15,8q�60   5d      5e      ��5g      �� 5h      " ��6"   .R�5i      �  5j      �� 5k      $
��Auto �6,   �� 5l      $��* �6,   .+�5p      �� $��0��6!   ���#ffedd8$��0,0,0,1q�5r   !   ���#E0E0E0._�5t      ��  ��Name$ HeaderPanel��6'    K� ZIndex$ 1��5u   %   -  'System.Windows.Input.KeyboardNavigation   TabIndex$ 1��5v   %    K� IsItemsHost . 5w   %   5x      �� 5{      $��1��6!   
�������$��0q�5}   !      TabNavigation$ Local=�5~   !      DirectionalNavigation$ 	Contained=�5   !   $ 2��5�   !   ._�5�      �� $ PART_SelectedContentHost��6/   $��SelectedContent��5�   -   5�      5�      5�      .@�5�      �� $��Book Management��6   .��5�       � -   5        $��BookManagementFrame��5�      5�      �� 5�      $��Patron Management��6   .��5�       � -   5        $��PatronManagementFrame��5�      5�      �� 5�      $��Transaction Management��6   .��5�       � -   5        $ ��TransactionManagementFrame��5�      5�      �� 5�      -   5        $��ReportAnalyticsTab��5�      $��Report and Analytics��60   .��5�       � -	   5        $��ReportAndAnalyticsFrame��5�      5�      5�      5�      5�      