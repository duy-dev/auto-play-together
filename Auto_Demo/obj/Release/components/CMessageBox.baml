   M S B A M L   `   `   `  ���� D  @Auto_Demo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:Auto_Demo.componentsAuto_Demo.components  &     Auto_Demo.components.CMessageBox   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     4local"clr-namespace:Auto_Demo.components     -     �Name$  window_main�� � Title$ CMessageBox�� � 
ResizeMode$ NoResize=�$	��200��$	��380�� � AllowsTransparency .  � WindowStyle$
 None=�$y�Visible=���Z� .��� .+��� $	��200��$	��380�����#0A060E �� CornerRadius$ 14��._��  � RowDefinitions �� $��50 ��� .+��  � ColumnDefinitions �� $	��320 ��� .+��� -    ��Name$ tb_Title��$��0��	 �� 
LineHeight$	 30��$��Tiêu đề thông báo�����#E5E7EB$��18(�$��Center=�$��15 0 0 0q�(� D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     -   $��	15 0 15 2q�$��1��$��Right=�$��24��$��24��
 � Cursor$

 Hand��.���� $��24��$��24��.+��� -    ��Name$ icon_ClosePrimaryGroup�� J� Opacity$	 0.7Q�$��24��$��24��.+�F� -    F�Name$ icon_ClosePrimary1������ � Q� �LI� �� 	���#EF4444$	��1.5�� �� StrokeStartLineCap$ round=� �� StrokeEndLineCap$ round=� �� StrokeLineJoin$ round=�F� -   $ icon_ClosePrimary2������LI� Q� � � �� 	���#EF4444$	��1.5��$ round=�$ round=�$ round=�F� -   $ icon_CloseSecondary��$ 1Q������@T� ��O���� ��O� -1��O��O -1��O��� р�O@T� Ӏ�O 	=  -1�� ��� �� �@T� �� � 	= �� ��  	= �� @T� р� ��� Ӏ�  -1 	= ��O@T� ��O	���#FFFFFF$	��1.5��$ round=�$ round=�$ round=����	#00000000 J� RenderTransformOrigin$ 
0.097,0.16&��� $	��Top=�$��1��$	��140��$	��360�����#1F1E25$ 14��._��  �� �� $��50 �.+��� $��0��$��Center=� �� Orientation$ Vertical=�$��14 10q�.+��� -   $ 
tb_Content��$��16(�$
��Wrap=����	#FFA9A9A9$=��7Bạn có muốn xoá máy này không? ahihi một hai��� $	��Top=�$��1�� �� �� .+��� -	   $��0��$

 Hand��$��15 0 0 0q����	#FF1F1E25���#059669$ 8��$	��120��$��40��$��2q�����  �� 
TargetType" ��.���  �� Property �@J�Opacity �� Value$	 0.7Q� �� Triggers P�  P� Property �@J�IsMouseOver P� Value . . ���  �@J�Opacity$ 1Q�._��� -
   $ tb_CancelButton��$��
Huỷ bỏ��$��18(�$��Center=�$��Center=�
��������� -   $��1��$

 Hand��$��0 0 15 0q����#059669$ 8��$	��120��$��40������ " ��.���  �@J�Opacity$	 0.7Q� P�  �@J�IsMouseOver . . ���  �@J�Opacity$ 1Q�._��� -   $ tb_OKButton��$
��Xoá��$��18(�$��Center=�$��Center=�
�������