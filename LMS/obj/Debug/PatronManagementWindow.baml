   M S B A M L   `   `   `  ����  LMS     LMS.PatronManagementWindow   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5        N� Title$  Patron Management��5       N� 
Background �#ffedd8.��5      �  � RowDefinitions 5   
   �� 5	      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��Patron Management System��6   $��24(�5      $
��Bold"�5      $��Center=�5      $��0,5,0,5q�5      ���#3d20155      � 5   
   $��1��6    � ColumnDefinitions 5      �� 5      $��2* �6#   �� 5      $��1* �6#   .+�5      �� $��Patron List��6   $��
10,0,10,10q�6,   $��0��6@   
�������
���   �$��1q�6w   .��5       �  5"      �� 5#      $��* �6(   �� 5%      $
��Auto �6(   .+�5*      &   System.Windows.Controls.DataGrid  -   5          Name$ PatronDataGrid��5*         AutoGenerateColumns .     CanUserAddRows .     
IsReadOnly .    SelectionMode$ Single=�5.      $��5q�5/      $��0��50      $��Visible=�51      $
��Auto=�52      
���   �
�������
���   �	   Columns	 56      0  *System.Windows.Controls.DataGridTextColumn  57      ,  &System.Windows.Controls.DataGridColumn
   Header$
 	Full Name��61      Width5  /System.Windows.Controls.DataGridLengthConverter$
 auto 6a   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6D   �� *
FullName+6a     58      $
 Membership ID��61   $
 auto 6i    6H   �� *MembershipID+6i     59      $
 Email��61   $
 auto 6Z    6@   �� *Email+6Z     5:      $
 Phone Number��61   $
 auto 6g    6G   �� *PhoneNumber+6g     5;      $
 Address��61   $
 auto 6^    6B   �� *	Address+6^     5<      $
 Date of Birth��61   $
 auto 6h    6H   �� *DisplayDate+6h     5=      $
 Membership Type��61   $
 auto 6m    6J   �� *MembershipType+6m     5>      $
 Status��61   $
 auto 6\    6A   �� *Status+6\   5@      �� 5C       �� Orientation$ 
Horizontal=�6!   $��Center=�6:   $��5q�6W   $��1��6b   .+�5D      �� -   5        $��Previous��5D   !   $	��100��64   $��5q�6@   6K   �� 5E      -   5         ��Name$ PageInfoTextBlock��5E   $   $��Center=�6?   $
��10,0q�6Z   �� 5F      -   5        $
��Next��5F   !   $	��100��60   $��5q�6<   6G   5G      5H      5I      �� 5L      $��1��6   $��10,10,10,10q�6)   .+�5M      �� $��Actions��6   $��0,0,0,10q�6,   
�������
���   �$��1q�6e   .��5N      �� $��5q�6!   .+�5O      �� -   5        $��Add New Patron��5O   !      SuccessButton#��[  5P   !   $	��0,5q�5Q   !   $��35��6.   6:   �� 5R      -   5        $��Edit Selected Patron��5R   !   $	��0,5q�5S   !   $��35��6.   6:   �� 5T      -   5        $��DeletePatronBtn��5T   !   $��Delete Selected Patron��68     DangerButton#��[ 5U   !   $	��0,5q�5V   !   $��35��6.   6:   5W      5X      �� 5Z      $��Search��6   $��0,10,0,0q�6+   
�������
���   �$��1q�6d   .��5[      �� $��5q�6!   .+�5\      � .+�5]      �� -   5         ��Name$ SearchTextBox��5]   &   $	��0,5q�5^   &   $��30��5_   &   5a   &   �� 5b      -	   5        $ SearchPlaceholder��5b   (   $��Search patron...��5c   (   
�������$��8,10,0,0q�5e   (    J� IsHitTestVisible .  5f   (   5g      �� 5h      -
   5        $��Search��5h   !   $��30��62   6>   5i      5j      5k      5l      5m      5n      