   M S B A M L   `   `   `  ����D  @BeckerBox, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null7 clr-namespace:BeckerBox.QBWindowBeckerBox.QBWindow  5   	   &     BeckerBox.QBWindow.Recalibration   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   2local clr-namespace:BeckerBox.QBWindow     5   	   -   5          � Title$  Recalibration��5   	   $	��800��68   $	��500��6E   .��5	      �  � RowDefinitions 5
   
   �� 5      $
��1.5* �6   6,   �� 5      $��1* �6   6*   �� 5      $��1* �6   6*   .+�5   
   � $��0��6    5      �� 5      $��1* �6    6.   �� 5      $
��1.5* �6    60   .+�5      �� $��0��6   $��Center=�6%   $��Center=�6B   
���  ��$��55(�6n   Calibration is bad?6�   � 5      -   5         �Name$ RecalibrateParent��5       $��1��6   .+�5      � -   5        $ Recalibrate��5       5      �� 5      $��1* �6(   66   �� 5      $
��1.5* �6(   68   .+�5      �� $��0��6    $��Center=�6-   $��Center=�6J   
��� � �$��26(�6x   .,Recalibrate in (press the button to cancel):6�   �� 5      -   5         ��Name$ _CountDownTB��5       $��1��66   $��Center=�6C   $��Center=�6`   $	��130(�6{   
���  �� �� Padding$ 0,0,0,20q�6�   6�   5      5      5       �� 5!   
   -   5         ��Name$ 
_CancelBtn��5!      $��1��6%   $��5q�62   .��6S   �� $��Stretch=�6]   $��Stretch=�6{   $	��130(�6�   Cancel6�   6�   �� 5"   
   -   5        $ _BBtn��5"      $��2��6    $��5q�6-   .��5#      �� $��Stretch=�6   $��Stretch=�66   $��90(�6R   Recalibrate6m   5$      5%      5&      