   M S B A M L   `   `   `  ����D  @Auto_Demo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:Auto_Demo.componentsAuto_Demo.components  5      '    !Auto_Demo.components.notification   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4local"clr-namespace:Auto_Demo.components     5      -   5          >�Name$
  root��5       � 	Resources 5       �� MergedDictionaries �� Source�� 5	   
    5
      �� 5      $  ../icons/noti-success.xaml?�6%   6K   �� 5      $ ../icons/noti-error.xaml?�6%   6I   �� 5      $ ../icons/noti-info.xaml?�6%   6H   �� 5      $  ../icons/noti-warning.xaml?�6%   6K   %    5      ��5      �� 5   
   .R�5      � $��0 9 0 0q�6    � ColumnDefinitions 5      �� 5      $	��42* �6'   ��63   ��  �� Path$ ActualWidth� �� ElementName$
 aaa1��6n   .+�5      ��  �� CornerRadius$ 8��6   
���   �$��70��6>   $��Right=�6J   ��6f   �� $ ActualWidth�$
 aaa1����5      �� 5       �� EndPoint$ 0.5,1&�6.   	 �� 
StartPoint$	 0.5,0&�6?   
 �� RelativeTransform
 5      V� 5   "   .~�5   &   ��  �� CenterY$	 0.5Q�65    �� CenterX$	 0.5Q�6C   �� 5   &    �� CenterY$	 0.5Q�64    �� CenterX$	 0.5Q�6B   �� 5   &    �� Angle$	 270Q�66    �� CenterY$	 0.5Q�6B    �� CenterX$	 0.5Q�6P   T� 5    &   5!   #   .��5#      �  � Offset$ 0Q�6b    � Color 6+   �� $ colorMessage1�$
 root��6b   � 5$      $ 0.125Q�6b    6+   �� $ colorMessage2�$
 root��6b   � 5%      $ 0.234Q�6b    6+   �� $ colorMessage3�$
 root��6b   � 5&      $ 0.324Q�6b    6+   �� $ colorMessage4�$
 root��6b   5'      ._�5*      �  �Name$
 aaa1��6   $��0��6)   $��54��66   ��6B   �� $ ActualWidth�$
 aaa1����5+      �� 5,      $ 0.5,1&�62   $	 0.5,0&�6C   
 5-   "   V� 5.   &   .~�5/   *   �� $	 0.5Q�69   $	 0.5Q�6G   �� 50   *   $	 0.5Q�68   $	 0.5Q�6F   �� 51   *   $	 270Q�6:   $	 0.5Q�6F   $	 0.5Q�6T   T� 52   *   53   '   .��55   "   � $ 0Q�6f    6/   �� $ colorMessage1�$
 root��6f   � 56   "   $ 0.125Q�6f    6/   �� $ colorMessage2�$
 root��6f   � 57   "   $ 0.234Q�6f    6/   �� $ colorMessage3�$
 root��6f   � 58   "   $ 0.324Q�6f    6/   �� $ colorMessage4�$
 root��6f   59       5<      �� 5=      $��50 �6/   6<   �� 5>      $
��auto �6/   6>   .+�5@      ��  J� Opacity$	 0.8Q�6    $��0 0 0 0q�6.   $��0��6?      tickcirclesvg#��[  6�   $��24��6�   $��24��6�   y�6O   �� $ iconSuccess�$
 root��5A      �� 5B      $	 0.8Q�6    $��0 0 0 0q�6.   $��0��6?     closecirclesvg#��[ 6�   $��24��6�   $��24��6�   y�6O   �� $ 	iconError�$
 root��5C      �� 5D      $	 0.8Q�6    $��0 0 0 0q�6.   $��0��6?     infocirclesvg#��[ 6�   $��24��6�   $��24��6�   y�6O   �� $ iconInfo�$
 root��5E      �� 5F      $	 0.8Q�6    $��0 0 0 0q�6.   $��0��6?     	dangersvg#��[ 6�   $��24��6�   $��24��6�   y�6P   �� $ iconWarning�$
 root��5G      � 5H      $��1��6   $��0 0 15 0q�6/   $��Center=�6A    � RowDefinitions 5I      �� 5J   "   $
��auto �60   6@   �� 5K   "   $
��auto �60   6@   .+�5M      ��  ��Name$ label_NotiTitle��6(   $��0��6A   $	 0.7Q�6N   $��0 -2 0 0q�6�   $��14(�6�   $
��Bold"�6�   ��6\   �� *Header+$
 root����6�   �� $ 
colorTitle�$
 root��6�   (� 5N      $��1��6&   $��0 2 0 2q�63   $
��Left=�6D   .��5O   "   ��  �� Orientation$ Vertical=�6-   $	��175��6D   ��6Q   �� $ ActualWidth�$ label_NotiTitle��.+�5P   &   �� $ subHeaderText��60   $��12(�5Q   #   $
��Wrap=�5R   #   ���	#FFA9A9A9��5T   #   �� *
Contents+$
 root��5U   #   5V      5W      5X      5Y      5Z      5[      5^      