   M S B A M L   `   `   `  ����D  @Auto_Demo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:Auto_Demo.testAuto_Demo.test  5   	       Auto_Demo.test.Window1   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:Auto_Demo.test     5   	     � Title$  Window1��5   	   $��377.459��6   $��427.459��6*    � 	Resources 5	         ScrollThumbs �� 
TargetType �� Property �� Value �� 
TargetType �Name ��Name �� CornerRadius	 �� Triggers
 P� Property P� Value �� 
TargetName ]�Name ]� Thumb n�Name ]� IncreaseRepeatButton ��Name J� Opacity ]� DecreaseRepeatButton �� Angle��%&  &	        '�� [    7   �� 5
   
   " n�6%   .�5      ��  � �� 5      �� 5      " n�6&   .R�5      � $
 Grid��6   .+�5      �� $��Stretch=�6(   $��Stretch=�6F   $
��Auto��6b   $
��Auto��6o   
������ 6}   �� 5      $ ScrollBarRectangle��6%   $ 5��6A   $��Stretch=�6R   $��Stretch=�6p   $
��Auto��6�   $
��Auto��6�   #��z��6�   5      	 5      P� 5      
 �@�Tag$ 
Horizontal��65   . �5   "   �� $ ScrollBarRectangle��6)    � ��$
 Auto��6Z   �� 5   "   $ ScrollBarRectangle��6)    � ��$ 7��6[   5      5      5      5      �� 5   
   " ��6   .�5      ��  � ��$ -5,0,0,0q�6'   �� 5       �@��IsFlicksEnabled .  67   �� 5       � �� �	#FF8C8C8C6+   �� 5        � ��
 ���� 6+   �� 5!       � ��$ 8��6&   �� 5"       � �� 5#      �� 5$      " ��6&   .R�5%      � $ GridRoot��6   $��8��61   #��z��6;   .+�5&      ]� $ 
PART_Track��6$   ��. |�.   5'   "   n� 5(   &   $ ScrollBarThumb��6,   #��z��6D   8��  6n    5*   "   �� 5+   &   $ PageUp��63   $��ScrollBar.PageDownCommand��6C   $ 0Q�6g   |�.  6s    5-   "   �� 5.   &   $ PageDown��63   $��ScrollBar.PageUpCommand��6E   $ 0Q�6g   |�.  6s   50      51      	 53      P� 54      
 � {� .  . �55   "   �� $ ScrollBarThumb��6)    � y�$ 	Collapsed=�6[   56      P� 57      
 �@��Orientation$ 
Horizontal=�6=   . �58   "   �� $ GridRoot��6)    �@�LayoutTransform 59   &   �� 5:   *   $	 -90Q�6:   5<   #   �� 5=   "   $ 
PART_Track��6)    �@�LayoutTransform 5>   &   �� 5?   *   $	 -90Q�6:   5A   #   �� 5B   "    � ��$
 Auto��6:   �� 5C   "    � ��$ 8��6;   �� 5D   "   $ ScrollBarThumb��6)    �@�Tag$ 
Horizontal��6T   �� 5E   "   $ PageDown��6)    � ��$ ScrollBar.PageLeftCommand��6R   �� 5F   "   $ PageUp��6)    � ��$  ScrollBar.PageRightCommand��6P   5G      5I      5K      5L      5M      .��5N      �  � RowDefinitions 5O   
   �� 5P      �� 5Q      .+�5T   
   �� -   5         ��Name$ scroll��5T      .��5U      �� $	��200��6   5V      �� 5X   
   $��1��6   $��Visible=�6   $
��0,10q�6R   .@�5Y      �� .��6   Test 16"   �� 5Z      .��6   Test 26"   �� 5[      .��6   Test 36"   �� 5\      .��6   Test 46"   �� 5]      .��6   Test 56"   �� 5^      .��6   Test 66"   �� 5_      .��6   Test 76"   �� 5`      .��6   Test 86"   �� 5a      .��6   Test 96"   �� 5b      .��6   	Test 106#   �� 5c      .��6   	Test 116#   5d      5e      5f      