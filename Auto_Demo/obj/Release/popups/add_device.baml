   M S B A M L   `   `   `  ���� D  @Auto_Demo, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:Auto_Demo.popupsAuto_Demo.popups  !    Auto_Demo.popups.add_device   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     0localclr-namespace:Auto_Demo.popups     -     �Name$  window_main�� � Icon$, &pack://application:,,,/images/icon.png�� � Title$ 
add_device�� � AllowsTransparency .  � WindowStyle$
 None=� � 
ResizeMode$ NoResize=�$	��300��$	��380����Z�  � 	Resources  �� MergedDictionaries �� Source��  �� $ ../custom/button.xaml?��� $ ../custom/scrollbar.xaml?��� $- '../animations/toggle_select_device.xaml?�%    .��� .+��� $��Right=�$	��380�����#1F1E25���#0A060E	 �� RadiusY$	 14.214��
 �� RadiusX$
 14.214���� $��
0,50,10,10q����#1F1E25$	 8��$
 8��$��Right=�$	��360��� $��	0,0,0,250q� � ColumnDefinitions �� $
��auto ��� .+�(� D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    :x,http://schemas.microsoft.com/winfx/2006/xaml     -   $
��15 0q�$��1��$��Right=�$��24��$��24�� � Cursor$
 Hand��.���� $��24��$��24��.+��� -    ��Name$ icon_ClosePrimaryGroup�� J� Opacity$	 0.7Q�$��24��$��24��.+�F� -    F�Name$ icon_ClosePrimary1������ � Q� �LI� �� 	���#EF4444$	��1.5�� �� StrokeStartLineCap$ round=� �� StrokeEndLineCap$ round=� �� StrokeLineJoin$ round=�F� -   $ icon_ClosePrimary2������LI� Q� � � �� 	���#EF4444$	��1.5��$ round=�$ round=�$ round=�F� -   $ icon_CloseSecondary��$ 1Q������@T� ��O���� ��O� -1��O��O -1��O��� р�O@T� Ӏ�O 	=  -1�� ��� �� �@T� �� � 	= �� ��  	= �� @T� р� ��� Ӏ�  -1 	= ��O@T� ��O	���#FFFFFF$	��1.5��$ round=�$ round=�$ round=����	#00000000 J� RenderTransformOrigin$ 
0.097,0.16&��� $&�� Thiết lập thiết bị mới�����#E5E7EB$��18(�$��Center=�$
��15 0q�� $��	0,55,0,10q�$	��300��$��Center=� ��  �� $  ../icons/chevron-down.xaml?��� $ ../icons/chevron-up.xaml?�%     � RowDefinitions �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� .+��� $��0��$��thiết bị��$	��300��$	��0 6q����	#FF7D7D85$��14(��� -   $
 Hand��$��1��$��Center=�$��44��$	 8��$
 8��$	��Top=�$	��300�����	#FF2D2D31� $��1�� �� �� $
��auto �.+�� -    �Name$
 grid��$��0��$��12 2 0 2q�$
 Hand�� �� .+�� $��0�� �� $��40 ��� .+��� -	    ��Name$ img_IconEmulator��$��3 0 0 0 q�$
��Left=�$��25��$
��auto��� $��1�� �� �� $
��auto �.+��� -
    ��Name$ tb_SelectName��$��16(�
�������$��1��$��0 0 0 4q��� -   $ label_ID��$��0��$��0 3 0 0q����	#FF7D7D85$��10(�$��16��$
��Left=��� -   $ tb_SelectID��$��0��$��10(�
�������$��16 3 0 0q���  J� IsHitTestVisible .  $��0��$��Chọn thiết bị��$��16(�$��Center=�$
��Left=�$��0,0,0,4q�
�����������  �� 
TargetType" ��.���  �� Property � y� �� Value$ 	Collapsed=� �� Triggers ��  �� Binding �� *Text+ �� ElementName$ tb_SelectID�� �� Value  .a���  � y�$ Visible=��� -     ��Name$  lb_ButonOpen��$��1��$��Center=�$
��10 0q�   chevrondownlinearsvg#��[  $��18��$��18��$
 Hand���� -   $  lb_ButtonClose��$��1��$��Center=�$
��10 0q�  chevronuplinearsvg#��[ $��18��$��18��$
 Hand��$y�Hidden=��� $	��Top=�$��2��$��tên phân biệt��$	��300��$��0 10 0 6q����	#FF7D7D85$��14(��� $��3��$��Center=�$��44��$	 8��$
 8��$	��Top=�$	��300�����	#FF2D2D31� $��3��.+��� -   ! ��Name$! tb_Name��$
��Left=�$��34��$	��0 4q�$��11 5 0 5q�" �� TextWrapping$
" Wrap=�$��Center=�# �� 
CaretBrush# �	#FF7D7D85$	��276��$��0q�$��16(�
���������Z� ��  .  $��Nhập tên phân biệt��$��16(�$��Center=�$
��Left=�$��12,0,0,4q�
����������� " ��.���  � y�$ 	Collapsed=� ��  �� *Text+$ tb_Name��  .a���  � y�$ Visible=��� -   $ ��Name$$ button_AddDevice��$��4��$��44��$��18(�$��	0 16 0 10q�  custom_Button#��[ .���� % �� Orientation$% 
Horizontal=� �� $ ../icons/add-square.xaml?�%    .+��� $��4 0 0 0q�  addsquaresvg#��[ $��24��$��24���� 
�������$	��4 0q�$��Thêm thiết bị���� -   & ��Name$& 	rectangle��$
��Left=�$��6��$��0,4,0,0q�$��2��$ round=�$��3��$ round=�$	��1.5��$ round=�$	��Top=�$	��300�����	#FF2D2D31$
 8��$	 8��$y�Hidden=��� -   ' ��Name$' lb_ListDevice��$��2����.  $
��Left=�$	��134��$��0,7,0,0q�$��3��$	��Top=�$	��300��
�������$( �� IsSynchronizedWithCurrentItem$( FalseY�$y�Hidden=���Z� ��Z� ���� .R�� $��0��$��12 2 0 2q� �� $��36 ��� $
��auto �.+��� $
��Left=�$��25��$
��auto������ *icon+� $��1������ " �.���  � y�$ Visible=� ��  �� *
isCustom+ True.a���  � y�$ Hidden=� �� �� $
��auto �.+��� $��16(�
�������$��1��$��0 0 0 4q����� *title+�� $
��ID: ��$��0��$��0 3 0 0q����	#FF7D7D85$��10(�$��16��$
��Left=��� $��0��$��10(�
�������$��16 3 0 0q����� *id+� $��1������ " �.���  � y�$ Visible=� ��  �� *
isCustom+	 False.a���  � y�$ Hidden=� �� �� $
��auto �.+��� $! tb_CustomName��-   $��0��$
��Left=�$��34��$	��0 4q�$��0 2 10 0q�$
" Wrap=�$��Center=�# �	#FF7D7D85$	��150��$��0q�$��16(�
���������Z� �� $��0�� .  $��Nhập tên giả lập��$��16(�$��Center=�$
��Left=�$��1 0 0 4q�
����������� " ��.���  � y�$ 	Collapsed=� ��  �� *Text+$ tb_CustomName��  .a���  � y�$ Visible=��� $$ button_AddDevice��-   $��1��$��Right=�$��Center=�$��16(�$��30��$��60��#��[ .���� 
�������$��0 -2 0 0q�$��thêm������ " ��.���  �@J�SnapsToDevicePixels . ��  �@�OverridesDefaultStyle . ��  � ��$ 3 0 3 0q���  � �� �� ) �� 
TargetType") ��.R��� $��Border��* �� CornerRadius$* 8�����	#FF2D2D31._��� $
��Left=�$��Center=���. + �� Triggers+ P� , P� Property, �@J�IsMouseOver- P� Value- . . ���  �@�Cursor$
 Hand���� . �� 
TargetName$. Border�� � �� �#1F1E25P� , �@J�IsMouseOver- .  . ���  �@�Cursor$ Arrow�� ��  �� *
isCustom+ True.a���  �@�Cursor$ Arrow����  �� *
isCustom+	 False.a���  �@�Cursor$
 Hand��