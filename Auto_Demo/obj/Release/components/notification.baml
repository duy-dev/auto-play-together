   M S B A M L   `   `   `  ���� D  @Auto_Demo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:Auto_Demo.componentsAuto_Demo.components  '    !Auto_Demo.components.notification   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     4local"clr-namespace:Auto_Demo.components     -     >�Name$
  root�� � 	Resources  �� MergedDictionaries �� Source��  �� $  ../icons/noti-success.xaml?��� $ ../icons/noti-error.xaml?��� $ ../icons/noti-info.xaml?��� $  ../icons/noti-warning.xaml?�%    ���� .R�� $��0 9 0 0q� � ColumnDefinitions �� $	��42* �����  �� Path$ ActualWidth� �� ElementName$
 aaa1��.+���  �� CornerRadius$ 8��
���   �$��70��$��Right=����� $ ActualWidth�$
 aaa1������  �� EndPoint$ 0.5,1&�	 �� 
StartPoint$	 0.5,0&�
 �� RelativeTransform
 V� .~���  �� CenterY$	 0.5Q� �� CenterX$	 0.5Q���  �� CenterY$	 0.5Q� �� CenterX$	 0.5Q���  �� Angle$	 270Q� �� CenterY$	 0.5Q� �� CenterX$	 0.5Q�T� .���  � Offset$ 0Q� � Color �� $ colorMessage1�$
 root��� $ 0.125Q� �� $ colorMessage2�$
 root��� $ 0.234Q� �� $ colorMessage3�$
 root��� $ 0.324Q� �� $ colorMessage4�$
 root��._��  �Name$
 aaa1��$��0��$��54������ $ ActualWidth�$
 aaa1������ $ 0.5,1&�$	 0.5,0&�
 V� .~��� $	 0.5Q�$	 0.5Q��� $	 0.5Q�$	 0.5Q��� $	 270Q�$	 0.5Q�$	 0.5Q�T� .��� $ 0Q� �� $ colorMessage1�$
 root��� $ 0.125Q� �� $ colorMessage2�$
 root��� $ 0.234Q� �� $ colorMessage3�$
 root��� $ 0.324Q� �� $ colorMessage4�$
 root�� �� $��50 ��� $
��auto �.+���  J� Opacity$	 0.8Q�$��0 0 0 0q�$��0��   tickcirclesvg#��[  $��24��$��24��y��� $ iconSuccess�$
 root���� $	 0.8Q�$��0 0 0 0q�$��0��  closecirclesvg#��[ $��24��$��24��y��� $ 	iconError�$
 root���� $	 0.8Q�$��0 0 0 0q�$��0��  infocirclesvg#��[ $��24��$��24��y��� $ iconInfo�$
 root���� $	 0.8Q�$��0 0 0 0q�$��0��  	dangersvg#��[ $��24��$��24��y��� $ iconWarning�$
 root��� $��1��$��0 0 15 0q�$��Center=� � RowDefinitions �� $
��auto ��� $
��auto �.+���  ��Name$ label_NotiTitle��$��0��$	 0.7Q�$��0 -2 0 0q�$��14(�$
��Bold"����� *Header+$
 root������ $ 
colorTitle�$
 root��(� $��1��$��0 2 0 2q�$
��Left=�.����  �� Orientation$ Vertical=�$	��175������ $ ActualWidth�$ label_NotiTitle��.+��� $ subHeaderText��$��12(�$
��Wrap=����	#FFA9A9A9���� *
Contents+$
 root��