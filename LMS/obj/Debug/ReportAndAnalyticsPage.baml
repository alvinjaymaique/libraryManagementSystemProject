   M S B A M L   `   `   `  ����>  :LMS, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:LMSLMS  5           LMS.ReportAndAnalyticsPage   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      #localclr-namespace:LMS     5        N� Title$  ReportAndAnalyticsPage��5	       N� 
Background �#ffedd8.��5      � .+�5   
   �� -   5        $��ReportAnalyticsTabControl��5      $��10q�6e    � 	Resources 5       �� 
TargetType �� Property �� Value �� 
TargetType ��Name �� Triggers	 P� Property
 P� Value �� 
TargetName��%�  '��       �� 5      " ��6   .�5      ��  � �� 5      �� 5      " ��6.   .R�5   "   �� $��Border��6)   $��0,0,4,0q�5   )   
������ $��0,0,0,2q�5   )   
������ ._�5   &   �� $ ContentSite��67   $��Center=�5   5   $��Center=�5   5   $��Header��5   5   $
��10,5q�5   5   5   #    5   "   P� 5    &   	 �@��
IsSelected
 . . �5!   *   �� $ Border��61    � �� �#3d20156\   �� 5"   *    � �� �#3d20156G   5#   '   P� 5$   &   	 �@��
IsSelected
 .  . �5%   *   ��  � �� �#6666666G   5&   '   5(      5*      �� 5+       � ��$ 14(�61   �� 5,       � ��$
 15,8q�60   5-      5.      .@�50      �� -   5        $��
OverDueTab��50      $��Overdue Books Report��6(   .��51      �� $��10q�6   .+�52      &   System.Windows.Controls.DataGrid  -   5        $��OverdueBooksDataGrid��52         AutoGenerateColumns .     CanUserAddRows .     
IsReadOnly .    SelectionMode$ Single=�53   1   $��Stretch=�6H   $	��300��6f   
�������
���   �   Columns 55      0  *System.Windows.Controls.DataGridTextColumn  56      ,  &System.Windows.Controls.DataGridColumn   Header$ 
Book Title��61      Width5  /System.Windows.Controls.DataGridLengthConverter$
 auto 6c   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6E   �� *	BookTitle+6c     57      $ Patron Name��61   $
 auto 6e    6F   �� *
PatronName+6e     58      $ Membership ID��61   $
 auto 6e    6H   �� *
MemberID+6e     59      $ Phone��61   $
 auto 6Z    6@   �� *Phone+6Z     5:      $ Email��61   $
 auto 6Z    6@   �� *Email+6Z     5;      $ Due Date��61   $
 auto 6f    6C   �� *DisplayDueDate+6f     5<      $ Overdue Days��61   $
 auto 6g    6G   �� *OverdueDays+6g     5=      $ Fine Amount��61   $
 auto 6e    6F   �� *
FineAmount+6e   5?      �� 5A       �� Orientation$ 
Horizontal=�6!   $��Center=�6:   $��0,10,0,0q�6W   .+�5B      �� -   5        $��PreviousOverdueButton��5B   !   $��Previous��6>   $��5q�6Q   $	��100��5C   "   $��35��6.   �� 5D      -   5        $��NextOverdueButton��5D   !   $
��Next��6:   $��5q�6I   $	��100��5E   "   $��35��6.   5F      �� 5H      -   5        $��ExportOverdueBooksButton��5H      $��Export to CSV��6=   $��Right=�6U   $��5q�5I      $��Bottom=�6(   $	��200��6C   $��35��6O   5J      5K      5L      �� 5N      -   5        $��CheckedOutTab��5N      $��Checked Out Books Report��6+   .��5O      �� $��10q�6   .+�5P        -   5        $��CheckedOutBooksDataGrid��5P       .   .   . $ Single=�5Q   1   $��Stretch=�6H   $	��300��6f   
�������
���   � 5S        5T      $ 
Book Title��61   $
 auto 6c    6E   �� *	BookTitle+6c     5U      $ Author��61   $
 auto 6\    6A   �� *Author+6\     5V      $ Due Date��61   $
 auto 6c    6C   �� *DisplayDate+6c   5X      �� 5Z      $ 
Horizontal=�6!   $��Center=�6:   $��0,10,0,0q�6W   .+�5[      �� -	   5        $��PreviousCheckedOutButton��5[   !   $��Previous��6A   $��5q�6T   $	��100��5\   "   $��35��6.   �� 5]      -
   5        $��NextCheckedOutButton��5]   !   $
��Next��6=   $��5q�6L   $	��100��5^   "   $��35��6.   5_      �� 5a      -   5        $!��ExportCheckedOutBooksButton��5a      $��Export to PDF/Excel/CSV��6@   $��Right=�6b   $��5q�5b      $��Bottom=�6(   $	��200��6C   $��35��6O   5c      5d      5e      �� 5g      -   5        $��TransactionTab��5g      $ ��Transaction History Report��6,   .��5h      �� $��10q�6   .+�5j      �� $ 
Horizontal=�6!   $
��Left=�6:   $��0,0,0,10q�6U   .+�5k      �� $��From:��6$   $��Center=�61   $��0,0,5,0q�6L   ���#3d20156]   (  "System.Windows.Controls.DatePicker  5l      -   5        $��TransactionFromDatePicker��5l   %   $	��150��6F   $��0,0,10,0q�6R   �� 5m      $	��To:��6$   $��Center=�6/   $��10,0,5,0q�6J   ���#3d20156\     5n      -   5        $��TransactionToDatePicker��5n   %   $	��150��6D   �� 5o      -   5        $��Apply��5o   !   $��10,0,0,0q�61   6C   5p        5s      -   5        $ ��TransactionHistoryDataGrid��5s       .   .   . $ Single=�5t   1   $��Stretch=�6H   $	��300��6f   
�������
���   � 5v        5w      $ Transaction ID��61   $
 auto 6k    6I   �� *TransactionID+6k     5x      $ Patron Name��61   $
 auto 6e    6F   �� *
PatronName+6e     5y      $ 
Book Title��61   $
 auto 6c    6E   �� *	BookTitle+6c     5z      $ Book Author��61   $
 auto 6e    6F   �� *
BookAuthor+6e     5{      $ Checkout Date��61   $
 auto 6p    6H   �� *DisplayCheckOutDate+6p     5|      $ Due Date��61   $
 auto 6f    6C   �� *DisplayDueDate+6f     5}      $ Return Date��61   $
 auto 6l    6F   �� *DisplayReturnDate+6l   5      �� 5�      $ 
Horizontal=�6!   $��Center=�6:   $��0,10,0,0q�6W   .+�5�      �� -   5        $��PreviousTransactionButton��5�   !   $��Previous��6B   $��5q�6U   $	��100��5�   "   $��35��6.   �� 5�      -   5        $��NextTransactionButton��5�   !   $
��Next��6>   $��5q�6M   $	��100��5�   "   $��35��6.   5�      �� 5�      -   5        $$��ExportTransactionHistoryButton��5�      $��Export to PDF/Excel/CSV��6C   $��Right=�6e   $��5q�5�      $��Bottom=�6(   $	��200��6C   $��35��6O   5�      5�      5�      �� 5�      -   5        $��PatronActivityTab��5�      $��Patron Activity Report��6/   .��5�      �� $��10q�6   .+�5�        -   5        $��PatronActivityDataGrid��5�       .   .   . $ Single=�5�   3   $��Stretch=�6J   $	��300��6h    5�        5�      $ Patron Name��61   $
 auto 6e    6F   �� *
PatronName+6e     5�      $ Membership ID��61   $
 auto 6e    6H   �� *
MemberID+6e     5�      $ Membership Type��61   $
 auto 6i    6J   �� *
MemberType+6i     5�      $ Status��61   $
 auto 6\    6A   �� *Status+6\     5�      $ Total Books Checked Out��61   $
 auto 6y    6R   �� *NumBooksCheckedOut+6y     5�      $ Total Overdue Books��61   $
 auto 6r    6N   �� *NumOverdueBooks+6r     5�      $ Total Fines��61   $
 auto 6`    6F   �� *Fines+6`   5�      �� 5�      $ 
Horizontal=�6!   $��Center=�6:   $��0,10,0,0q�6W   .+�5�      �� -   5        $"��PreviousPatronActivityButton��5�   !   $��Previous��6E   $��5q�6X   $	��100��5�   "   $��35��6.   �� 5�      -   5        $��NextPatronActivityButton��5�   !   $
��Next��6A   $��5q�6P   $	��100��5�   "   $��35��6.   5�      �� 5�      -   5        $ ��ExportPatronActivityButton��5�      $��Export to PDF/Excel/CSV��6?   $��Right=�6a   $��5q�5�      $��Bottom=�6(   $	��200��6C   $��35��6O   5�      5�      5�      5�      5�      5�      