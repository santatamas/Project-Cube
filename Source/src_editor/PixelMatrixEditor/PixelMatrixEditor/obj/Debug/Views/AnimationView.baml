   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5      L HPixelMatrixEditor, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:PixelMatrixEditor.ViewsPixelMatrixEditor.Views 5      +   %PixelMatrixEditor.Views.AnimationView   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      9system&clr-namespace:System;assembly=mscorlib     5      7views%clr-namespace:PixelMatrixEditor.Views     5        � 	Resources  5         ControlLightColor  BorderMediumColor �� 
TargetType �� Property �� Value �� 
TargetType �� Color � ColumnDefinitions �� Triggers P� Property	 P� Value
 �� 
TargetName  DisabledBorderLightColor��%s  &	        &	      '�� >     7   7  ��5	   
   	White��61   ��5
   
   	#FF888888��65   �� 5   
   " ��5      .�5      ��  �@J�SnapsToDevicePixels . 5      �� 5       �@�OverridesDefaultStyle . 5      �� 5       � ��$
 Auto=�5      �� 5       � ��$
 Auto=�5      �� 5       � �� . 5      �� 5       � ��$ Center=�5      �� 5       � �� 5      �� 5      " ��6&   .R�5      �� $��Border��6!   $��1q�6/   ��5      �� 5   "   8   62   ��5       �� 5!   "   8  62   ._�5#      �� .��5$   "   �  5%   &   �� 5&   *   $��* �6;   6G   �� 5'   *   $
��Auto �6;   6J   .+�5)   &   �� $��0��65   �� 5*   &   $��1��6-   $��	Add Frame��6=   $	��150��6Q   $	��150��6]   ��6j   �� *AddFrameCommand+6�   5+   #   5,      5-       5.      P� 5/       �@��
IsGrouping	 . . �50   "   ��  � �� .  6R   51      P� 52       � {�	 .  . �53   "   �� $
 Border��6)    � �� 54   &   �� 55   *   # �  6:   57   #   58      5:      5<      5=      5>      .��5?      �� -   5         ��Name$ AnimListView��5?      ��6%   ��  �� Path$ Animation.Frames� �� SelectedItem 6S   �� $ CurrentMatrix� �� Mode$ TwoWay=���5@   
   �� 5A      .R�5B      ��  �� Orientation$ 
Horizontal=�6   5C      ��5E   
   �� 5F      .R�5G      �  5H      �� 5I      $��* �6+   67   �� 5J      $
��auto �6+   6:   .+�5L      -  'PixelMatrixEditor.Views.PixelMatrixView    Name$ 
MatrixView��6i   $��2��6,   $��0��6@   $	��150��6P   $	��150��6\      MatrixSource 6}   ��    AutoRefresh 5M   ,   �� $ 
IsSelected� �� RelativeSource �� *FindAncestor+ �� AncestorType" ��   RenderDepth 5N   ,   ��  �� *FindAncestor+" ��$&  DataContext.Animation.ColorDepth�$ OneWay=��� 5O      $��X��6   
���  ��$��20��5P      $��20��6(   $��1��5Q      $	��Top=�6-   ��5R      ��  �� *FindAncestor+" ��$$ DataContext.DeleteFrameCommand�$ OneWay=���6�   �� ��5S      �� 5T      " ��6$    �� Triggers 5U   "   P� 5V   &    �@J�IsMouseOver	 . . �5W   *   ��  �@J�Opacity$ 1Q�6D   6P   5X   '   P� 5Y   &    �@J�IsMouseOver	 .  . �5Z   *   ��  �@J�Opacity$	 0.1Q�6D   6R   5[   '   5]      5_      5`      5a      5c      5d      