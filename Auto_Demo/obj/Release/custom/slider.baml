   M S B A M L   `   `   `  ���� \  XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089   SliderCustom  �� 
TargetType ��Name J� SnapsToDevicePixels � RowDefinitions m�Name m� Fill m� 	Placement ��Name ]�Name	 ]� DecreaseRepeatButton
 �� 
TargetType �� Property �� Value ]� IncreaseRepeatButton ]� Thumb n�Name � OverridesDefaultStyle � UseLayoutRounding F�Name �� Stretch �� Triggers P� Property P� Value �� 
TargetName�� D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     :x,http://schemas.microsoft.com/winfx/2006/xaml      %�  &	        �� "  ��.R��� $ border��#��z��#��z��#��z�� . ._��  �� $
��Auto ��� $
��Auto �#��z���� $
��Auto �.+�m� $ TopTick��# z��$��4��$��0,0,0,2q�$	 Top=�$��0��$y�	Collapsed=�m� $ 
BottomTick��# z��$��4��$��0,2,0,0q�$ Bottom=�$��2��$y�	Collapsed=��� $ TrackBackground�����	#FF1F1E25$��1q����	#FF1F1E25$��4��$	��5,0q�$��1��$��Center=�._��� $��-6,-1q�.+��� $ PART_SelectionRange��#��� ��$��4��$y�Hidden=�]� $ 
PART_Track��$��1��	 �� $��Slider.DecreaseLarge������ "
 ��.���  �@�OverridesDefaultStyle . ��  � ��
 ���� ��  � |� .  ��  � �� .  ��  � �� �� "  ��.R��� #��z��#��z��#��z�� �� $��Slider.IncreaseLarge������ "
 ��.���  �@�OverridesDefaultStyle . ��  � ��
 ���� ��  � |� .  ��  � �� .  ��  � �� �� "  ��.R��� #��z��#��z��#��z�� n� $ Thumb��|�.  $��18�� . $��Center=�$��11������ "  n�.R�� $��Center=� . $��Center=�.+�F� $
 grip��d����        �        �ا �ا ��ا     �ا ���ا ����ا �������    ��	���	#FFF0F0F0$
 Fill=� . ���	#FFACACAC$��1�� . $��Center=� P�  �@J�IsMouseOver . . ��� $
 grip�� � �� �	#FFDCECFC�� $
 grip�� � �� �	#FF7EB4EAP�  �@n�
IsDragging . . ��� $
 grip�� � �� �	#FFDAECFC�� $
 grip�� � �� �	#FF569DE5P�  � {� .  . ��� $
 grip�� � �� �	#FFF0F0F0�� $
 grip�� � �� �	#FFD9D9D9 P�  �@��TickPlacement$ TopLeft=�. ��� $ TopTick�� � y�$ Visible=��� $ Thumb�� � �� �� "  n�.R�� $��Center=� . $��Center=�.+�F� $
 grip�������    ��[ �    ��[ `�S `�S �`�S     �ا ��[ �ا ��[ ��ا ��[ �ا ���ا ����ا �������    ����[ ��[ 	���	#CC059669$
 Fill=� . ���	#FF2F5147$��1�� . $��Center=� P�  �@J�IsMouseOver . . ��� $
 grip�� � �� �	#FF059669�� $
 grip�� � �� �	#FF2F5147P�  �@n�
IsDragging . . ��� $
 grip�� � �� �	#FF059669�� $
 grip�� � �� �	#FF2F5147P�  � {� .  . ��� $
 grip�� � �� �	#FFF0F0F0�� $
 grip�� � �� �	#FFD9D9D9�� $ TrackBackground�� � ��$ 5,2,5,0q�P�  �@��TickPlacement$ BottomRight=�. ��� $ 
BottomTick�� � y�$ Visible=��� $ Thumb�� � �� �� "  n�.R�� $��Center=� . $��Center=�.+�F� $
 grip�������     � �     � `�S ��`�S ���`�S ���ا  � �ا  � ��ا  � �ا �ا ��ا     �         �  � 	���	#FFF0F0F0$
 Fill=� . ���	#FFACACAC$��1�� . $��Center=� P�  �@J�IsMouseOver . . ��� $
 grip�� � �� �	#FFDCECFC�� $
 grip�� � �� �	#FF7EB4EAP�  �@n�
IsDragging . . ��� $
 grip�� � �� �	#FFDAECFC�� $
 grip�� � �� �	#FF569DE5P�  � {� .  . ��� $
 grip�� � �� �	#FFF0F0F0�� $
 grip�� � �� �	#FFD9D9D9�� $ TrackBackground�� � ��$ 5,0,5,2q�P�  �@��TickPlacement$
 Both=�. ��� $ TopTick�� � y�$ Visible=��� $ 
BottomTick�� � y�$ Visible=�P�  �@��IsSelectionRangeEnabled . . ��� $ PART_SelectionRange�� � y�$ Visible=�P�  �@J�IsKeyboardFocused . . ��� $ Thumb�� � ��
 ��  �