   M S B A M L   `   `   `  ����  PixelMatrixEditor.    (PixelMatrixEditor.Views.NewAnimationView   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  New Animation��5   	    � 
ResizeMode$ NoResize=�6    � SizeToContent$ WidthAndHeight=�65   .��5      � $��5q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   6,   �� 5      $
��auto �6   6,   �� 5	      6    � ColumnDefinitions 5   
   �� 5      $
��auto �6   $	��100��6,   6=   �� 5      6!   .+�5   
   �� $��Color depth:��6   $��0��6(   $��0��65   $
��Left=�6E   $��Center=�6`   6}   �� 5   
   $��5q�6   $��0��6�   $��1��6�   ��6   �� *AvaliableColorDepths+ �� SelectedItem 6K   �� *SelectedColorDepth+ �� Mode$ TwoWay=�6�   �� 5   
   $��Frame size:��6   $��1��6'   $��0��64   $
��Left=�6D   $��Center=�6_   6|   � 5   
   $��1��6   $��1��6   $��5q�6-    5      �� 5      6%   �� 5      $
��auto �6#   62   �� 5      6%   .+�5      �� $��0��62   ��6   �� *
FrameWidth+6D   �� 5      $��X��6   $��1��6!   $��Center=�61   $��5,0,5,0q�6N   6a   �� 5      $��2��63   ��6   �� *FrameHeight+6E   5      �� 5    
    �� Orientation$ 
Horizontal=�6   $��Right=�6.   $��2��6J   $��1��6W   .+�5!      �� $��Cancel��6   $��75��6&   $��5q�61    �� IsCancel . 6N   �� 5"      -   5        $��Ok��5"      $��75��6"   $��5q�6-   	 �� 	IsDefault	 . 6d   5#      5$      5%      