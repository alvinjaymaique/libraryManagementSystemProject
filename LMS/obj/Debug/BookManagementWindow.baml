   M S B A M L   `   `   `  ����  LMS    LMS.BookManagementWindow   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5        N� Title$  Book Management��5       N� 
Background �#ffedd8.��5      �  � RowDefinitions 5   
   �� 5	      $
��Auto �6   �� 5      $��* �6   .+�5   
   �� $��Book Management System��6   $��24(�5      $
��Bold"�5      $��Center=�5      $��0,5,0,5q�5      ���#3d20155      � 5   
   $��1��6    � ColumnDefinitions 5      �� 5      $	��32* �6#   �� 5      $
��555* �6#   �� 5      $
��293* �6#   .+�5       �� $��	Book List��6   $��
10,0,10,10q�6*   $��2��6>   
�������
���   �$��1q�6y   .��5!      �  5#      �� 5$      $��* �6(   �� 5&      $
��Auto �6(   .+�5+      &   System.Windows.Controls.DataGrid  -   5          Name$ BookDataGrid��5+         AutoGenerateColumns .     CanUserAddRows .     
IsReadOnly .    SelectionMode$ Single=�5/      $��5q�50      $��0��51      $��Visible=�52      $��Disabled=�53      
���   �
�������
���   �	   Columns	 59      0  *System.Windows.Controls.DataGridTextColumn  5;      ,  &System.Windows.Controls.DataGridColumn
   Header$
 Title��61      Width5  /System.Windows.Controls.DataGridLengthConverter$
 auto 6Z   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6@   �� *Title+6Z     5<      $
 Author��61   $
 auto 6\    6A   �� *Author+6\     5=      $

 ISBN��61   $
 auto 6X    6?   �� *ISBN+6X     5>      $
 Genre��61   $
 auto 6Z    6@   �� *Genre+6Z     5?      $
 	Publisher��61   $
 auto 6b    6D   �� *	Publisher+6b     5@      $
 Year Published��61   $
 auto 6k    6I   �� *YearPublished+6k     5A      $
 Copies Available��61   $
 auto 6n    6K   �� *NumberOfCopies+6n   5C      �� 5F       �� Orientation$ 
Horizontal=�6!   $��Center=�6:   $��5q�6W   $��1��6b   .+�5G      �� -   5        $��PrevBtn��5G   !   $��Previous��60   $	��100��6C   $��5q�6O   6Z   �� 5H      -   5         ��Name$ PageInfoTextBlock��5H   $   $��Center=�6?   $
��10,0q�6Z   �� 5I      -   5         ��Name$ NextBtn��5I   !   $
��Next��62   $	��100��6A   $��5q�6M   6X   5J      5K      5L      �� 5O      $��2��6   $��10,10,10,10q�6)   .+�5P      �� $��Actions��6   $��0,0,0,10q�6,   
�������
���   �$��1q�6e   .��5Q      �� $��5q�6!   .+�5R      �� -   5        $��Add New Book��5R   !      SuccessButton#��[  5S   !   $	��0,5q�5T   !   $��35��6.   6:   �� 5U      -   5        $��Edit Selected Book��5U   !   $	��0,5q�5V   !   $��35��6.   6:   �� 5W      -   5        $��DeleteBookBtn��5W   !   $��Delete Selected Book��66     DangerButton#��[ 5X   !   $	��0,5q�5Y   !   $��35��6.   6:   5Z      5[      �� 5]      $��Search��6   $��0,10,0,0q�6+   
�������
���   �$��1q�6d   .��5^      �� $��5q�6!   .+�5_      � .+�5`      �� -   5         ��Name$ SearchTextBox��5`   &   $	��0,5q�5a   &   $��30��5b   &   5d   &   �� 5e      -	   5        $ SearchPlaceholder��5e   (   $��Search book...��5f   (   
�������$��8,10,0,0q�5h   (    J� IsHitTestVisible .  5i   (   5j      �� 5k      -
   5        $��Search��5k   !   $��30��62   6>   5l      5m      5n      5o      5p      5q      